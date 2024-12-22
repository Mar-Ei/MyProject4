using BackendApi.Help.BusinessLogic.Helpers;
using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Models.Accounts;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly IOptions<AppSettings> _appSettings;
        private readonly IEmailService _emailService;

        public AccountsService(
      IRepositoryWrapper repositoryWrapper,
      IJwtUtils jwtUtils,
      IMapper mapper,
      IOptions<AppSettings> appSettings, 
      IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings;  
            _emailService = emailService;
        }
        private void removeOldRefreshToken(User account)
        {
            // Теперь доступ к _appSettings.Value.RefreshTokenTTL
            account.RefreshTokens.RemoveAll(x =>
            x.IsActive &&
            x.Created.AddDays(_appSettings.Value.RefreshTokenTTL) <= DateTime.UtcNow);
        }
        public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var account = await _repositoryWrapper.User.GetByEmailWithToken(model.Email);

            // Validate
            if (account == null || !account.IsVerified || !BCrypt.Net.BCrypt.Verify(model.Password, account.Password))
                throw new AppException("Email or password is incorrect");

            // Authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtUtils.GenerateJwtToken(account);
            var refreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            // Remove old refresh tokens
            removeOldRefreshToken(account);

            // Save changes to db
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<AccountResponse> Create(CreateRequest model)
        {
            // Validate if email already exists
            if ((await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Any())
                throw new AppException($"Email '{model.Email}' is already registered");

            // Map model to new account object
            var account = _mapper.Map<User>(model);
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            // Hash password
            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // Save account
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        private async Task<User> getAccount(int id)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.UserId == id)).FirstOrDefault();
            if (account == null) throw new KeyNotFoundException("Account not found");
            return account;
        }

        public async Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            var account = await getAccount(id);

            // Validate if email already exists
            if (account.Email != model.Email && (await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Any())
                throw new AppException($"Email '{model.Email}' is already registered");

            // Hash password if entered
            if (!string.IsNullOrEmpty(model.Password))
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            }

            // Copy model to account and save
            _mapper.Map(model, account);
            account.Updated = DateTime.UtcNow;
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            return _mapper.Map<AccountResponse>(account);
        }

        public async Task Delete(int id)
        {
            var account = await getAccount(id);
            await _repositoryWrapper.User.Delete(account);
            await _repositoryWrapper.Save();
        }

        public async Task<IEnumerable<AccountResponse>> GetAll()
        {
            var accounts = await _repositoryWrapper.User.FindAll();
            return _mapper.Map<IList<AccountResponse>>(accounts);
        }

        public async Task<AccountResponse> GetById(int id)
        {
            var account = await getAccount(id);
            return _mapper.Map<AccountResponse>(account);
        }

        private async Task<string> generateResetToken()
        {
            // Token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // Ensure token is unique by checking against db
            var tokenIsUnique = (await _repositoryWrapper.User.FindByCondition(x => x.ResetToken == token)).Any() == false;
            if (!tokenIsUnique)
                return await generateResetToken();

            return token;
        }

        public async Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).FirstOrDefault();

            // Always return ok response to prevent email enumeration
            if (account == null) return;

            // Create reset token that expires after 1 day
            account.ResetToken = await generateResetToken();
            account.ResetTokenExpires = DateTime.UtcNow.AddDays(1);

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }

        private async Task<User> getAccountByRefreshToken(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(u => u.RefreshTokens.Any(t => t.Token == token))).SingleOrDefault();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        private async Task<RefreshToken> rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = await _jwtUtils.GenerateRefreshToken(ipAddress);  
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;  // Возвращаем новый refreshToken, а не старый
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }
        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User account, string ipAddress, string reason)
        {
            //recursively traverse the refresh token chain and ensure all descendants are revoked
            if (!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = account.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, account, ipAddress, reason);
            }
        }

        public async Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (refreshToken.IsRevoked)
            {
                //revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, account, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                await _repositoryWrapper.User.Update(account);
                await _repositoryWrapper.Save();
            }
            if (!refreshToken.IsActive)
            {
                throw new AppException("Invalid token");
            }
            //replace old refresh token with a new one (rotate token)
            var newRefreshToken = await rotateRefreshToken(refreshToken, ipAddress);
            account.RefreshTokens.Add(newRefreshToken);

            //remove old refresh tokens from account
            removeOldRefreshToken(account);

            //save changes to db
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();

            //generate new jwt
            var jwtToken = _jwtUtils.GenerateJwtToken(account);

            //return data in authenticate response object
            var response = _mapper.Map<AuthenticateResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = newRefreshToken.Token;
            return response;

        }
        private async Task<string> generateVerificationToken()
        {
            // Generate a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64));

            // Ensure the token is unique by checking against db
            var tokenIsUnique = !(await _repositoryWrapper.User.FindByCondition(x => x.VerificationToken == token)).Any();  // Use Any() instead of Count == 0
            if (!tokenIsUnique)
                return await generateVerificationToken();

            return token;
        }
        public async Task Register(RegisterRequest model, string origin)
        {
            // Validate if the email already exists
            if ((await _repositoryWrapper.User.FindByCondition(x => x.Email == model.Email)).Any())
            {
                return;
            }

            // Map model to new account object
            var account = _mapper.Map<User>(model);

            // First registered account is an admin
            var isFirstAccount = !(await _repositoryWrapper.User.FindAll()).Any();  // Use Any() instead of Count == 0
            account.Role = isFirstAccount ? Role.Admin : Role.User;
            account.Created = DateTime.UtcNow;
            account.Verified = DateTime.UtcNow;
            account.VerificationToken = await generateVerificationToken();

            // Hash the password
            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            // Save the account (assuming there's further save logic here)
        }
        private async Task<User> getAccountByResetToken(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x =>
            x.ResetToken == token && x.ResetTokenExpires > DateTime.UtcNow)).SingleOrDefault();
            if (account == null) throw new AppException("Invalid token");
            return account;
        }

        public async Task ResetPassword(ResetPasswordRequest model)
        {
            var account = await getAccountByResetToken(model.Token);

            //update password and remove reset token
            account.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            account.PasswordReset = DateTime.UtcNow;
            account.ResetToken = null;
            account.ResetTokenExpires = null;

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }

        public async Task RevokeToken(string token, string ipAddress)
        {
            var account = await getAccountByRefreshToken(token);
            var refreshToken = account.RefreshTokens.Single(x => x.Token == token)
;
            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            //revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }



        public async Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            await getAccountByResetToken(model.Token);
        }

        public async Task VerifyEmail(string token)
        {
            var account = (await _repositoryWrapper.User.FindByCondition(x => x.VerificationToken == token)).FirstOrDefault();

            if (account == null)
                throw new AppException("Verification failed");

            account.Verified = DateTime.UtcNow;
            account.VerificationToken = null;

            await _repositoryWrapper.User.Update(account);
            await _repositoryWrapper.Save();
        }
    }
}
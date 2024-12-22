﻿using BusinessLogic.Authorization;
using BusinessLogic.Helpers;
using BusinessLogic.Models.Accounts;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using MapsterMapper;
using Microsoft.Extensions.Options;
using System.Security.Cryptography;

namespace BusinessLogic.Services
{
    public class AccountsService : IAccountService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IJwtUtils _jwtUtils;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public AccountsService(
            IRepositoryWrapper repositoryWrapper,
            IJwtUtils jwtUtils,
            IMapper mapper,
            AppSettings appSettings,
            IEmailService emailService)
        {
            _repositoryWrapper = repositoryWrapper;
            _jwtUtils = jwtUtils;
            _mapper = mapper;
            _appSettings = appSettings;
            _emailService = emailService;
        }

        public Task<AuthenticateResponse> Authenticate(AuthenticateRequest model, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> Create(CreateRequest model)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task ForgotPassword(ForgotPasswordRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<AccountResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticateResponse> RefreshToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task Register(RegisterRequest model, string origin)
        {
            throw new NotImplementedException();
        }

        public Task ResetPassword(ResetPasswordRequest model)
        {
            throw new NotImplementedException();
        }

        public Task RevokeToken(string token, string ipAddress)
        {
            throw new NotImplementedException();
        }

        public Task<AccountResponse> Update(int id, UpdateRequest model)
        {
            throw new NotImplementedException();
        }

        public Task ValidateResetToken(ValidateResetTokenRequest model)
        {
            throw new NotImplementedException();
        }

        public Task VerifyEmail(string token)
        {
            throw new NotImplementedException();
        }
    }
}
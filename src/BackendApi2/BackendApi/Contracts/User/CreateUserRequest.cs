namespace BackendApi.Contracts.User
{
    public class CreateUserRequest
    {

        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Middlename { get; set; } = null!;
        public DateTime Birthdate { get; set; }

        public string Login { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;


    }
    
}

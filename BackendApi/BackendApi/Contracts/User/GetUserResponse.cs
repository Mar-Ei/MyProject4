namespace BackendApi.Contracts.User
{
    public class GetUserResponse
    {
        public int Id { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public DateTime? CreatedAt { get; set; }

    }
}

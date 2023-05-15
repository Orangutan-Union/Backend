namespace TECHUB.Service.ViewModels
{
    public class AuthenticatedResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}

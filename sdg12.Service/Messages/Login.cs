using MediatR;

namespace sdg12.Service.Messages
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public LoginCommand()
        {
        }

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponse
    {
        public bool IsValidUser { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
    }
}
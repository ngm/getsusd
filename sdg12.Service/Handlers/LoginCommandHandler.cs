using System.Linq;
using NHibernate.Linq;
using MediatR;
using NHibernate;
using sdg12.Core.Entities;
using sdg12.Service.Messages;

namespace sdg12.Service.Handlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly ISession nhSession;

        public LoginCommandHandler(ISession nhSession)
        {
            this.nhSession = nhSession;
        }

        public LoginResponse Handle(LoginCommand command)
        {
            var user = nhSession.Query<User>()
                .FirstOrDefault(u => u.UserName == command.Username && u.Password == command.Password);

            if (user == null)
                return new LoginResponse { IsValidUser = false };

            return new LoginResponse
            {
                IsValidUser = true,
                UserId = user.Id,
                UserName = user.UserName
            };
        }
    }
}
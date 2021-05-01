using System.Threading;
using System.Threading.Tasks;
using Application.Users.Queries.GetUserByEmail;
using Domain.Entities.User;
using MediatR;

namespace Application.Users.Commands.LogoutUser
{
    internal class LogoutUserHandler : IRequestHandler<LogoutUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly ISender mediator;

        public LogoutUserHandler(IUserRepository userRepository, ISender mediator)
        {
            this.userRepository = userRepository;
            this.mediator = mediator;
        }

        public async Task<Unit> Handle(LogoutUserCommand request, CancellationToken cancellationToken)
        {
            IUser user = await mediator.Send(new GetUserByEmailQuery { Email = request.Email });
            IUser mutatedUser = user.MutateSessionToken(null);
            await userRepository.UpdateAsync(mutatedUser);
            return Unit.Value;
        }
    }
}
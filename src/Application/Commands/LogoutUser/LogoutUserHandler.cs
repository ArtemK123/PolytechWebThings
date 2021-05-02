using System.Threading;
using System.Threading.Tasks;
using Application.Queries.GetUserByEmail;
using Application.Repositories;
using Domain.Entities.User;
using MediatR;

namespace Application.Commands.LogoutUser
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
            IUser user = await mediator.Send(new GetUserByEmailQuery(email: request.Email), cancellationToken);
            IUser mutatedUser = user.MutateSessionToken(null);
            await userRepository.UpdateAsync(mutatedUser);
            return Unit.Value;
        }
    }
}
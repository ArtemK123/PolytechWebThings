using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.User;
using Domain.Exceptions;
using Domain.Services;
using MediatR;

namespace Application.Users.Commands.LoginUser
{
    internal class LoginUserHandler : IRequestHandler<LoginUserCommand>
    {
        private readonly IGuidProvider guidProvider;
        private readonly IUserRepository userRepository;

        public LoginUserHandler(IUserRepository userRepository, IGuidProvider guidProvider)
        {
            this.userRepository = userRepository;
            this.guidProvider = guidProvider;
        }

        public async Task<Unit> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            IUser? user = await userRepository.GetByEmailAsync(request.Email);
            if (user == null)
            {
                throw new UserNotFoundByEmailException(request.Email);
            }

            if (user.Password != request.Password)
            {
                throw new WrongUserPasswordException(user.Email);
            }

            string userToken = GenerateToken();
            IUser updatedUser = user.MutateSessionToken(userToken);
            await userRepository.UpdateAsync(updatedUser);
            return Unit.Value;
        }

        private string GenerateToken()
        {
            // TODO: Change to JWT token
            return guidProvider.CreateGuid().ToString();
        }
    }
}
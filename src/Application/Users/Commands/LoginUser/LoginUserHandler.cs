using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Common;
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
        private readonly IFactory<StoredUserCreationModel, IUser> userFactory;

        public LoginUserHandler(IUserRepository userRepository, IGuidProvider guidProvider, IFactory<StoredUserCreationModel, IUser> userFactory)
        {
            this.userRepository = userRepository;
            this.guidProvider = guidProvider;
            this.userFactory = userFactory;
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
            StoredUserCreationModel storedUserCreationModel = new(user.Id, user.Email, user.Password, userToken, user.Role);
            IUser updatedUser = userFactory.Create(storedUserCreationModel);
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
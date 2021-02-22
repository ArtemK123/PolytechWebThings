using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Common;
using Domain.Entities.User;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IFactory<NewUserCreationModel, IUser> newUserFactory;

        public CreateUserCommandHandler(IUserRepository userRepository, IFactory<NewUserCreationModel, IUser> newUserFactory)
        {
            this.userRepository = userRepository;
            this.newUserFactory = newUserFactory;
        }

        public Task<Unit> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            IUser? optionalStoredUser = userRepository.GetByEmail(request.Email);
            if (optionalStoredUser != null)
            {
                throw new EmailTakenByOtherUserException(optionalStoredUser.Email, optionalStoredUser.Id);
            }

            IUser newUser = newUserFactory.Create(new NewUserCreationModel { Email = request.Email, Password = request.Password, Role = request.Role });
            userRepository.Add(newUser);
            return Task.FromResult(new Unit());
        }
    }
}
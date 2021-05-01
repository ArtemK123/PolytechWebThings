using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.User;
using Domain.Exceptions;
using MediatR;

namespace Application.Users.Queries.GetUserByEmail
{
    public class GetUserByEmailHandler : IRequestHandler<GetUserByEmailQuery, IUser>
    {
        private readonly IUserRepository userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<IUser> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            IUser? user = await userRepository.GetByEmailAsync(request.Email);
            if (user is null)
            {
                throw new UserNotFoundByEmailException(request.Email);
            }

            return user;
        }
    }
}
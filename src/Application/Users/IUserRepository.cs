using Domain.Entities.User;

namespace Application.Users
{
    public interface IUserRepository
    {
        void Add(IUser user);

        IUser? GetByEmail(string email);
    }
}
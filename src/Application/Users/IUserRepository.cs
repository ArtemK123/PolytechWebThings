using System.Collections.Generic;
using Domain.Entities.User;

namespace Application.Users
{
    public interface IUserRepository
    {
        void Add(IUser user);

        IReadOnlyCollection<IUser> GetAll();

        IUser? GetByEmail(string email);
    }
}
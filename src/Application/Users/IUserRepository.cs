using System.Threading.Tasks;
using Domain.Entities.User;

namespace Application.Users
{
    public interface IUserRepository
    {
        Task AddAsync(IUser user);

        Task<IUser?> GetByEmailAsync(string email);

        Task UpdateAsync(IUser user);
    }
}
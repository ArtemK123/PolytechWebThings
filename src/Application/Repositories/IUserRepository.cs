using System.Threading.Tasks;
using Domain.Entities.User;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(IUser user);

        Task<IUser?> GetByEmailAsync(string email);

        Task UpdateAsync(IUser user);
    }
}
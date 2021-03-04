using System.Linq;
using System.Threading.Tasks;
using Application.Users;
using Domain.Entities.Common;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace PolytechWebThings.Infrastructure.Database.Users
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IFactory<StoredUserCreationModel, IUser> storedUserFactory;

        public UserRepository(ApplicationDbContext dbContext, IFactory<StoredUserCreationModel, IUser> storedUserFactory)
        {
            this.dbContext = dbContext;
            this.storedUserFactory = storedUserFactory;
        }

        public async Task AddAsync(IUser user)
        {
            var databaseModel = new UserDatabaseModel
            {
                Id = user.Id,
                Email = user.Email,
                Password = user.Password,
                Role = user.Role,
                SessionToken = user.SessionToken
            };
            await dbContext.Users.AddAsync(databaseModel);
            await dbContext.SaveChangesAsync();
        }

        public async Task<IUser?> GetByEmailAsync(string email)
        {
            UserDatabaseModel? databaseModel = await dbContext.Users.Where(user => user.Email == email).FirstOrDefaultAsync();
            return databaseModel is not null
                ? storedUserFactory.Create(new StoredUserCreationModel
                {
                    Id = databaseModel.Id,
                    Email = databaseModel.Email,
                    Password = databaseModel.Password,
                    SessionToken = databaseModel.SessionToken,
                    Role = databaseModel.Role
                })
                : null;
        }
    }
}
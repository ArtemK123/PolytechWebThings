using System;
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
            UserDatabaseModel? databaseModel = await dbContext.Users.Where(user => user.Email == email).SingleOrDefaultAsync();
            return databaseModel is not null
                ? storedUserFactory.Create(Convert(databaseModel))
                : null;
        }

        public async Task UpdateAsync(IUser user)
        {
            UserDatabaseModel? databaseModel = await dbContext.Users.Where(databaseUser => databaseUser.Id == user.Id).SingleOrDefaultAsync();
            if (databaseModel is null)
            {
                throw new Exception("User to update is not found in the database");
            }

            databaseModel.Email = user.Email;
            databaseModel.Password = user.Password;
            databaseModel.Role = user.Role;
            databaseModel.SessionToken = user.SessionToken;

            await dbContext.SaveChangesAsync();
        }

        private StoredUserCreationModel Convert(UserDatabaseModel databaseModel)
            => new StoredUserCreationModel(
                id: databaseModel.Id ?? throw new NullReferenceException(),
                email: databaseModel.Email ?? throw new NullReferenceException(),
                password: databaseModel.Password ?? throw new NullReferenceException(),
                sessionToken: databaseModel.SessionToken,
                role: databaseModel.Role ?? throw new NullReferenceException());
    }
}
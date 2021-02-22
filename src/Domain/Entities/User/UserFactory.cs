using Domain.Entities.Common;
using Domain.Services;

namespace Domain.Entities.User
{
    internal class UserFactory : IFactory<NewUserCreationModel, IUser>
    {
        private readonly IGuidProvider guidProvider;

        public UserFactory(IGuidProvider guidProvider)
        {
            this.guidProvider = guidProvider;
        }

        public IUser Create(NewUserCreationModel creationModel)
        {
            return new User
            {
                Id = guidProvider.CreateGuid().ToString(),
                Email = creationModel.Email,
                Password = creationModel.Password,
                SessionToken = null,
                Role = creationModel.Role
            };
        }
    }
}
using Domain.Entities.Common;
using Domain.Services;

namespace Domain.Entities.User
{
    internal class UserFactory : IFactory<NewUserCreationModel, IUser>, IFactory<StoredUserCreationModel, IUser>
    {
        private readonly IGuidProvider guidProvider;

        public UserFactory(IGuidProvider guidProvider)
        {
            this.guidProvider = guidProvider;
        }

        public IUser Create(NewUserCreationModel creationModel)
        {
            return new User(
                id: guidProvider.CreateGuid().ToString(),
                email: creationModel.Email,
                password: creationModel.Password,
                sessionToken: null,
                role: creationModel.Role);
        }

        public IUser Create(StoredUserCreationModel creationModel)
        {
            return new User(
                id: creationModel.Id,
                email: creationModel.Email,
                password: creationModel.Password,
                sessionToken: creationModel.SessionToken,
                role: creationModel.Role);
        }
    }
}
using Domain.Entities.Common;
using Domain.Providers;

namespace Domain.Entities.Workspace
{
    internal class WorkspaceFactory : IFactory<NewWorkspaceCreationModel, IWorkspace>, IFactory<StoredWorkspaceCreationModel, IWorkspace>
    {
        private readonly IGuidProvider guidProvider;

        public WorkspaceFactory(IGuidProvider guidProvider)
        {
            this.guidProvider = guidProvider;
        }

        public IWorkspace Create(NewWorkspaceCreationModel creationModel)
            => new Workspace(
                id: guidProvider.CreateGuid().GetHashCode(),
                name: creationModel.Name,
                gatewayUrl: creationModel.GatewayUrl,
                accessToken: creationModel.AccessToken,
                userEmail: creationModel.UserEmail);

        public IWorkspace Create(StoredWorkspaceCreationModel creationModel)
            => new Workspace(
                id: creationModel.Id,
                name: creationModel.Name,
                gatewayUrl: creationModel.GatewayUrl,
                accessToken: creationModel.AccessToken,
                userEmail: creationModel.UserEmail);
    }
}
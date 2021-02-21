namespace Domain.Entities.Common
{
    public interface IFactory<in TCreationModel, out TEntity>
    {
        TEntity Create(TCreationModel creationModel);
    }
}
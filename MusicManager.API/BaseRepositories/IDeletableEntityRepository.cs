using MusicManager.API.Infrastructures.Models;

namespace MusicManager.API.BaseRepositories
{
    public interface IDeletableEntityRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        Task<IList<TEntity>> AllWithDeleted();
        Task<IList<TEntity>> AllAsNoTrackingWithDeleted();
        void HardDelete(TEntity entity);
        void Undelete(TEntity entity);
    }
}

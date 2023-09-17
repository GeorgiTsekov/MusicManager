using MusicManager.API.Common.Models;

namespace MusicManager.API.Common.Repositories
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

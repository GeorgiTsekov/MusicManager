using MusicManager.API.Common.Models;

namespace MusicManager.API.Common.Repositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        Task<IList<TEntity>> AllAsync();
        Task<IList<TEntity>> AllAsNoTrackingAsync();
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> ByIdAsync(TKey id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

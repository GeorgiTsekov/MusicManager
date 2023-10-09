using MusicManager.API.Data;
using MusicManager.API.Features.Users;
using MusicManager.Infrastructure.Models;

namespace MusicManager.API.BaseRepositories
{
    public class DeletableEntityRepository<TEntity, TKey> : EfRepository<TEntity, TKey>, IDeletableEntityRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public DeletableEntityRepository(MusicManagerDbContext db, IUserService userService) : base(db, userService)
        {
        }

        public override async Task<IList<TEntity>> AllAsync()
        {
            var entities = await base.AllAsync();
            return entities.Where(x => !x.IsDeleted).ToList();
        }

        public override async Task<IList<TEntity>> AllMineAsync()
        {
            var entities = await base.AllMineAsync();
            return entities.Where(x => !x.IsDeleted).ToList();
        }

        public override async Task<IList<TEntity>> AllAsNoTrackingAsync()
        {
            var entities = await base.AllAsNoTrackingAsync();
            return entities.Where(x => !x.IsDeleted).ToList();
        }

        public async Task<IList<TEntity>> AllWithDeleted() => await base.AllAsync();

        public async Task<IList<TEntity>> AllAsNoTrackingWithDeleted() => await base.AllAsNoTrackingAsync();

        public void HardDelete(TEntity entity)
        {
            base.Delete(entity);
        }

        public void Undelete(TEntity entity)
        {
            entity.IsDeleted = false;
            entity.DeletedOn = null;
            base.Update(entity);
        }

        public override void Delete(TEntity entity)
        {
            entity.DeletedOn = DateTime.Now;
            entity.IsDeleted = true;
            base.Update(entity);
        }

        public async override Task<TEntity> ByIdAsync(TKey id)
        {
            var entity = await base.ByIdAsync(id);
            if (entity.IsDeleted)
            {
                return null;
            }

            return entity;
        }
    }
}

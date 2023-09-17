using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Models;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;

namespace MusicManager.API.Repositories
{
    public class DeletableEntityRepository<TEntity, TKey> : EfRepository<TEntity, TKey>, IDeletableEntityRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public DeletableEntityRepository(MusicManagerDbContext db)
            : base(db)
        {
        }

        public virtual async Task<IList<TEntity>> AllAsync()
        {
            var entities = await this.DbSet.ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public virtual async Task<IList<TEntity>> AllAsNoTrackingAsync()
        {
            var entities = await this.DbSet.AsNoTracking().ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public async Task<IList<TEntity>> AllWithDeleted() => await this.DbSet.ToListAsync();

        public async Task<IList<TEntity>> AllAsNoTrackingWithDeleted() => await this.DbSet.AsNoTracking().ToListAsync();

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
    }
}

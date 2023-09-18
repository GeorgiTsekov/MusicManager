using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Models;
using MusicManager.API.Data;

namespace MusicManager.API.Common.Repositories
{
    public class DeletableEntityRepository<TEntity, TKey> : EfRepository<TEntity, TKey>, IDeletableEntityRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public DeletableEntityRepository(MusicManagerDbContext db)
            : base(db)
        {
        }

        public override async Task<IList<TEntity>> AllAsync()
        {
            var entities = await DbSet.ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public override async Task<IList<TEntity>> AllAsNoTrackingAsync()
        {
            var entities = await DbSet.AsNoTracking().ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public async Task<IList<TEntity>> AllWithDeleted() => await DbSet.ToListAsync();

        public async Task<IList<TEntity>> AllAsNoTrackingWithDeleted() => await DbSet.AsNoTracking().ToListAsync();

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

        public async override Task<TEntity> ByIdAsync(TKey id) => await DbSet.Where(x => !x.IsDeleted).FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}

using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Models;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;

namespace MusicManager.API.Repositories
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public EfRepository(MusicManagerDbContext db)
        {
            this.Context = db ?? throw new ArgumentNullException(nameof(db));
            this.DbSet = this.Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected MusicManagerDbContext Context { get; set; }

        public virtual async Task<IList<TEntity>> AllAsync() => await this.DbSet.ToListAsync();

        public virtual async Task<IList<TEntity>> AllAsNoTrackingAsync() => await this.DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            await this.DbSet.AddAsync(entity);
            await this.Context.SaveChangesAsync();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            var entry = this.Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            entity.ModifiedOn = DateTime.UtcNow;
            this.Context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        public virtual async Task<TEntity> ByIdAsync(TKey id) => await this.DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}

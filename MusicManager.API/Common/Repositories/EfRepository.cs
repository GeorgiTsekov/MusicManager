using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Models;
using MusicManager.API.Data;

namespace MusicManager.API.Common.Repositories
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseDeletableModel<TKey>
    {
        public EfRepository(MusicManagerDbContext db)
        {
            Context = db ?? throw new ArgumentNullException(nameof(db));
            DbSet = Context.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }

        protected MusicManagerDbContext Context { get; set; }

        public virtual async Task<IList<TEntity>> AllAsync() => await DbSet.ToListAsync();

        public virtual async Task<IList<TEntity>> AllAsNoTrackingAsync() => await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            await DbSet.AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            var entry = Context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            entity.ModifiedOn = DateTime.UtcNow;
            Context.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            Context.SaveChanges();
        }

        public virtual async Task<TEntity> ByIdAsync(TKey id) => await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}

using Microsoft.EntityFrameworkCore;
using MusicManager.API.Data;
using MusicManager.API.Features.Users;
using MusicManager.API.Infrastructures.Models;

namespace MusicManager.API.BaseRepositories
{
    public class EfRepository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey>
    {

        public EfRepository(MusicManagerDbContext db, IUserService userService)
        {
            DbContext = db ?? throw new ArgumentNullException(nameof(db));
            UserService = userService;
            DbSet = DbContext.Set<TEntity>();
        }

        protected DbSet<TEntity> DbSet { get; set; }
        protected MusicManagerDbContext DbContext { get; set; }
        public IUserService UserService { get; }

        public virtual async Task<IList<TEntity>> AllAsync() => await DbSet.ToListAsync();
        public virtual async Task<IList<TEntity>> AllMineAsync()
        {
            var entities = await DbSet.ToListAsync();

            var currentUserEmail = UserService.GetCurrentUserDetails().Result.Email;

            if (currentUserEmail == null)
            {
                return new List<TEntity>();
            }

            return entities.Where(x => x.CreatedBy == currentUserEmail).ToList();
        }

        public virtual async Task<IList<TEntity>> AllAsNoTrackingAsync() => await DbSet.AsNoTracking().ToListAsync();

        public virtual async Task<TEntity> CreateAsync(TEntity entity)
        {
            entity.CreatedOn = DateTime.Now;
            await DbSet.AddAsync(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public virtual void Update(TEntity entity)
        {
            var entry = DbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                DbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
            entity.ModifiedOn = DateTime.UtcNow;
            DbContext.SaveChanges();
        }

        public virtual void Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            DbContext.SaveChanges();
        }

        public virtual async Task<TEntity> ByIdAsync(TKey id) => await DbSet.FirstOrDefaultAsync(x => x.Id.Equals(id));
    }
}

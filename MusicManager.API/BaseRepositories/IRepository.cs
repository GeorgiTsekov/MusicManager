﻿using MusicManager.API.Infrastructures.Models;

namespace MusicManager.API.BaseRepositories
{
    public interface IRepository<TEntity, TKey>
        where TEntity : BaseModel<TKey>
    {
        Task<IList<TEntity>> AllAsync();
        Task<IList<TEntity>> AllMineAsync();
        Task<IList<TEntity>> AllAsNoTrackingAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> ByIdAsync(TKey id);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}

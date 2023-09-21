﻿using Microsoft.EntityFrameworkCore;
using MusicManager.API.Common.Repositories;
using MusicManager.API.Data;
using MusicManager.API.Models.Domain;

namespace MusicManager.API.Repositories
{
    public class BandRepository : DeletableEntityRepository<Band, int>
    {
        public BandRepository(MusicManagerDbContext db) : base(db)
        {
        }

        public async override Task<IList<Band>> AllAsync()
        {
            var entities = await DbSet.Include(x => x.Musicians).Include(x => x.Albums).ToListAsync();
            entities = entities.Where(x => !x.IsDeleted).ToList();
            return entities;
        }

        public override async Task<Band> ByIdAsync(int id)
        {
            return await base.DbSet.Include(x => x.Musicians).Include(x => x.Albums).FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }
    }
}

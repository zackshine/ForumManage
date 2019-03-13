using ForumManage.Data;
using ForumManage.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForumManage.Repository
{
    public class EFRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ForumContext _context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public EFRepository(ForumContext context)
        {
            _context = context;
            entities = context.Set<T>();
        }
        public List<T> GetAll()
        {
            return entities.Where(e=>e.IsDeleted == false)
                  .AsNoTracking()
                  .ToList();
        }

        public async Task<T> GetById(long id)
        {
            return await entities.Where(e => e.IsDeleted == false)
                     .AsNoTracking()
                     .SingleOrDefaultAsync(s => s.Id == id);
        }
        public async Task<T> Add(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
             entities.Add(entity);
            await _context.SaveChangesAsync();
            return await GetById(entity.Id);
        }

        public async Task<T> Update(long id,T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            await _context.SaveChangesAsync();
            return await GetById(entity.Id);
        }

        public async Task Delete(long id)
        {
            var entity = await GetById(id);
            entities.Remove(entity);
            await _context.SaveChangesAsync();       
        }
    }
}

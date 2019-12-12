using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vKurzuCore.Models;

namespace vKurzuCore.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseModel
    {
        protected readonly DbContext Context;
        public Repository(DbContext context)
        {
            Context = context;
        }

        public virtual Task<TEntity> FindByIdAsync(int id)
        {
            return Context.Set<TEntity>()
                .SingleOrDefaultAsync(x => x.Id == id && !x.Deleted);
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await Context.Set<TEntity>()
                .Where(x => !x.Deleted).OrderByDescending(x=>x.Position).ToListAsync();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate);
        }

        public void Add(TEntity entity)
        {
            entity.Created = DateTime.Now;
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(ent => {
                ent.Created = DateTime.Now;               
                Context.Set<TEntity>().Add(ent);
            });

        }

        public void Remove(TEntity entity)
        {
            var entityToRemove = Context.Set<TEntity>().Find(entity.Id);
            entityToRemove.Deleted = true;
            entityToRemove.Changed = DateTime.Now;
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            entities.ToList().ForEach(entity => {
                entity.Deleted = true;
                entity.Changed = DateTime.Now; 
            });
        }
    }
}

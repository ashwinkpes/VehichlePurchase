using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Vega.Persistence;

namespace Vega.core.GenericRepositry
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly VegaDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;
        public Repository(VegaDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            return entities.AsEnumerable();
        }
        // public T Get(long id)
        // {
        //     return entities.SingleOrDefault(s => s. == id);
        // }
        public void Insert(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            context.SaveChanges();
        }
        public void Update(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            context.SaveChanges();
        }
        public void Delete(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            context.SaveChanges();
        }
    }
}

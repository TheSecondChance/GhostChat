using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GhostChat.Data
{
    public class Repository<Type> : IRepository<Type> where Type : class
    {
        private readonly ApplicationContext dbContext;
        private DbSet<Type> entities;

        public Repository(ApplicationContext dbContext)
        {
            this.dbContext = dbContext;
            entities = dbContext.Set<Type>();
        }

        public List<Type> GetAll()
        {
            return entities.ToList();
        }

        public Type GetById(object id)
        {
            return entities.Find(id);
        }

        public void Add(Type entity)
        {
            entities.Add(entity);
            dbContext.SaveChanges();
        }

        public void Update(Type entity)
        {
            entities.Update(entity);
            dbContext.SaveChanges();
        }

        public void Put(Type entity)
        {
            var entry = dbContext.Entry(entity);
            switch (entry.State)
            {
                case EntityState.Detached:
                    Update(entity);
                    break;
                case EntityState.Modified:
                    Add(entity);
                    break;
                case EntityState.Added:
                    Add(entity);
                    break;
            }
        }

        public void Remove(Type entity)
        {
            entities.Remove(entity);
            dbContext.SaveChanges();
        }
    }
}

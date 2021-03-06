using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Domain.Modelo;
using SharedKernel.Domain.Repositories;

namespace SharedKernel.EntityFramework.Repositories
{
    public class Repository<T> : QueryRepository<T>, IRepository<T> where T : EntityBase
    {
        public Repository(DbContext context) : base(context)
        {
        }

        public void Insert(T entity)
        {
            Entities.Add(entity);
        }
        
        public void Update(T entity)
        {
            Context.Entry<T>(entity).State = EntityState.Modified;            
        }

        public void Save(T entity)
        {
            if(entity.Id == 0)
                Insert(entity);
            else
                Update(entity);
        }

        public void Delete(T entity)
        {
            Entities.Remove(entity);
        }

        public int RunCommand(string command, params object[] poParams)
        {
            return Context.Database.ExecuteSqlCommand(command, poParams);
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Insert(IList<T> entities)
        {
            Entities.AddRange(entities);
        }
    }
}
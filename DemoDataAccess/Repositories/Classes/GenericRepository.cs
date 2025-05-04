using DemoDataAccess.Contexts;
using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Models.Shared;
using DemoDataAccess.Repositories.interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.Classes
{
    public class GenericRepository<TEntity>(ApplicationDbContext _dbContext) : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        //CRUD Opreations:

        // GetAll
        public IEnumerable<TEntity>GetAll(bool withTracking = false)
        {
            if (withTracking)
                return _dbContext.Set<TEntity>().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
        }
        //GetByid
        public TEntity? GetBYId(int id) => _dbContext.Set<TEntity>().Find(id);

        //Add
        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }
        //Update
        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity); //Update Locally.
        }
        //Delete
        public void Remove(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> Predicate)
        {
            return _dbContext.Set<TEntity>()
                .Where(Predicate)
                .ToList();
        }
    }
}

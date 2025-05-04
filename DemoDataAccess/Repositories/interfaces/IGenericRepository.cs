using DemoDataAccess.Models.DepartmentModel;
using DemoDataAccess.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DemoDataAccess.Repositories.interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        void Add(TEntity entity);
        IEnumerable<TEntity> GetAll(bool withTracking = false);
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity,bool>> Predicate);
        TEntity? GetBYId(int id);
        void Remove(TEntity entity); 
        void Update(TEntity entity);

    }
}

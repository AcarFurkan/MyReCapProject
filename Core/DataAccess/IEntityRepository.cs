using Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T: class,IEntity,new()
    {
        T GetById(Expression<Func<T,bool>> filter = null);
        List<T> GetAll(Expression<Func<T,bool>> filter = null);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        List<T> GetAllByCategory(Expression<Func<T, bool>> filter = null);
    }
}

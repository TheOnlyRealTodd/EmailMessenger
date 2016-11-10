using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ContactFormWithEmail.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IList<TEntity> GetAll();

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        IList<TEntity> FindAny(Expression<Func<TEntity, bool>> predicate);

        void PersistChanges();

    }
}

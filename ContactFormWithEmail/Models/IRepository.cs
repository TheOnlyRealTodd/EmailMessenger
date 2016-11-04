using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ContactFormWithEmail.Models
{
    public interface IRepository<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IList<TEntity> GetAll();

        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);

        void PersistChanges();

    }
}

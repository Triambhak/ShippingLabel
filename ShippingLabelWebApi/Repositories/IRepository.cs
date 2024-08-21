using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ShippingLabelWebApi.Repositories
{
    public interface IRepository<T> where T : class
    {
        #region syncmethods
        void Add(T entity);
        List<T> Add(List<T> listofEntitites);
        T AddReturn(T entity);
        void Update(T entity);
        T UpdateReturn(T entity);
        void Update(List<T> listofEntitites);
        void Delete(T entity);
        void Delete(List<T> listofEntitites);
        void Delete(Expression<Func<T, bool>> where);
        T GetById(int id);
        T Get(Expression<Func<T, bool>> where);
        List<T> GetAll();
        IList<T> GetMany(Expression<Func<T, bool>> where);
        IList<T> GetPage(Expression<Func<T, bool>> where, int PageSize);

        int GetCount();
        // DbRawSqlQuery<TEntity> SQLQuery<TEntity>(string sql, params object[] parameters);

        void SaveChanges();
        void Delete(int id);
        List<T> GetMany(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, params Expression<Func<T, object>>[] includes);
        #endregion

    }
    public interface IRepository
    {
    }
}

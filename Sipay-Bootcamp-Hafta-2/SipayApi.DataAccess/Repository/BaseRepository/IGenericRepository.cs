using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SipayApi.DataAccess.Repository.Base
{
    public interface IGenericRepository<T> where T : class
    {

        void Save();
        List<T> GetAll();
        T GetById(int id);
        void Insert(T t);
        void Update(T t);
        void Delete(T t);
        void DeleteById(int id);
        IQueryable<T> GetAllAsQueryable();
        List<T> GetByParameter(Expression<Func<T, bool>> expression);
    }
}

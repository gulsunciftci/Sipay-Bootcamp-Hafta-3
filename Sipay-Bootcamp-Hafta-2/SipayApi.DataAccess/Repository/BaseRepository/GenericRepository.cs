using Microsoft.EntityFrameworkCore;
using SipayApi.Base.BaseModel;
using SipayApi.DataAccess.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SipayApi.DataAccess.Repository.Base
{
    //GenericRepository IGenericRepositoryden türer(Kalıtım alır)
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
    {
        private readonly SimDbContext _simDbContext;
        public GenericRepository(SimDbContext simDbContext)
        {
            _simDbContext = simDbContext;
        }

        public void Save()
        {
            _simDbContext.SaveChanges();
        }

        public void Delete(T t)
        {
            _simDbContext.Set<T>().Remove(t);
            Save();
        }

        public void DeleteById(int id)
        {
            var t = _simDbContext.Set<T>().Find(id);
            Delete(t);
            Save();
        }

        public  List<T> GetAll()
        {
            return _simDbContext.Set<T>().ToList();
        }

        public IQueryable<T> GetAllAsQueryable()
        {
            return _simDbContext.Set<T>().AsQueryable();
        }


        public List<T> GetByParameter(Expression<Func<T, bool>> expression)
        {
            return _simDbContext.Set<T>().Where(expression).ToList();
        }


        public T GetById(int id)
        {
            var t = _simDbContext.Set<T>().Find(id);
            return t;
        }

        public void Insert(T t)
        {
            t.InsertDate = DateTime.Now;
            t.InsertUser = "simadmin@pay.com";
            _simDbContext.Set<T>().Add(t);
            Save();
        }

        public void Update(T t)
        {
            _simDbContext.Set<T>().Update(t);
            Save();
        }
    }
}

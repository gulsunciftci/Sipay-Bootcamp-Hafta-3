using SipayApi.Base.BaseModel;
using SipayApi.DataAccess.ApplicationDbContext;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Unitofw
{
    public class UnitOfWork : IUnitOfWork //implement ettim
    {
        private readonly SimDbContext _dbContext;

        public UnitOfWork(SimDbContext dbContext)
        {
            _dbContext = dbContext;
            AccountRepository = new GenericRepository<Account>(dbContext);
            CustomerRepository = new GenericRepository<Customer>(dbContext);
            TransactionRepository = new GenericRepository<Transaction>(dbContext);
        }

        public IGenericRepository<T> Repository<T>() where T : BaseModel
        {
            return new GenericRepository<T>(_dbContext); 
        }
        public IGenericRepository<Account> AccountRepository { get; private set; }

        public IGenericRepository<Customer> CustomerRepository { get; private set; }

        public IGenericRepository<Transaction> TransactionRepository { get; private set; }

        public void Complete()
        {
            _dbContext.SaveChanges();
        }
        public void CompleteWithTransaction()
        {
            using(var dbTransaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    _dbContext.SaveChanges();
                    dbTransaction.Commit();
                }
                catch(Exception ex)
                {
                    dbTransaction.Rollback();
                }
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using SipayApi.DataAccess.ApplicationDbContext;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Repository.AccountRepository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        private readonly SimDbContext _dbContext;
        public AccountRepository(SimDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public List<Account> GetAll()
        {
            //return _dbContext.Set<Account>().ToList();

            return _dbContext.Set<Account>().Include(x => x.Transactions).ToList();
        }
        public Account GetById(int id)
        {
            //return _dbContext.Set<Account>().FirstOrDefault(x=>x.AccountNumber==id);

            return _dbContext.Set<Account>().Include(x => x.Transactions).FirstOrDefault(x=>x.AccountNumber==id);
        }
    }
}

using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Repository.AccountRepository
{
    public interface IAccountRepository:IGenericRepository<Account>
    {
        List<Account> GetAll();
        Account GetById(int id);
    }
}

using Azure.Core;
using Azure;
using SipayApi.Base.Response;
using SipayApi.Business.Generic;
using SipayApi.DataAccess.Domain;
using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SipayApi.Base.Transaction;

namespace SipayApi.Business.AccountS
{
    public interface IAccountService : IGenericService<Account, AccountRequest, AccountResponse>
    {
        public int GetNextAccountNumber();
        ApiResponse Balance(int accountNumber, decimal amount, TransactionDirection direction);
    }
}
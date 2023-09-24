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

namespace SipayApi.Business.CustomerS
{
    public interface ITransactionService : IGenericService<Customer, CustomerRequest, CustomerResponse>
    {
        public int GetNextCustomerNumber();
    }
}
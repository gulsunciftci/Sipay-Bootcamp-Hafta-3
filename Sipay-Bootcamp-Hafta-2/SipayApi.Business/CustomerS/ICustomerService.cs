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
    public interface ICustomerService : IGenericService<Customer, CustomerRequest, CustomerResponse>
    {

    }
}
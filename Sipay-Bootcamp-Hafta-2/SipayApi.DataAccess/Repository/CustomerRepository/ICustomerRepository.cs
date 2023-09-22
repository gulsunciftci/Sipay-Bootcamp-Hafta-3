using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Repository.CustomerRepository
{
    public interface ICustomerRepository:IGenericRepository<Customer>
    {

        List<Customer> GetAll();
        Customer GetById(int id);

    }
}

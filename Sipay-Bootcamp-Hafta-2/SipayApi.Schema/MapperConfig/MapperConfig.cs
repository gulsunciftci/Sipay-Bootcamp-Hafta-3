using AutoMapper;
using SipayApi.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            //Nereye mapleyeceğimizi belirtiyoruz
            //Post ve put apileri için geçerli
            CreateMap<CustomerRequest, Customer>();
            CreateMap<Customer, CustomerResponse>();

            CreateMap<AccountRequest, Account>();
            CreateMap<Account, AccountResponse>();


            CreateMap<TransactionRequest, Transaction>();
            CreateMap<Transaction, TransactionResponse>();

        }
    }
}

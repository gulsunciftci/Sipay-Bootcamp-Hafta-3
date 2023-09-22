﻿using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Unitofw
{
    public interface IUnitOfWork
    {
        void Complete();
        IGenericRepository<Account> AccountRepository { get; }
        IGenericRepository<Customer> CustomerRepository { get; }
        IGenericRepository<Transaction> TransactionRepository { get; }

    }
}

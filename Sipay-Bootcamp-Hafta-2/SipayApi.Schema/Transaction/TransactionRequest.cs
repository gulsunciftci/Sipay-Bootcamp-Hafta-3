﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema
{
    public class TransactionRequest
    {
        public int AccountNumber { get; set; }
        public decimal CreditAmount { get; set; }
        public decimal DebitAmount { get; set; }
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceNumber { get; set; }
    }
}

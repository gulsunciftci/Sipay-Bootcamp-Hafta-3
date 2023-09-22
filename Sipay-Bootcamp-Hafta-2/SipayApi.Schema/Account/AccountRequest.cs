using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema
{
    public class AccountRequest
    {
        public int CustomerNumber { get; set; }
        public int AccountNumber { get; set; }
        public string Name { get; set; }
        public string CurrencyCode { get; set; }
    }
}

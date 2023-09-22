using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Schema
{
    public class AccountResponse
    {
        public int CustomerNumber { get; set; }
        public int AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public string Name { get; set; }

        //OpenDate sistemde gözükmesin istediğimiz için bunu kaldırdık
        //public DateTime OpenDate { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsActive { get; set; }
        public virtual List<TransactionResponse> Transactions { get; set; }

    }
}

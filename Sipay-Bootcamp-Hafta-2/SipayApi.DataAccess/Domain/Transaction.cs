using SipayApi.Base.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Domain
{
    [Table("Transaction", Schema="dbo")]
    public class Transaction:IdBaseModel
    {
        //Account nesnesini verdim ve foreign key ile bağladım, 1 tane account var(n-1 ilişkisi)
        // Yani bir transaction'ın bir account'u vardır
        public int AccountNumber { get; set; }
        public virtual Account Account { get; set; }

        public decimal CreditAmount { get; set; }// -
        public decimal DebitAmount { get; set; }// +
        public string Description { get; set; }
        public DateTime TransactionDate { get; set; }
        public int AccontNumber { get; set; }
        public string ReferenceNumber { get; set; }

    }
}

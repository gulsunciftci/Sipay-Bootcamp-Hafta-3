using SipayApi.Base.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Domain
{
    [Table("Account", Schema = "dbo")]
    public class Account:BaseModel //Base modeli extend ettim
    {
        //Primary Key
        public int AccountNumber { get; set; }
        //n-ı ilişki kapsamında foreign key olarak CustomerId yi ekledim
        //yani bir hesap bir müşteriye ait olabilir
        public int CustomerNumber { get; set; }
        public virtual Customer Customer { get; set; } 
        public decimal Balance { get; set; }
        public string Name { get; set; }
        public DateTime OpenDate { get; set; }
        public string CurrencyCode { get; set; } 
        public bool IsActive { get; set; }

        // 1-n ilişkisi kapsamında Transaction listesi ekledim 
        // Yani bir account'ın birden çok transaction'ı vardır
        public virtual List<Transaction> Transactions { get; set; }

    }
}

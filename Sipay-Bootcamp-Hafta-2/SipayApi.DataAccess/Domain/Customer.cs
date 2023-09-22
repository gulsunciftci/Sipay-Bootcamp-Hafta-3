using SipayApi.Base.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Domain
{
    [Table("Customer", Schema = "dbo")]
    public class Customer:BaseModel
    {
        //PK
        public int CustomerNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string Address { get; set; }

        //1-n ilişkisi
        // Yani bir customer'ın birden çok account'u olabilir
        public virtual List<Account> Accounts { get; set; }

    }
}

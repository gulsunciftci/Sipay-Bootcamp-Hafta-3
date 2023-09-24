using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Base.ReferenceNumber
{
    public class ReferenceNumberGenerator
    {
        public static string Get()
        {
            return Guid.NewGuid().ToString().ToUpper().Replace("-", "").Substring(0, 15);
        }
    }
}

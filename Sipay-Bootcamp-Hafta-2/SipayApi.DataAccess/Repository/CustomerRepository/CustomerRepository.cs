using Microsoft.EntityFrameworkCore;
using SipayApi.DataAccess.ApplicationDbContext;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.Repository.CustomerRepository
{
    public class CustomerRepository : GenericRepository<Customer>,ICustomerRepository
    {
        private readonly SimDbContext _dbContext;
        public CustomerRepository(SimDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public  List<Customer> GetAll()
        {
            //return _dbContext.Set<Customer>().ToList();


            //Customerların Accountlarıyla birlikte Transactionsları getirmelerini söylüyoruz.
            ///<summary>
            ///# NOT:
            ///*Bir tablodaki bilgiler dışında diğer tablodaki bilgilere ulaşmak için include kullanırız. (sadece ilk tabloya geçiş)(include=join(foreignkey çalıştırıyor))
            ///*ilk tabloya geçişten sonra ikinci tabloya geçiş yapmak için tekrar include kullanılamaz onun yerine theninclude kullanılır.(ikinci tabloya geçiş) 
            /// </summary>
            return _dbContext.Set<Customer>().Include(x => x.Accounts).ThenInclude(x => x.Transactions).ToList();
        }
        public Customer GetById(int id)
        {
            //return _dbContext.Set<Customer>().FirstOrDefault(x=>x.CustomerNumber==id);

            ///<summary>
            ///# NOT:
            ///*Bir tablodaki bilgiler dışında diğer tablodaki bilgilere ulaşmak için include kullanırız. (sadece ilk tabloya geçiş)(include=join(foreignkey çalıştırıyor))
            ///*ilk tabloya geçişten sonra ikinci tabloya geçiş yapmak için tekrar include kullanılamaz onun yerine theninclude kullanılır.(ikinci tabloya geçiş) 
            ///Thenincludedan sonra tekrar include kullanılabilir
            /// </summary>
            return _dbContext.Set<Customer>().Include(x => x.Accounts).ThenInclude(x => x.Transactions).FirstOrDefault(x => x.CustomerNumber == id);
        }

    }
}

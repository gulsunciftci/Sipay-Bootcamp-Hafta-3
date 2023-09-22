using Microsoft.EntityFrameworkCore;
using SipayApi.DataAccess.Config;
using SipayApi.DataAccess.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.DataAccess.ApplicationDbContext
{
    public class SimDbContext:DbContext
    {
        public SimDbContext(DbContextOptions<SimDbContext> options):base(options)
        {

        }

        //DbSet
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }




        //Model create ederken kullanılır
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}

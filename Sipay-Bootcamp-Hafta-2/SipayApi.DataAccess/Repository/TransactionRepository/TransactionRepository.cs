using SipayApi.DataAccess.ApplicationDbContext;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.Base;
using System.Linq.Expressions;

namespace SipayApi.DataAccess.Repository.TransactionRepository
    {
    public class TransactionRepository : GenericRepository<Transaction>, ITransactionRepository
    {
        //implement edilirken kullanılır
        private readonly SimDbContext _dbContext;
        public TransactionRepository(SimDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        //implement edildi
        public List<Transaction> GetByReference(string reference)
        {
            return _dbContext.Set<Transaction>().Where(x => x.ReferenceNumber == reference).ToList();
        }
    }
}



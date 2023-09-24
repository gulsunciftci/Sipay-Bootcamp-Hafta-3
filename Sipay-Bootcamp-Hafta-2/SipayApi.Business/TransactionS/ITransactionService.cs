using Azure.Core;
using Azure;
using SipayApi.Base.Response;
using SipayApi.Business.Generic;
using SipayApi.DataAccess.Domain;
using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Business.TransactionS
{
    public interface ITransactionService
    {
        ApiResponse<List<TransactionResponse>> GetAll();
        ApiResponse<TransactionResponse> GetById(int id);
        ApiResponse<List<TransactionResponse>> GetByReference(string referenceNumber);

        ApiResponse<TransferResponse> Transfer(TransferRequest request);
        ApiResponse<TransferResponse> Deposit(CashRequest request);
        ApiResponse<TransferResponse> Withdraw(CashRequest request);
    }
}
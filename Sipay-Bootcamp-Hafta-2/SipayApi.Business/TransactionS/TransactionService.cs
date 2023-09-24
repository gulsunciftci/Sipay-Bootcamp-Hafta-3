using AutoMapper;
using Microsoft.AspNetCore.Http.Metadata;
using Serilog;
using SipayApi.Base.ReferenceNumber;
using SipayApi.Base.Response;
using SipayApi.Base.Transaction;
using SipayApi.Business.AccountS;
using SipayApi.Business.Generic;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SipayApi.Business.TransactionS
{
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAccountService _accountService;

        public TransactionService(IMapper mapper, IUnitOfWork unitOfWork,IAccountService accountService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public ApiResponse<List<TransactionResponse>> GetAll()
        {
            try
            {
                var entityList = _unitOfWork.TransactionRepository.GetAll();
                var mapped = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
                return new ApiResponse<List<TransactionResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "TransactionService.GetAll");
                return new ApiResponse<List<TransactionResponse>>(ex.Message);
            }
        }

        public ApiResponse<TransactionResponse> GetById(int id)
        {
            try
            {
                var entity = _unitOfWork.TransactionRepository.GetById(id);
                var mapped = _mapper.Map<Transaction, TransactionResponse>(entity);
                return new ApiResponse<TransactionResponse>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "TransactionService.GetById");
                return new ApiResponse<TransactionResponse>(ex.Message);
            }
        }

        public ApiResponse<List<TransactionResponse>> GetByReference(string referenceNumber)
        {
            try
            {
                var entityList = _unitOfWork.TransactionRepository.where(x=>x.ReferenceNumber==referenceNumber).ToList();
                var mapped = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
                return new ApiResponse<List<TransactionResponse>>(mapped);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "TransactionService.GetByReference");
                return new ApiResponse<List<TransactionResponse>>(ex.Message);
            }
        }
        //virman
        public ApiResponse<TransferResponse> Transfer(TransferRequest request)
        {

            if (request == null)
            {
                return new ApiResponse<TransferResponse> ("Invalid Request");
            }

           if (request.FromAccountNumber==request.ToAccountNumber)
           {
                return new ApiResponse<TransferResponse> ("Invalid Accounts");
           }
           if (request.Amount <= 0)
           {
                return new ApiResponse<TransferResponse> ("Invalid Amount");
           }

          ApiResponse <AccountResponse> accountResponseFrom = _accountService.GetById(request.FromAccountNumber);
          if (accountResponseFrom.Success || accountResponseFrom.Response is null)
          {
                return new ApiResponse<TransferResponse> (accountResponseFrom.Message);
           }


         AccountResponse accountFrom=accountResponseFrom.Response;
         ApiResponse balanceResponseFrom = _accountService.Balance(accountFrom.AccountNumber, request.Amount, TransactionDirection.Withdraw);
         if (balanceResponseFrom.Success)
         {
                return new ApiResponse<TransferResponse>(balanceResponseFrom.Message);
         }

         ApiResponse<AccountResponse> accountResponseTo = _accountService.GetById(request.FromAccountNumber);
         if (accountResponseTo.Success || accountResponseTo.Response is null)
         {
                return new ApiResponse<TransferResponse>(accountResponseTo.Message);
         }


         AccountResponse accountTo = accountResponseFrom.Response;
         ApiResponse balanceResponseTo = _accountService.Balance(accountTo.AccountNumber, request.Amount, TransactionDirection.Deposit);
         if (balanceResponseTo.Success)
         {
                return new ApiResponse<TransferResponse>(balanceResponseTo.Message);
         }

         if (accountFrom.CustomerNumber == accountTo.CustomerNumber)
         {
                return new ApiResponse<TransferResponse>("Invalid Accounts");
         }

         string referenceNumber = ReferenceNumberGenerator.Get();



            Transaction transactionTo = new();
            transactionTo.ReferenceNumber = referenceNumber;
            transactionTo.AccountNumber = accountTo.AccountNumber;
            transactionTo.TransactionDate = DateTime.UtcNow;
            transactionTo.Description = request.Description;
            transactionTo.DebitAmount = request.Amount;

            _unitOfWork.TransactionRepository.Insert(transactionTo);


            Transaction transactionFrom = new();
         transactionFrom.ReferenceNumber = referenceNumber;
         transactionFrom.AccountNumber = accountFrom.AccountNumber;
         transactionFrom.TransactionDate = DateTime.UtcNow;
         transactionFrom.Description = request.Description;
         transactionFrom.DebitAmount = request.Amount;

         _unitOfWork.TransactionRepository.Insert(transactionFrom);
         _unitOfWork.Complete();



            var response = new TransferResponse()
            {
                ReferenceNumber = referenceNumber
            };

            return new ApiResponse<TransferResponse>(response);

        }


        public ApiResponse<TransferResponse> Deposit(CashRequest request)
        {
            if (request == null)
            {
                return new ApiResponse<TransferResponse>("Invalid Request");
            }
            var accountResponse = _accountService.GetById(request.AccountNumber);
            if(!accountResponse.Success || accountResponse.Response is null)
            {
                return new ApiResponse<TransferResponse>(accountResponse.Message);
            }
            AccountResponse account = accountResponse.Response;
            ApiResponse balanceResponse = _accountService.Balance(account.AccountNumber, request.Amount, TransactionDirection.Deposit);
            if (balanceResponse.Success)
            {
                return new ApiResponse<TransferResponse>(balanceResponse.Message);
            }

            string referenceNumber = ReferenceNumberGenerator.Get();
          

            

            Transaction transaction = new();
            transaction.ReferenceNumber = referenceNumber;
            transaction.AccountNumber = account.AccountNumber;
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.Description = request.Description;
            transaction.DebitAmount = request.Amount;

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.Complete();



            var response = new TransferResponse()
            {
                ReferenceNumber = referenceNumber
            };

            return new ApiResponse<TransferResponse>(response);
        }
        public ApiResponse<TransferResponse> Withdraw(CashRequest request)
        {
            if (request == null)
            {
                return new ApiResponse<TransferResponse>("Invalid Request");
            }
            var accountResponse = _accountService.GetById(request.AccountNumber);
            if (!accountResponse.Success || accountResponse.Response is null)
            {
                return new ApiResponse<TransferResponse>(accountResponse.Message);
            }
            AccountResponse account = accountResponse.Response;
            ApiResponse balanceResponse = _accountService.Balance(account.AccountNumber, request.Amount, TransactionDirection.Withdraw);
            if (balanceResponse.Success)
            {
                return new ApiResponse<TransferResponse>(balanceResponse.Message);
            }

            string referenceNumber = ReferenceNumberGenerator.Get();




            Transaction transaction = new();
            transaction.ReferenceNumber = referenceNumber;
            transaction.AccountNumber = account.AccountNumber;
            transaction.TransactionDate = DateTime.UtcNow;
            transaction.Description = request.Description;
            transaction.DebitAmount = request.Amount;

            _unitOfWork.TransactionRepository.Insert(transaction);
            _unitOfWork.Complete();



            var response = new TransferResponse()
            {
                ReferenceNumber = referenceNumber
            };

            return new ApiResponse<TransferResponse>(response);
        }
    }
}

using AutoMapper;
using SipayApi.Base.Response;
using SipayApi.Base.Transaction;
using SipayApi.Business.Generic;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Business.AccountS
{
    public class AccountService : IGenericService<Account, AccountRequest, AccountResponse>, IAccountService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public AccountService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ApiResponse Balance(int accountNumber, decimal amount, TransactionDirection direction)
        {

            if (accountNumber == 0)
            {
                return new ApiResponse("Invalid Account");
            }
            var account = _unitOfWork.AccountRepository.GetById(accountNumber); 
            if (account is null)
            {
                return new ApiResponse("Invalid Account");
            }
            decimal balance=account.Balance;
            if (direction == TransactionDirection.Deposit)
            {
                    balance += amount;
            }
            if (direction== TransactionDirection.Withdraw)
            {
                if (account.Balance<amount)
                {
                    return new ApiResponse("insufficient balance");
                }

                balance-=amount;
            }

            account.Balance = balance;
            _unitOfWork.AccountRepository.Update(account);
            _unitOfWork.Complete();

            return new ApiResponse();
        }

        public ApiResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<List<AccountResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApiResponse<AccountResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int GetNextAccountNumber()
        {
            int accountNumber = 10000;
            var lastaccountNumber = _unitOfWork.AccountRepository.GetAllAsQueryable().OrderByDescending(X => X.CustomerNumber).FirstOrDefault();
            if (lastaccountNumber == null)
            {
                return accountNumber;
            }
            return lastaccountNumber.CustomerNumber + 1;
        }

        public ApiResponse Insert(AccountRequest request)
        {
            request.AccountNumber = GetNextAccountNumber();
            return Insert(request);
        }

        public ApiResponse Update(int id, AccountRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

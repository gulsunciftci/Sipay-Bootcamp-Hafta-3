using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository;
using SipayApi.DataAccess.Repository.AccountRepository;
using SipayApi.Schema;
using System.Collections.Generic;
using System.Transactions;
using Transaction = SipayApi.DataAccess.Domain.Transaction;

namespace SipayApi.Service.Controllers
{
    [ApiController]
    [Route("Sipay/api/[controller]/[action]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _repository;
        private readonly IMapper _mapper;
        public TransactionController(ITransactionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ApiResponse<List<TransactionResponse>> GetAll()
        {
            var list = _repository.GetAll();
            var mapper = _mapper.Map<List<Transaction>, List<TransactionResponse>>(list);
            return new ApiResponse<List<TransactionResponse>>(mapper);
        }

        [HttpGet("{id}")]
        public ApiResponse<TransactionResponse> Get(int id) //id'ye göre arama yapar
        {
            var entity = _repository.GetById(id);
            var mapper = _mapper.Map<Transaction, TransactionResponse>(entity);
            return new ApiResponse<TransactionResponse>(mapper);
        }

        [HttpGet("{GetByReference}")]
        public ApiResponse<List<TransactionResponse>> GetByReference(string referenceNumber)  //referans numarasına göre arama yapar
        {
            var entityList = _repository.GetByReference(referenceNumber);
            var mapper = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapper);
        }


        [HttpPost]
        public ApiResponse Post([FromBody] TransactionRequest request) //Yeni bir student eklendi
        {
            var mapper = _mapper.Map<TransactionRequest, Transaction>(request);
            _repository.Insert(mapper);
            _repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] TransactionRequest request) //id si verilen student bulunuyorsa güncelleme işlemi yapıldı
        {
            var entity = _mapper.Map<TransactionRequest, Transaction>(request);
            _repository.Insert(entity);
            return new ApiResponse();
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id) //id si verilen Stundent silindi
        {
            _repository.DeleteById(id);
            _repository.Save();
            return new ApiResponse();
        }


    }
}

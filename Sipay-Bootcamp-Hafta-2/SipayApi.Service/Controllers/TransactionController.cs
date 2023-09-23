using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository;
using SipayApi.DataAccess.Unitofw;
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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TransactionController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ApiResponse<List<TransactionResponse>> GetAll()
        {
            var list = _unitOfWork.TransactionRepository.GetAll();
            var mapper = _mapper.Map<List<Transaction>, List<TransactionResponse>>(list);
            return new ApiResponse<List<TransactionResponse>>(mapper);
        }

        [HttpGet("{id}")]
        public ApiResponse<TransactionResponse> Get(int id) //id'ye göre arama yapar
        {
            var entity = _unitOfWork.TransactionRepository.GetById(id);
            var mapper = _mapper.Map<Transaction, TransactionResponse>(entity);
            return new ApiResponse<TransactionResponse>(mapper);
        }

        [HttpGet("{GetByReference}")]
        public ApiResponse<List<TransactionResponse>> GetByReference([FromQuery]string referenceNumber)  //referans numarasına göre arama yapar
        {
            var entityList = _unitOfWork.TransactionRepository.where(x=>x.ReferenceNumber==referenceNumber).ToList();
            var mapper = _mapper.Map<List<Transaction>, List<TransactionResponse>>(entityList);
            return new ApiResponse<List<TransactionResponse>>(mapper);
        }


        [HttpPost]
        public ApiResponse Post([FromBody] TransactionRequest request) //Yeni bir student eklendi
        {
            var mapper = _mapper.Map<TransactionRequest, Transaction>(request);
            _unitOfWork.TransactionRepository.Insert(mapper);
            _unitOfWork.TransactionRepository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] TransactionRequest request) //id si verilen student bulunuyorsa güncelleme işlemi yapıldı
        {
            var entity = _mapper.Map<TransactionRequest, Transaction>(request);
            _unitOfWork.TransactionRepository.Insert(entity);
            return new ApiResponse();
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id) //id si verilen Stundent silindi
        {
            _unitOfWork.TransactionRepository.DeleteById(id);
            _unitOfWork.TransactionRepository.Save();
            return new ApiResponse();
        }


    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;
using System.Collections.Generic;

namespace SipayApi.Service.Controllers
{
    [Route("Sipay/api/[controller]/[action]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CustomerController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ApiResponse<List<CustomerResponse>> GetAll()
        {
            var list = _unitOfWork.CustomerRepository.GetAll();
            //list customerı, list customerresponse maplemek istediğimizi söylemiş oluyoruz
            var mapper = _mapper.Map<List<Customer>, List<CustomerResponse>>(list);
            return new ApiResponse<List<CustomerResponse>>(mapper);
        }

        [HttpGet("{id}")]
        public ApiResponse<CustomerResponse> Get(int id) //id'ye göre arama yapar
        {
            var entity = _unitOfWork.CustomerRepository.GetById(id);
            var mapper = _mapper.Map<Customer, CustomerResponse>(entity);
            return new ApiResponse<CustomerResponse>(mapper);
        }


        [HttpPost]
        public ApiResponse Post([FromBody] CustomerRequest request) //Yeni bir student eklendi
        {
            var mapper = _mapper.Map<CustomerRequest, Customer>(request);
            mapper.IsActive = true;
            _unitOfWork.CustomerRepository.Insert(mapper);
            _unitOfWork.CustomerRepository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] CustomerRequest request) //id si verilen student bulunuyorsa güncelleme işlemi yapıldı
        {
            var mapper = _mapper.Map<CustomerRequest, Customer>(request);
            mapper.IsActive = true;
            _unitOfWork.CustomerRepository.Insert(mapper);
            return new ApiResponse();
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id) //id si verilen Stundent silindi
        {
            _unitOfWork.CustomerRepository.DeleteById(id);
            _unitOfWork.CustomerRepository.Save();
            return new ApiResponse();
        }

    }
}

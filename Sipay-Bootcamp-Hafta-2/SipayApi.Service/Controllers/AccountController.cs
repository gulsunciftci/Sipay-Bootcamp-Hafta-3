using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;

namespace SipayApi.Service.Controllers
{
    [Route("Sipay/api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        public ApiResponse<List<AccountResponse>> GetAll()
        {
            var list = _unitOfWork.AccountRepository.GetAll();
            var mapper = _mapper.Map<List<Account>, List<AccountResponse>>(list);
            return new ApiResponse<List<AccountResponse>>(mapper);
        }

        [HttpGet("{id}")]
        public ApiResponse<AccountResponse> Get(int id) //id'ye göre arama yapar
        {
            var entity = _unitOfWork.AccountRepository.GetById(id);
            var mapper = _mapper.Map<Account, AccountResponse>(entity);
            return new ApiResponse<AccountResponse>(mapper);
        }


        [HttpPost]
        public ApiResponse Post([FromBody] AccountRequest request) //Yeni bir student eklendi
        {
            var mapper = _mapper.Map<AccountRequest, Account>(request);
            mapper.IsActive = true;
            _unitOfWork.AccountRepository.Insert(mapper);
            _unitOfWork.AccountRepository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] AccountRequest request) //id si verilen student bulunuyorsa güncelleme işlemi yapıldı
        {
            var mapper = _mapper.Map<AccountRequest, Account>(request);
            mapper.IsActive = true;
            _unitOfWork.AccountRepository.Insert(mapper);
            return new ApiResponse();
        }


        [HttpDelete("{id}")]
        public ApiResponse Delete(int id) //id si verilen Stundent silindi
        {
            _unitOfWork.AccountRepository.DeleteById(id);
            _unitOfWork.AccountRepository.Save();
            return new ApiResponse();
        }


    }
}

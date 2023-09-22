using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Repository.AccountRepository;
using SipayApi.DataAccess.Repository.CustomerRepository;
using SipayApi.Schema;

namespace SipayApi.Service.Controllers
{
    [Route("Sipay/api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _repository;
        private readonly IMapper _mapper;
        public AccountController(IAccountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public ApiResponse<List<AccountResponse>> GetAll()
        {
            var list = _repository.GetAll();
            var mapper = _mapper.Map<List<Account>, List<AccountResponse>>(list);
            return new ApiResponse<List<AccountResponse>>(mapper);
        }

        [HttpGet("{id}")]
        public ApiResponse<AccountResponse> Get(int id) //id'ye göre arama yapar
        {
            var entity = _repository.GetById(id);
            var mapper = _mapper.Map<Account, AccountResponse>(entity);
            return new ApiResponse<AccountResponse>(mapper);
        }


        [HttpPost]
        public ApiResponse Post([FromBody] AccountRequest request) //Yeni bir student eklendi
        {
            var mapper = _mapper.Map<AccountRequest, Account>(request);
            mapper.IsActive = true;
            _repository.Insert(mapper);
            _repository.Save();
            return new ApiResponse();
        }

        [HttpPut("{id}")]
        public ApiResponse Put(int id, [FromBody] AccountRequest request) //id si verilen student bulunuyorsa güncelleme işlemi yapıldı
        {
            var mapper = _mapper.Map<AccountRequest, Account>(request);
            mapper.IsActive = true;
            _repository.Insert(mapper);
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

using AutoMapper;
using SipayApi.Base.Response;
using SipayApi.Business.Generic;
using SipayApi.DataAccess.Domain;
using SipayApi.DataAccess.Unitofw;
using SipayApi.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Business.CustomerS
{
    public class CustomerService : IGenericService<Customer, CustomerRequest, CustomerResponse>, ICustomerService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;


        public CustomerService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ApiResponse Delete(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse<List<CustomerResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public ApiResponse<CustomerResponse> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public ApiResponse Insert(CustomerRequest request)
        {
            throw new NotImplementedException();
        }

        public ApiResponse Update(int id, CustomerRequest request)
        {
            throw new NotImplementedException();
        }
    }
}

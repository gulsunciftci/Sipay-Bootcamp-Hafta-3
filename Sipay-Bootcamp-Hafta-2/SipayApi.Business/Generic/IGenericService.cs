using SipayApi.Base.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Business.Generic
{
    //Bu servisin 3 modeli olacak(TEntity,TRequest,TResponse)
    public interface IGenericService<TEntity,TRequest,TResponse>
    {
        ApiResponse<List<TResponse>> GetAll();
        ApiResponse<TResponse> GetById(int id);
        ApiResponse Insert(TRequest request);
        ApiResponse Update(int id,TRequest request);
        ApiResponse Delete(int id);
    }
}

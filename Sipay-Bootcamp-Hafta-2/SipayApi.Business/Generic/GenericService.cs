using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SipayApi.Base.BaseModel;
using SipayApi.Base.Response;
using SipayApi.DataAccess.Repository.Base;
using SipayApi.DataAccess.Unitofw;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipayApi.Business.Generic
{
    public class GenericService<TEntity, TRequest, TResponse> : IGenericService<TEntity, TRequest, TResponse> where TEntity : BaseModel
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        //IUnitOfWork olduğu için gerek kalmadı
        //private readonly IGenericRepository<TEntity> _genericRepository;
        //private readonly DbContext _dbContext;
        public GenericService(IMapper mapper,  IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public ApiResponse Delete(int id)
        {

            //var entity = _dbContext.Set<TEntity>().Find(id);
            //_dbContext.Set<TEntity>().Remove(entity);
            //_dbContext.SaveChanges();
           try
            {
               var entity =_unitOfWork.Repository<TEntity>().GetById(id);
               if (entity == null)
               {
                   return new ApiResponse("Record not found");
               }
               _unitOfWork.Repository<TEntity>().DeleteById(id);
               return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse<List<TResponse>> GetAll()
        {
            try
            {
                var entity = _unitOfWork.Repository<TEntity>().GetAll();
                var mapped = _mapper.Map<List<TEntity>,List<TResponse>>(entity);
                return new ApiResponse<List<TResponse>>(mapped);
            }
            catch (Exception ex)
            {
                return new ApiResponse<List<TResponse>>(ex.Message);
            }
        }

        public ApiResponse<TResponse> GetById(int id)
        {
            try
            {
                var entity = _unitOfWork.Repository<TEntity>().GetById(id);
                var mapped = _mapper.Map<TEntity, TResponse>(entity);
                return new ApiResponse<TResponse>(mapped);
            }
            catch (Exception ex)
            {
                return new ApiResponse<TResponse>(ex.Message);
            }
        }

        public ApiResponse Insert(TRequest request)
        {
            try
            {
                //trequestten tentityye çevirdim
                var entity = _mapper.Map<TRequest,TEntity>(request);
                entity.InsertDate = DateTime.Now;
                entity.InsertUser = "sim@admin.com";
                _unitOfWork.Repository<TEntity>().Insert(entity);
                _unitOfWork.Repository<TEntity>().Save();
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }

        public ApiResponse Update(int id, TRequest request)
        {
            try
            {
                var exist = _unitOfWork.Repository<TEntity>().GetById(id);
                if (exist == null)
                {
                    return new ApiResponse("Record not found");
                }

                var entity = _mapper.Map<TRequest, TEntity>(request);
                _unitOfWork.Repository<TEntity>().Update(entity);
                _unitOfWork.Repository<TEntity>().Save();
                
                return new ApiResponse();
            }
            catch (Exception ex)
            {
                return new ApiResponse(ex.Message);
            }
        }
    }
}

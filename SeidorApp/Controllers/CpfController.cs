using BaseCore.ApiControllers;
using BaseCore.Interfaces;
using BaseCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SeidorApp.Core.Business;
using SeidorApp.Core.Models;

namespace SeidorApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CpfController : NDBaseController
    {
        private readonly ILogger _logger;
        private readonly CpfBusiness _cpfBusiness;

        public CpfController(CpfBusiness cpfBusiness, ILogger<CpfController> logger, IAuth authService) : base(logger, authService)
        {
            _logger = logger;
            _cpfBusiness = cpfBusiness;
        }


        [HttpPost]
        public ActionResult<Response<long>> Insert(Cpf cpf)
        {
            try
            {
                return GetResponse(() => _cpfBusiness.Insert(cpf));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<long>(ex);
            }
        }

        [HttpPut]
        public ActionResult<Response<bool>> Update(Cpf cpf)
        {
            try
            {
                return GetResponse(() => _cpfBusiness.Update(cpf));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<bool>(ex);
            }
        }

        [HttpGet]
        public ActionResult<ListResponse<Cpf>> FindAll()
        {
            try
            {
                Request request = new Request();
                return GetListResponse(() => _cpfBusiness.FindByRequest(request));
            }
            catch (Exception ex)
            {
                return GetErrorListResponse<Cpf>(ex);
            }
        }

        [HttpGet("findCountAll")]
        public ActionResult<Response<int>> FindCountAll()
        {
            try
            {
                Request request = new Request();
                return GetResponse(() => _cpfBusiness.FindCountByRequest(request));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<int>(ex);
            }
        }

        [HttpGet("findByDocument")]
        public ActionResult<ListResponse<Cpf>> FindByDocument([FromQuery] string document)
        {
            try
            {
                return GetListResponse(() => _cpfBusiness.FindByDocument(document));
            }
            catch (Exception ex)
            {
                return GetErrorListResponse<Cpf>(ex);
            }
        }

        [HttpGet("findByBlockStatus")]
        public ActionResult<ListResponse<Cpf>> FindByBlockStatus([FromQuery] bool isBlocked)
        {
            try
            {
                return GetListResponse(() => _cpfBusiness.FindByBlockStatus(isBlocked));
            }
            catch (Exception ex)
            {
                return GetErrorListResponse<Cpf>(ex);
            }
        }

        [HttpDelete()]
        public ActionResult<Response<bool>> Delete([FromQuery] long cpfId)
        {
            try
            {
                Cpf cpf = new Cpf()
                {
                    Id = cpfId
                };

                return GetResponse(() => _cpfBusiness.Delete(cpf));
            }
            catch (Exception ex)
            {
                return GetErrorResponse<bool>(ex);
            }
        }
    }
}

using AutoMapper;
using Framework.MainAPI.Filters;
using Framework.MainEntity.DbEntity;
using Framework.MainEntity.Dto;
using Framework.Service.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Framework.MainAPI.Controllers
{
    [RoutePrefix("api/company")]
    [FrameworkAuthorize]
    public class CompanyController : ApiController
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper, ILoggerService logger)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        // GET: Company
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var companys = await _companyService.GetCompanies();
            var mappedModels = _mapper.Map<IEnumerable<CompanyQueryDto>>(companys);
            _logger.FileInfo("Index Loaded");
            //_logger.FileWarn("Index Warn");

            //_logger.DbFatal("Index Loaded Begin");
            //_logger.DbFatal("Index Loaded End");

            return Ok(mappedModels);
        }


        [HttpPost]
        public async Task<IHttpActionResult> Add(CompanyAddDto addDto)
        {
            throw new Exception("test");

            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<Company>(addDto);
                mappedModel.Id = Guid.NewGuid();
                if (await _companyService.AddCompanyAsync(mappedModel))
                {
                    return Created(nameof(Get), addDto);
                }
            }

            return BadRequest();

        }

        [HttpPatch]
        public async Task<IHttpActionResult> Update(CompanyUpdateDto updateDto)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<Company>(updateDto);
                if (await _companyService.UpdateAsync(mappedModel))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            if (await _companyService.DeleteAsync(id))
                return Ok();

            return BadRequest();
        }
    }
}

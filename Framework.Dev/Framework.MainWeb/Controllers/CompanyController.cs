using AutoMapper;
using Framework.MainEntity.DbEntity;
using Framework.MainEntity.Dto;
using Framework.MainWeb.Filters;
using Framework.Service.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Framework.MainWeb.Controllers
{
    [FrameworkAuthorize]
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        private readonly ILoggerService _logger;

        public CompanyController(ICompanyService companyService, IMapper mapper,ILoggerService logger)
        {
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
        }

        // GET: Company
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var companys = await _companyService.GetCompanies();
            var mappedModels = _mapper.Map<IEnumerable<CompanyQueryDto>>(companys);
            _logger.FileInfo("Index Loaded");
            //_logger.FileWarn("Index Warn");

            //_logger.DbFatal("Index Loaded Begin");
            //_logger.DbFatal("Index Loaded End");

            return View(mappedModels);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(new CompanyAddDto());
        }

        [HttpPost]
        public async Task<ActionResult> Add(CompanyAddDto addDto)
        {          
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<Company>(addDto);
                mappedModel.Id = Guid.NewGuid();
                if (await _companyService.AddCompanyAsync(mappedModel))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();

        }

        [HttpGet]
        public async Task<ActionResult> Update(Guid id)
        {
            var model = await _companyService.QueryAsync(id);
            if (model == null)
                return RedirectToAction(nameof(Index));
            var mappedModel = _mapper.Map<CompanyUpdateDto>(model);
            return View(mappedModel);
        }

        [HttpPost]
        public async Task<ActionResult> Update(CompanyUpdateDto updateDto)
        {
            if (ModelState.IsValid)
            {
                var mappedModel = _mapper.Map<Company>(updateDto);
                if(await _companyService.UpdateAsync(mappedModel))
                {
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid id)
        {
            if(await _companyService.DeleteAsync(id))
                return RedirectToAction(nameof(Index));

            return View();
        }
    }
}
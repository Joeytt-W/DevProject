using AutoMapper;
using Dev.CoreApi.Filters;
using Dev.Entity.ViewModel;
using Dev.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dev.CoreApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("CorsPolicy")]属性启用跨域
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };
        private readonly ITestService _test;
        private readonly IMapper _mapper;
        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, ITestService test,IMapper mapper)
        {
            _logger = logger;
            _test = test;
            _mapper = mapper;
        }

        /// <summary>
        /// GetWeatherForecast
        /// </summary>
        /// <returns>WeatherForecasts</returns>
        [HttpGet]
        [TypeFilter(typeof(CustomActionFilter))]//用typefilter是为了过滤器中使用依赖注入,如果使用ServiceFilter需要在Startup注册服务
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }

        /// <summary>
        /// 获取所有学生信息
        /// </summary>
        /// <returns>Students</returns>
        [HttpGet(nameof(GetTestStudentInfo))]
        [CustomAuthsizeFilter]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//jwt验证
        public async Task<IEnumerable<Student>> GetTestStudentInfo()
        {
            var stus = await _test.GetTestStudentInfo();
            _logger.LogInformation("获取所有学生信息");
            return _mapper.Map<IEnumerable<Student>>(stus);
        }

        /// <summary>
        /// GetSingleStudent
        /// </summary>
        /// <param name="student">输入要查询的学生信息</param>
        /// <returns></returns>
        [HttpPost(nameof(GetSingleStudent))]
        //指定认证时，采用CustomerAuthenticationHandler.CustomerSchemeName
        [Authorize(AuthenticationSchemes = CustomerAuthenticationHandler.CustomerSchemeName)]
        public async Task<Student> GetSingleStudent(Student student)
        {
            var stus = await _test.GetTestStudentInfo();
            var st = stus.SingleOrDefault(c => c.Id == student.Id && c.DbName == student.Name);
            return _mapper.Map<Student>(st);
        }
    }
}

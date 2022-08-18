using AutoMapper;
using Dev.Web.DbEntities;
using Dev.Web.Utils;
using Dev.Web.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dev.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMapper _mapper;

        public HomeController()
        {
            _mapper = AutoFacConfig.Resolve<Mapper>();
        }
        public ActionResult Index()
        {
            var dbStus = new List<DbStu>();

            for (int i = 0; i < 10; i++)
            {
                dbStus.Add(new DbStu()
                {
                    Id = i + 1,
                    DbName = "张三" + i
                });
            }

            var students = _mapper.Map<IEnumerable<Student>>(dbStus);

            DevWebLogUtil.Info("Index View");
            foreach (var item in students)
            {
                DevWebLogUtil.Info(item.Name);
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
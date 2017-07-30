using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;
using CustomControl.Models;
using CustomControl.Services;

namespace CustomControl.Controllers
{
    public class HomeController : Controller
    {
        private TimeIntervalsService _intervalsService;
        public HomeController()
        {
            _intervalsService = new TimeIntervalsService();
        }

        public ActionResult Index()
        {
            var sReader = new StreamReader(Server.MapPath("~/App_Data/temp.xml"));
            var timeLineList = _intervalsService.TimeLineSerializer(sReader);

            //timeLineList.Add(new TimeLine
            //{
            //    TimeInterval = _intervalsService.TimeIntervalResolver("6:30", "22:30", 15),
            //    From = "6:30",
            //    Until = "21:30",
            //    IntervalMinutes = 15,
            //    SwimmingPools = new List<SwimmingPool>()
            //});
            //timeLineList.Add();
            return View("Template", timeLineList);
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
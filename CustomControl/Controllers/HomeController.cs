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
            //var v = XmlTemp();
            //var sReader = new StreamReader(Server.MapPath("~/App_Data/temp.xml"));
            var timeLineList = _intervalsService.TimeLineSerializer(XmlTemp());

            return View("Template", timeLineList);
        }

        public ActionResult SwimmingPoolTablePart(SwimmingPool swimmingPool, int intervalsCount)
        {
            swimmingPool = _intervalsService.SwimLinesTableStructCreator(swimmingPool, intervalsCount);
            swimmingPool.TimeIntervals = intervalsCount;
            return View("_SwimmingPoolTablePart", swimmingPool);
        }

        public string XmlTemp()
        {
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                    "XmlTemp");
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                    ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
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
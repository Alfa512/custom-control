﻿using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        //public ActionResult ResourceTimeline(TimeLine timeLine)
        //{
        //    return View("_ResourceTimeline", timeLine);
        //}

        public ActionResult ResourceSearch(string search)
        {
            search = search.Trim().ToLower();
            var timeLineList = _intervalsService.TimeLineSerializer(XmlTemp());//.Select(r => r).Where(r => r.SwimmingPools.All(a => a.Name.Contains(search)));
            var resultList = new List<TimeLine>();
            foreach (var timeLine in timeLineList)
            {
                timeLine.SwimmingPools = timeLine.SwimmingPools.Where(r => r.Name.ToLower().Contains(search)).ToList();
                if (timeLine.SwimmingPools.Count > 0)
                    resultList.Add(timeLine);
            }
            return View("_ResourceTimeline", resultList);
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
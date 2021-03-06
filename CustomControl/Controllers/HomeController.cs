﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using CustomControl.Models;
using CustomControl.Services;
using Microsoft.Ajax.Utilities;

namespace CustomControl.Controllers
{
	public class HomeController : Controller
	{
		private readonly TimeIntervalsService _intervalsService;

		public HomeController()
		{
			_intervalsService = new TimeIntervalsService();
		}

		public ActionResult Index()
		{
			var timeLineList = _intervalsService.TimeLineSerializer(XmlTemp());

			return View("Template", timeLineList);
		}

		public ActionResult SwimmingPoolTablePart(SwimmingPool swimmingPool, int intervalsCount)
		{
			swimmingPool = _intervalsService.SwimLinesTableStructCreator(swimmingPool, intervalsCount);
			swimmingPool.TimeIntervals = intervalsCount;
			return View("_SwimmingPoolTablePart", swimmingPool);
		}

		public ActionResult SwimmingPoolTableRender(SwimmingPool swimmingPool, int? intervalsCount)
		{
			if (intervalsCount != null && intervalsCount > 0)
				swimmingPool.TimeIntervals = (int)intervalsCount;
			return View("_SwimmingPoolTablePart", swimmingPool);
		}

		public ActionResult ResourceSearch(string search, string startTime, string endTime)
		{
			startTime = !startTime.IsNullOrWhiteSpace() ? startTime.Trim().ToLower() : "";
			endTime = !endTime.IsNullOrWhiteSpace() ? endTime.Trim().ToLower() : "";
			search = !search.IsNullOrWhiteSpace() ? search.Trim().ToLower() : "";
			var timeLineList = _intervalsService.TimeLineSerializer(XmlTemp());
			var resultList = new List<TimeLine>();
			foreach (var timeLine in timeLineList)
			{
				timeLine.SwimmingPools.ForEach(r => _intervalsService.SwimLinesTableStructCreator(r, r.PoolSchedule.Intervals?.Count ?? r.TimeIntervals));
				timeLine.SwimmingPools = timeLine.SwimmingPools.Where(r => r.Name.ToLower().Contains(search)).ToList();
				if (timeLine.SwimmingPools.Count > 0)
					resultList.Add(timeLine);
			}

			if (!startTime.IsNullOrWhiteSpace() && startTime.Split(':').Length > 1)
			{
				foreach (var timeLine in timeLineList)
				{
					var timeInterval =
						timeLine.TimeInterval.Intervals.Where(r => GetMinutes(r.From) > GetMinutes(startTime))
							.ToList();
					var poolTimeInterval = timeInterval;
					if (timeInterval.Count > 0)
						timeLine.TimeInterval.Intervals = timeInterval;
					timeLine.SwimmingPools.ForEach(r =>
					{
						var sPoolTimeInterval =
							r.PoolSchedule.Intervals.Where(interval => GetMinutes(interval.From) > GetMinutes(startTime))
								.ToList();

						if (sPoolTimeInterval.Count > 0)
							poolTimeInterval = sPoolTimeInterval;

						r.PoolSchedule.Intervals = poolTimeInterval;
						r.SwimLinesList.RemoveRange(0, r.SwimLinesList.Count - poolTimeInterval.Count);
					});
				}
			}

			if (!endTime.IsNullOrWhiteSpace() && endTime.Split(':').Length > 1)
			{
				foreach (var timeLine in timeLineList)
				{
					var timeInterval =
						timeLine.TimeInterval.Intervals.Where(r => GetMinutes(r.From) < GetMinutes(endTime))
							.ToList();
					var poolTimeInterval = timeInterval;
					if (timeInterval.Count > 0)
						timeLine.TimeInterval.Intervals = timeInterval;
					timeLine.SwimmingPools.ForEach(r =>
					{
						var sPoolTimeInterval =
							r.PoolSchedule.Intervals.Where(interval => GetMinutes(interval.From) < GetMinutes(endTime))
								.ToList();

						if (sPoolTimeInterval.Count > 0)
							poolTimeInterval = sPoolTimeInterval;

						r.PoolSchedule.Intervals = poolTimeInterval;
						r.SwimLinesList.RemoveRange(poolTimeInterval.Count, r.SwimLinesList.Count - poolTimeInterval.Count);
					});
				}
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

		public int GetMinutes(string time)
		{
			if (time.IsNullOrWhiteSpace() || time.Split(':').Length < 2)
				return 0;
			var hours = time.Split(':')[0];
			var minutes = time.Split(':')[1];
			return Convert.ToInt32(hours ?? "0") * 60 + Convert.ToInt32(minutes ?? "0");
		}
	}
}
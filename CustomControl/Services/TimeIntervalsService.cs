using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using CustomControl.Models;

namespace CustomControl.Services
{
    public class TimeIntervalsService
    {
        public TimeInterval TimeIntervalResolver(string from, string until, int interval)
        {
            var timeInterval = new TimeInterval();
            var splitHourMinute = from.Split(':');
            var fromTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(splitHourMinute[0]), Convert.ToInt32(splitHourMinute[1]), 0);
            splitHourMinute[0] = until.Split(':')[0];
            splitHourMinute[1] = until.Split(':')[1];
            var untilTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, Convert.ToInt32(splitHourMinute[0]), Convert.ToInt32(splitHourMinute[1]), 0);
            timeInterval.TotalMinutes = (untilTime.Hour - fromTime.Hour) * 60 - fromTime.Minute + untilTime.Minute;
            timeInterval.IntervalList = new List<WaterTime>();
            for (var i = 0; i < timeInterval.TotalMinutes; i += interval)
            {
                timeInterval.IntervalList.Add(new WaterTime
                {
                    From = fromTime.AddMinutes(i).ToString("HH:mm"),
                    Until = fromTime.AddMinutes(i + interval).ToString("HH:mm")
                });
            }
            return timeInterval;
        }

        public List<TimeLine> TimeLineSerializer(string text)
        {

	        var serializer = new XmlSerializer(typeof(TimeLineCollection));

            var textReader = new StringReader(text);
            var timeLine = (TimeLineCollection)serializer.Deserialize(textReader);
	        if (timeLine?.TimeLines == null || !timeLine.TimeLines.Any()) return timeLine?.TimeLines?.ToList();
	        foreach (var line in timeLine.TimeLines)
	        {
		        line.TimeInterval = TimeIntervalResolver(line.From, line.Until, line.IntervalMinutes);
		        line.SwimmingPools.ForEach(r => r.TimeIntervals = line.TimeInterval.IntervalList.Count);
	        }

	        return timeLine.TimeLines?.ToList();
        }

        public SwimmingPool SwimLinesTableStructCreator(SwimmingPool pool, int intervalsCount)
        {
            for (var i = 0; i < intervalsCount; i++)
            {
                pool.SwimLinesList.Add(pool.SwimLines.Select(item => item.SwimLineItems
                        .Select((value, j) => new { j, value })
                        .FirstOrDefault(r => r.value != null && r.j == i)
                        ?.value)
                    .Where(r => r != null)
                    .ToList());
                if (pool.SwimLinesList[i].Count >= pool.SwimLines.Count) continue;

                pool.SwimLinesList[i].AddRange(Enumerable.Repeat(new SwimLineItem(), pool.SwimLines.Count - pool.SwimLinesList[i].Count));
            }

            return pool;
        }


    }
}
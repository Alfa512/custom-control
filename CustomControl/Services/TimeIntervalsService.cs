﻿using System;
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
            timeInterval.TotalMinutes= (untilTime.Hour - fromTime.Hour) * 60 - fromTime.Minute + untilTime.Minute;
            timeInterval.IntervalList = new List<WaterTime>();
            for (var i = 0; i < timeInterval.TotalMinutes; i+=interval)
            {
                timeInterval.IntervalList.Add(new WaterTime
                {
                    From = fromTime.AddMinutes(i).ToString("HH:mm"),
                    Until = fromTime.AddMinutes(i + interval).ToString("HH:mm")
                });
            }
            return timeInterval;
        }

        public List<TimeLine> TimeLineSerializer(StreamReader sReader)
        {
            const string path = "C:\\Source\\custom-control\\CustomControl\\Templates\\temp.xml";

            var xRoot = new XmlRootAttribute();
            xRoot.ElementName = "timelines";
            xRoot.IsNullable = false;

            var serializer = new XmlSerializer(typeof(TimeLineCollection));

            var reader = sReader;//new StreamReader(path);
            //reader.BaseStream.Seek(0, SeekOrigin.Begin);
            var timeLine = (TimeLineCollection)serializer.Deserialize(reader);
            if (timeLine?.TimeLines != null && timeLine.TimeLines.Any())
            {
                foreach (var line in timeLine.TimeLines)
                {
                    line.TimeInterval = TimeIntervalResolver(line.From, line.Until, line.IntervalMinutes);
                }
            }
            //reader.BaseStream.Seek(0, SeekOrigin.Begin);
            //var timeLine2 = (TimeLineCollection)serializer.Deserialize(reader);
            reader.Close();
            return timeLine?.TimeLines?.ToList();
        }
    }
}
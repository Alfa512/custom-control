using System.Collections.Generic;

namespace CustomControl.Models
{
    public class TimeInterval
    {
        public int TotalMinutes { get; set; }
        public List<WaterTime> IntervalList { get; set; }
    }
}
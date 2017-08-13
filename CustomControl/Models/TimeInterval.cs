using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    public class TimeInterval
    {
        public int TotalMinutes { get; set; }
	    [XmlElement("interval", typeof(WaterTime))]
		public List<WaterTime> Intervals { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
	[Serializable]
	public class PoolSchedule
	{
		[XmlElement("interval", typeof(WaterTime))]
		public List<WaterTime> Intervals { get; set; }
	}
}
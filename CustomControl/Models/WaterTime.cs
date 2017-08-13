using System;
using System.Xml.Serialization;

namespace CustomControl.Models
{
	[Serializable]
    public class WaterTime
    {
	    [XmlAttribute("from")]
		public string From { get; set; }
	    [XmlAttribute("to")]
		public string To { get; set; }
    }
}
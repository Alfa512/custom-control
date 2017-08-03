using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    public class SwimmingPool
    {
        //[XmlArray("swimmingpool")]
        [XmlElement("swimlines", typeof(SwimLine))]
        public List<SwimLine> SwimLines { get; set; }

        public List<List<SwimLineItem>> SwimLinesList { get; set; }
        public int TimeIntervals { get; set; }
    }
}
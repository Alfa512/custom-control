﻿using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    public class TimeLine
    {
        [XmlAttribute("from")]
        public string From { get; set; }
        [XmlAttribute("until")]
        public string Until { get; set; }
        [XmlAttribute("intervalMinutes")]
        public int IntervalMinutes { get; set; }
        [XmlElement("swimmingpool", typeof(SwimmingPool))]
        public List<SwimmingPool> SwimmingPools { get; set; }
        public TimeInterval TimeInterval { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    [XmlRoot("timelineColl")]
    public class TimeLineCollection
    {
        [XmlArray("timelines")]
        [XmlArrayItem("timeline", typeof(TimeLine))]
        public List<TimeLine> TimeLines { get; set; }
    }
}
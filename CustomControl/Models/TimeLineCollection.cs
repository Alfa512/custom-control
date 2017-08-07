using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    [XmlRoot("timelinecollection")]
    public class TimeLineCollection
    {
        [XmlArray("timelines")]
        [XmlArrayItem("timeline", typeof(TimeLine))]
        public List<TimeLine> TimeLines { get; set; }
    }
}
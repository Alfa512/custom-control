using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using CustomControl.Enums;

namespace CustomControl.Models
{
    [Serializable]
    public class SwimLine
    {
        [XmlAttribute("orderbydirection")]
        public string OrderByDirection { get; set; }
        [XmlAttribute("orderby")]
        public string OrderBy { get; set; }
        [XmlAttribute("resourcetype")]
        public string ResourceType { get; set; }
        [XmlAttribute("filter")]
        public string Filter { get; set; }
        //[XmlArray("swimlines")]
        [XmlElement("add", typeof(SwimLineItem))]
        public List<SwimLineItem> SwimLineItems { get; set; }

    }
}
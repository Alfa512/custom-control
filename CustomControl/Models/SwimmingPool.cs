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
    }
}
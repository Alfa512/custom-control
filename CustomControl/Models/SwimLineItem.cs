using System;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    public class SwimLineItem
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
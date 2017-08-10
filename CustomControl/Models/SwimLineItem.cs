using System;
using System.Xml.Serialization;

namespace CustomControl.Models
{
    [Serializable]
    public class SwimLineItem
    {
        public SwimLineItem()
        {
            Name = "";
            Value = "";
        }

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
        [XmlAttribute("from")]
        public string From { get; set; }
        [XmlAttribute("until")]
        public string Until { get; set; }
    }
}
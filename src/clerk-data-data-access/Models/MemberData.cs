using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    [XmlRoot("MemberData")]
    public class MemberData
    {
        [XmlAttribute("publish-date")]
        public string PublishDate { get; set; }
        
        [XmlElement("title-info")]
        public TitleInfo TitleInfo { get; set; }
        
        [XmlArray("members")]
        [XmlArrayItem("member")]
        public List<Member> Members { get; set; }
        
        [XmlArray("committees")]
        [XmlArrayItem("committee")]
        public List<Committee> Committees { get; set; }
    }
}

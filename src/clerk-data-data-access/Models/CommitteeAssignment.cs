using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class CommitteeAssignment
    {
        [XmlAttribute("comcode")]
        public string CommitteeCode { get; set; }
        [XmlAttribute("rank")]
        public string Rank { get; set; }
    }
}
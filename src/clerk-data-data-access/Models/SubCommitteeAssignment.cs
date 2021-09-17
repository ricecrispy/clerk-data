using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class SubCommitteeAssignment : CommitteeAssignment
    {
        [XmlAttribute("subcomcode")]
        public string SubCommitteeCode { get; set; }
    }
}
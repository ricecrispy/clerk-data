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

    [Serializable]
    public class SubCommitteeAssignment : CommitteeAssignment
    {
        [XmlAttribute("subcomcode")]
        public string SubCommitteeCode { get; set; }
    }

    public class CommitteeAssignmentDb
    {
        public string CommitteeCode { get; set; }
        public string Rank { get; set; }
        public bool IsSubCommittee { get; set; }
    }
}
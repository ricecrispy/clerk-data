using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class Member
    {
        [XmlElement("statedistrict")]
        public string StateDistrict { get; set; }
        [XmlElement("member-info")]
        public MemberInfo MemberInfo { get; set; } = new MemberInfo();
        [XmlArray("committee-assignments")]
        [XmlArrayItem("committee", Type = typeof(CommitteeAssignment))]
        [XmlArrayItem("subcommittee", Type = typeof(SubCommitteeAssignment))]
        public List<CommitteeAssignment> CommitteeAssignments { get; set; }
    }

    public class MemberDb: Member
    {
        public string StateCode { get; set; }
    }
}
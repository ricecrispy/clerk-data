using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class CommitteeAssignments
    {
        [XmlArray("comm")]
        public List<CommitteeAssignment> CommitteeMemberships { get; set; }
        [XmlElement(ElementName = "subcommittee")]
        public List<SubCommitteeAssignment> SubComitteeMemberships { get; set; }
    }
}
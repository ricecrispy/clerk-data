using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    /// <summary>
    /// The data model for the committee-assignments element
    /// </summary>
    [Serializable]
    public class CommitteeAssignments
    {
        [XmlArray("comm")]
        public List<CommitteeAssignment> CommitteeMemberships { get; set; }
        [XmlArray("subcommittee")]
        public List<SubCommitteeAssignment> SubComitteeMemberships { get; set; }
    }
}
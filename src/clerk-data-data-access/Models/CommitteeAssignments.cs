using System.Collections.Generic;

namespace clerk_data_data_access.Models
{
    public class CommitteeAssignments
    {
        public List<CommitteeAssignment> CommitteeMemberships { get; set; }
        public List<SubCommitteeAssignment> SubComitteeMemberships { get; set; }
    }
}
using System;

namespace clerk_data_data_access.Models
{
    public class Member
    {
        public string StateDistrict { get; set; }
        public MemberInfo MemberInfo { get; set; }
        public CommitteeAssignments CommitteeAssignments { get; set; }
    }
}
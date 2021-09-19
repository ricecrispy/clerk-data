using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class CommitteeAssignmentDbMap : EntityMap<CommitteeAssignmentDb>
    {
        public CommitteeAssignmentDbMap()
        {
            Map(t => t.CommitteeCode).ToColumn("committee_code");
            Map(t => t.Rank).ToColumn("member_rank");
            Map(t => t.IsSubCommittee).ToColumn("is_sub_committee");
        }
    }
}

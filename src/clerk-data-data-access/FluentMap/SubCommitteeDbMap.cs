using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class SubCommitteeDbMap : EntityMap<SubCommitteeDb>
    {
        public SubCommitteeDbMap()
        {
            Map(r => r.SubComCode).ToColumn("subcommittee_code");
            Map(r => r.SubComRoom).ToColumn("subcommittee_room");
            Map(r => r.SubComZip).ToColumn("subcommittee_zip");
            Map(r => r.SubComZipSuffix).ToColumn("subcommittee_zip_suffix");
            Map(r => r.SubComBuildingCode).ToColumn("subcommittee_building_code");
            Map(r => r.SubComPhone).ToColumn("subcommittee_phone");
            Map(r => r.SubComFullName).ToColumn("subcommittee_full_name");
            Map(r => r.CommitteeMajority).ToColumn("subcommittee_majority");
            Map(r => r.CommitteeMinority).ToColumn("subcommittee_minority");
        }
    }
}

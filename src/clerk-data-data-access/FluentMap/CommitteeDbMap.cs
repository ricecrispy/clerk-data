using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class CommitteeDbMap : EntityMap<CommitteeDb>
    {
        public CommitteeDbMap()
        {
            Map(r => r.Type).ToColumn("committee_type");
            Map(r => r.Code).ToColumn("committee_code");
            Map(r => r.Room).ToColumn("committee_room");
            Map(r => r.HeaderText).ToColumn("committee_header_text");
            Map(r => r.Zip).ToColumn("committee_zip");
            Map(r => r.ZipSuffix).ToColumn("committee_zip_suffix");
            Map(r => r.BuildingCode).ToColumn("committee_building_code");
            Map(r => r.Phone).ToColumn("committee_phone");
            Map(r => r.FullName).ToColumn("committee_full_name");
            Map(r => r.CommitteeMajority).ToColumn("committee_majority");
            Map(r => r.CommitteeMinority).ToColumn("committee_minority");
        }
    }
}

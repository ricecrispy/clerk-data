using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class MemberDbMap : EntityMap<MemberDb>
    {
        public MemberDbMap()
        {
            Map(t => t.StateDistrict).ToColumn("state_district");
            Map(t => t.StateCode).ToColumn("representing_state");
            Map(t => t.BioGuideId).ToColumn("bioguide_id");
            Map(t => t.LastName).ToColumn("last_name");
            Map(t => t.FirstName).ToColumn("first_name");
            Map(t => t.MiddleName).ToColumn("middle_name");
            Map(t => t.Suffix).ToColumn("suffix");
            Map(t => t.Courtesy).ToColumn("courtesy");
            Map(t => t.NameList).ToColumn("name_list");
            Map(t => t.SortName).ToColumn("sort_name");
            Map(t => t.OfficialName).ToColumn("official_name");
            Map(t => t.FormalName).ToColumn("formal_name");
            Map(t => t.PriorCongress).ToColumn("prior_congress");
            Map(t => t.Party).ToColumn("party");
            Map(t => t.Caucus).ToColumn("caucus");
            Map(t => t.District).ToColumn("district");
            Map(t => t.TownName).ToColumn("town_name");
            Map(t => t.OfficeBuilding).ToColumn("office_building");
            Map(t => t.OfficeRoom).ToColumn("office_room");
            Map(t => t.OfficeZip).ToColumn("office_zip");
            Map(t => t.OfficeZipSuffix).ToColumn("office_zip_suffix");
            Map(t => t.PhoneNumber).ToColumn("office_phone_number");
            Map(t => t.ElectedDate).ToColumn("elected_date");
            Map(t => t.SwornDate).ToColumn("sworn_date");

        }
    }
}

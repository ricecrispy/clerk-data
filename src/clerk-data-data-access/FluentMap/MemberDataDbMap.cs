using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class MemberDataDbMap : EntityMap<MemberDataDb>
    {
        public MemberDataDbMap()
        {
            Map(t => t.PublishDate).ToColumn("publish_date");
            Map(t => t.DbCongressNum).ToColumn("congress_num");
            Map(t => t.DbCongressText).ToColumn("congress_text");
            Map(t => t.DbSession).ToColumn("meeting_session");
            Map(t => t.DbMajority).ToColumn("majority");
            Map(t => t.DbMinority).ToColumn("minority");
            Map(t => t.DbClerk).ToColumn("clerk");
            Map(t => t.DbWebUrl).ToColumn("web_url");
        }
    }
}

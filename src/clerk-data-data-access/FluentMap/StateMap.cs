using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class StateMap : EntityMap<State>
    {
        public StateMap()
        {
            Map(r => r.PostalCode).ToColumn("state_postal_code");
            Map(r => r.FullName).ToColumn("state_name");
        }
    }
}

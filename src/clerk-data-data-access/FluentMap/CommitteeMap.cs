using clerk_data_data_access.Models;
using Dapper.FluentMap.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal class CommitteeMap : EntityMap<Committee>
    {
        public CommitteeMap()
        {
        }
    }
}

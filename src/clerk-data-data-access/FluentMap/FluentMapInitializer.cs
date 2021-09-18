using Dapper.FluentMap;
using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.FluentMap
{
    internal static class FluentMapInitializer
    {
        static FluentMapInitializer()
        {
            FluentMapper.Initialize(t => t.AddMap(new MemberDataMap()));
            FluentMapper.Initialize(t => t.AddMap(new MemberMap()));
            FluentMapper.Initialize(t => t.AddMap(new CommitteeDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new SubCommitteeDbMap()));
        }

        /// <summary>
        /// Ensures the static constructor has been run to initialize the mappings.
        /// </summary>
        public static void EnsureMapsInitialized()
        {
            //See summary for purpose of this method.
        }
    }
}

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
            FluentMapper.Initialize(t => t.AddMap(new MemberDataDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new MemberDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new CommitteeDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new SubCommitteeDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new CommitteeAssignmentDbMap()));
            FluentMapper.Initialize(t => t.AddMap(new StateMap()));
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

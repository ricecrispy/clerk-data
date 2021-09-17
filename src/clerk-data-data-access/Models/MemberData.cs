using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.Models
{
    public class MemberData
    {
        public DateTime PublishData { get; set; }

        public TitleInfo TitleInfo { get; set; }

        public List<Member> Members { get; set; }

        public List<Committee> Committees { get; set; }
    }
}

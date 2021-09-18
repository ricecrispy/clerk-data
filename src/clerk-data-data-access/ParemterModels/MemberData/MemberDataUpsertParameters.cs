using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.ParemterModels.MemberData
{
    internal class MemberDataUpsertParameters
    {
        public string p_publish_date { get; set; }
        public int p_congress_num { get; set; }
        public string p_congress_text { get; set; }
        public int p_session { get; set; }
        public string p_majority { get; set; }
        public string p_minority { get; set; }
        public string p_clerk { get; set; }
        public string p_web_url { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.ParameterModel.Committee
{
    internal class SubCommitteeUpsertParameters
    {
        public string p_code { get; set; }
        public string p_room { get; set; }
        public string p_zip { get; set; }
        public string p_zip_suffix { get; set; }
        public string p_building_code { get; set; }
        public string p_phone { get; set; }
        public string p_full_name { get; set; }
        public int p_majority { get; set; }
        public int p_minority { get; set; }
    }
}

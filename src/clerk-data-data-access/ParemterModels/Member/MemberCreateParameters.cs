using System;
using System.Collections.Generic;
using System.Text;

namespace clerk_data_data_access.ParemterModels.Member
{
    internal class MemberCreateParameters
    {
        public string p_state_district { get; set; }
        public string p_bioguide_id { get; set; }
        public string p_last_name { get; set; }
        public string p_first_name { get; set; }
        public string p_middle_name { get; set; }
        public string p_suffix { get; set; }
        public string p_courtesy { get; set; }
        public string p_name_list { get; set; }
        public string p_sort_name { get; set; }
        public string p_official_name { get; set; }
        public string p_formal_name { get; set; }
        public int p_prior_congress { get; set; }
        public string p_party { get; set; }
        public string p_caucus { get; set; }
        public string p_state { get; set; }
        public string p_district { get; set; }
        public string p_town_name { get; set; }
        public string p_office_building { get; set; }
        public string p_office_room { get; set; }
        public string p_office_zip { get; set; }
        public string p_office_zip_suffix { get; set; }
        public string p_phone_number { get; set; }
        public string p_elected_date { get; set; }
        public string p_sworn_date { get; set; }
    }
}

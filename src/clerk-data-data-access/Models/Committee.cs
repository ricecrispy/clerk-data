using System.Collections.Generic;

namespace clerk_data_data_access.Models
{
    public class Committee
    {
        public string Type { get; set; }
        public string Code { get; set; }
        public int Room { get; set; }
        public string HeaderText { get; set; }
        public int Zip { get; set; }
        public int ZipSuffix { get; set; }
        public string BuildingCode { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public CommitteeRatio Ratio { get; set; }
        public List <Committee> SubCommittees { get; set; }
    }
}
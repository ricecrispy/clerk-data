using clerk_data_data_access.Enums;

namespace clerk_data_data_access.Models
{
    public class TitleInfo
    {
        public int CongressNum { get; set; }
        public string CongressText { get; set; }
        public int Session { get; set; }
        public PoliticalParty Majority { get; set; }
        public PoliticalParty Minority { get; set; }
        public string Clerk { get; set; }
        public string WebUrl { get; set; }
    }
}
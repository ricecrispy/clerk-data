using clerk_data_data_access.Enums;
using System;

namespace clerk_data_data_access.Models
{
    public class MemberInfo
    {
        public string BioGuideId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Courtesy { get; set; }
        public string NameList { get { return $"{LastName}, {FirstName}"; } }
        public string SortName { get { return $"{LastName},{FirstName}".ToUpper(); } }
        public string OfficialName { get; set; } // FirstName MiddleName LastName, Suffix
        public string FormalName { get; set; } // can be anything i.e. Mr. Lawson of Florida
        public int PriorCongress { get; set; }
        public PoliticalParty Party { get; set; }
        public PoliticalParty Caucus { get; set; }
        public State State { get; set; }
        public string District { get; set; }
        public string TownName { get; set; }
        public string OfficeBuilding { get; set; }
        public int OfficeRoom { get; set; }
        public int OfficeZip { get; set; }
        public int OfficeZipSuffix { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime ElectedDate { get; set; }
        public DateTime SwornDate { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [XmlRoot("committee")]
    [Serializable]
    public class Committee
    {
        [XmlAttribute("type")]
        public string Type { get; set; }
        [XmlAttribute("comcode")]
        public string Code { get; set; }
        [XmlAttribute("com-room")]
        public string Room { get; set; }
        [XmlAttribute("com-header-text")]
        public string HeaderText { get; set; }
        [XmlAttribute("com-zip")]
        public string Zip { get; set; }
        [XmlAttribute("com-zip-suffix")]
        public string ZipSuffix { get; set; }
        [XmlAttribute("com-building-code")]
        public string BuildingCode { get; set; }
        [XmlAttribute("com-phone")]
        public string Phone { get; set; }
        [XmlElement("committee-fullname")]
        public string FullName { get; set; }
        [XmlElement("ratio")]
        public CommitteeRatio Ratio { get; set; }
        [XmlElement("subcommittee")]
        public List<SubCommittee> SubCommittees { get; set; }
    }

    public class CommitteeDb : Committee
    {
        public int CommitteeMajority { get; set; }
        public int CommitteeMinority { get; set; }

        public Committee ConvertToCommitteeWithEmptySubCommittees()
        {
            CommitteeRatio ratio = new CommitteeRatio
            {
                Majority = CommitteeMajority,
                Minority = CommitteeMinority
            };

            return new Committee
            {
                Type = Type,
                Code = Code,
                Room = Room,
                HeaderText = HeaderText,
                Zip = Zip,
                ZipSuffix = ZipSuffix,
                BuildingCode = BuildingCode,
                Phone = Phone,
                FullName = FullName,
                Ratio = ratio
            };
        }
    }
}
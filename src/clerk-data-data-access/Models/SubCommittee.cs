using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class SubCommittee
    {
        [XmlAttribute("subcomcode")]
        public string SubComCode { get; set; }
        [XmlAttribute("subcom-room")]
        public string SubComRoom { get; set; }
        [XmlAttribute("subcom-zip")]
        public string SubComZip { get; set; }
        [XmlAttribute("subcom-zip-suffix")]
        public string SubComZipSuffix { get; set; }
        [XmlAttribute("subcom-building-code")]
        public string SubComBuildingCode { get; set; }
        [XmlAttribute("subcom-phone")]
        public string SubComPhone { get; set; }
        [XmlElement("subcommittee-fullname")]
        public string SubComFullName { get; set; }
        [XmlElement("ratio")]
        public CommitteeRatio Ratio { get; set; }
    }

    public class SubCommitteeDb : SubCommittee
    {
        public int CommitteeMajority { get; set; }
        public int CommitteeMinority { get; set; }

        public SubCommittee ConvertToSubCommittee()
        {
            CommitteeRatio ratio = new CommitteeRatio
            {
                Majority = CommitteeMajority,
                Minority = CommitteeMinority
            };

            return new SubCommittee
            {
                SubComCode = SubComCode,
                SubComRoom = SubComRoom,
                SubComZip = SubComZip,
                SubComZipSuffix = SubComZipSuffix,
                SubComBuildingCode = SubComBuildingCode,
                SubComPhone = SubComPhone,
                SubComFullName = SubComFullName,
                Ratio = ratio
            };
        }
    }
}

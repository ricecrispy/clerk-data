﻿using System;
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
        public int SubComZip { get; set; }
        [XmlAttribute("subcom-zip-suffix")]
        public int SubComZipSuffix { get; set; }
        [XmlAttribute("subcom-building-code")]
        public string SubComBuildingCode { get; set; }
        [XmlAttribute("subcom-phone")]
        public string SubComPhone { get; set; }
        [XmlElement("subcommittee-fullname")]
        public string SubComFullName { get; set; }
        [XmlElement("ratio")]
        public CommitteeRatio Ratio { get; set; }
    }
}
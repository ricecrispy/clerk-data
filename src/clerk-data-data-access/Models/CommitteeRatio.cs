using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    public class CommitteeRatio
    {
        [XmlElement("majority")]
        public int Majority { get; set; }
        [XmlElement("minority")]
        public int Minority { get; set; }
    }
}
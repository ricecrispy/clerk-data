using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    /// <summary>
    /// The data model for the ratio element 
    /// under a committee element in the committees element.
    /// </summary>
    [Serializable]
    public class CommitteeRatio
    {
        [XmlElement("majority")]
        public int Majority { get; set; }
        [XmlElement("minority")]
        public int Minority { get; set; }
    }
}
using clerk_data_data_access.Enums;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    public class TitleInfo
    {
        [XmlElement("congress-num")]
        public int CongressNum { get; set; }
        [XmlElement("congress-text")]
        public string CongressText { get; set; }
        [XmlElement("session")]
        public int Session { get; set; }
        [XmlElement("majority")]
        public string Majority { get; set; }
        [XmlElement("minority")]
        public string Minority { get; set; }
        [XmlElement("clerk")]
        public string Clerk { get; set; }
        [XmlElement("weburl")]
        public string WebUrl { get; set; }
    }
}
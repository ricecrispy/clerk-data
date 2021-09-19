using clerk_data_data_access.Enums;
using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    /// <summary>
    /// The data model for the member-info element 
    /// under the member element.
    /// </summary>
    [Serializable]
    public class MemberInfo
    {
        [XmlElement(ElementName = "bioguideID")]
        public string BioGuideId { get; set; }
        [XmlElement(ElementName = "lastname")]
        public string LastName { get; set; }
        [XmlElement(ElementName = "firstname")]
        public string FirstName { get; set; }
        [XmlElement(ElementName = "middlename")]
        public string MiddleName { get; set; }
        [XmlElement(ElementName = "suffix")]
        public string Suffix { get; set; }
        [XmlElement(ElementName = "courtesy")]
        public string Courtesy { get; set; }
        [XmlElement(ElementName = "namelist")]
        public string NameList { get; set; }
        [XmlElement(ElementName = "sort-name")]
        public string SortName { get; set; }
        [XmlElement(ElementName = "official-name")]
        public string OfficialName { get; set; }
        [XmlElement(ElementName = "formal-name")]
        public string FormalName { get; set; }
        [XmlElement(ElementName = "prior-congress")]
        public int PriorCongress { get; set; }
        [XmlElement(ElementName = "party")]
        public string Party { get; set; }
        [XmlElement(ElementName = "caucus")]
        public string Caucus { get; set; }
        [XmlElement(ElementName = "state")]
        public State State { get; set; }
        [XmlElement(ElementName = "district")]
        public string District { get; set; }
        [XmlElement(ElementName = "townname")]
        public string TownName { get; set; }
        [XmlElement(ElementName = "office-building")]
        public string OfficeBuilding { get; set; }
        [XmlElement(ElementName = "office-room")]
        public int OfficeRoom { get; set; }
        [XmlElement(ElementName = "office-zip")]
        public string OfficeZip { get; set; }
        [XmlElement(ElementName = "office-zip-suffix")]
        public string OfficeZipSuffix { get; set; }
        [XmlElement(ElementName = "phone")]
        public string PhoneNumber { get; set; }
        [XmlElement(ElementName = "elected-date")]
        public string ElectedDate { get; set; }
        [XmlElement(ElementName = "sworn-date")]
        public string SwornDate { get; set; }
    }
}
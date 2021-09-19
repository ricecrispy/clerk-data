using System;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    /// <summary>
    /// The data model for the state element 
    /// under the member-info element.
    /// </summary>
    [Serializable]
    public class State
    {
        [XmlAttribute(AttributeName = "postal-code")]
        public string PostalCode { get; set; }
        [XmlElement(ElementName = "state-fullname")]
        public string FullName { get; set; }
    }
}
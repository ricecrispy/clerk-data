using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    [Serializable]
    [XmlRoot("MemberData")]
    public class MemberData
    {
        [XmlAttribute("publish-date")]
        public string PublishDate { get; set; }

        [XmlElement("title-info")]
        public TitleInfo TitleInfo { get; set; } = new TitleInfo();
        
        [XmlArray("members")]
        [XmlArrayItem("member")]
        public List<Member> Members { get; set; }
        
        [XmlArray("committees")]
        [XmlArrayItem("committee")]
        public List<Committee> Committees { get; set; }
    }

    public class MemberDataDb: MemberData
    {
        public int DbCongressNum { get; set; }
        public string DbCongressText { get; set; }
        public int DbSession { get; set; }
        public string DbMajority { get; set; }
        public string DbMinority { get; set; }
        public string DbClerk { get; set; }
        public string DbWebUrl { get; set; }

        public MemberData ConvertToMemberDataWithTitleInfo()
        {
            TitleInfo titleInfo = new TitleInfo
            {
                CongressNum = DbCongressNum,
                CongressText = DbCongressText,
                Session = DbSession,
                Majority = DbMajority,
                Minority = DbMinority,
                Clerk = DbClerk,
                WebUrl = DbWebUrl
            };
            return new MemberData
            {
                PublishDate = PublishDate,
                TitleInfo = titleInfo
            };
        }
    }
}

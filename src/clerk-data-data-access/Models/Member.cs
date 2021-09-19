using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace clerk_data_data_access.Models
{
    /// <summary>
    /// The data model for the member element.
    /// </summary>
    [Serializable]
    public class Member
    {
        [XmlElement("statedistrict")]
        public string StateDistrict { get; set; }
        [XmlElement("member-info")]
        public MemberInfo MemberInfo { get; set; }
        [XmlArray("committee-assignments")]
        [XmlArrayItem("committee", Type = typeof(CommitteeAssignment))]
        [XmlArrayItem("subcommittee", Type = typeof(SubCommitteeAssignment))]
        public List<CommitteeAssignment> CommitteeAssignments { get; set; }
    }

    public class MemberDb: Member
    {
        public string StateCode { get; set; }
        public string BioGuideId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string Suffix { get; set; }
        public string Courtesy { get; set; }
        public string NameList { get; set; }
        public string SortName { get; set; }
        public string OfficialName { get; set; }
        public string FormalName { get; set; }
        public int PriorCongress { get; set; }
        public string Party { get; set; }
        public string Caucus { get; set; }
        public string District { get; set; }
        public string TownName { get; set; }
        public string OfficeBuilding { get; set; }
        public int OfficeRoom { get; set; }
        public string OfficeZip { get; set; }
        public string OfficeZipSuffix { get; set; }
        public string PhoneNumber { get; set; }
        public string ElectedDate { get; set; }
        public string SwornDate { get; set; }

        public Member ConvertToMemberWithNoCommitteeAndStateFullName()
        {
            State state = new State
            {
                PostalCode = StateCode
            };
            MemberInfo memberInfo = new MemberInfo
            {
                BioGuideId = BioGuideId,
                LastName = LastName,
                FirstName = FirstName,
                MiddleName = MiddleName,
                Suffix = Suffix,
                Courtesy = Courtesy,
                NameList = NameList,
                SortName = SortName,
                OfficialName = OfficialName,
                FormalName = FormalName,
                PriorCongress = PriorCongress,
                Party = Party,
                Caucus = Caucus,
                State = state,
                District = District,
                TownName = TownName,
                OfficeBuilding = OfficeBuilding,
                OfficeRoom = OfficeRoom,
                OfficeZip = OfficeZip,
                OfficeZipSuffix = OfficeZipSuffix,
                PhoneNumber = PhoneNumber,
                ElectedDate = ElectedDate,
                SwornDate = SwornDate
            };
            return new Member
            {
                StateDistrict = StateDistrict,
                MemberInfo = memberInfo,
            };
        }
    }
}
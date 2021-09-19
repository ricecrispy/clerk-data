using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface IMemberDataRepository
    {
        Task CreateMemberData(string publishData, TitleInfo titleInfo);
        Task AssociateMemberToMemberDataAsync(int congressNum, string bioGuideId, int session);
        Task AssociateCommitteeToMemberDataAsync(int congressNum, string code, int session);
        Task<IEnumerable<MemberData>> SearchMemberDataAsync();
        Task<MemberData> GetMemberDataAsync(int congressNum, int session);
        Task<IEnumerable<Member>> GetAssociatedMembersAsync(int congressNum, int session);
        Task<IEnumerable<Committee>> GetAssociatedCommitteesAsync(int congressNum, int session);
    }
}

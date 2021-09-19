using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface IMemberRepository
    {
        Task CreateMemberAsync(Member member);
        Task<Member> GetMemberAsync(string memberBioGuideId);
        Task<IEnumerable<Member>> SearchMembersAsync();
        Task UpdateMemberAsync(string memberBioGuideId, Member member);
        Task DeleteMemberAsync(string memberBioGuideId);
        Task<Member> GetStateAndCommitteeAssignmentsAsync(Member member);
    }
}

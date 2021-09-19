using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface IMemberRepository
    {
        /// <summary>
        /// Create a Member object in the database.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task CreateMemberAsync(Member member);
        /// <summary>
        /// Retrieve a Member object with the matching bioguide ID.
        /// </summary>
        /// <param name="memberBioGuideId"></param>
        /// <returns></returns>
        Task<Member> GetMemberAsync(string memberBioGuideId);
        /// <summary>
        /// Search and retrieve all Member objects.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<Member>> SearchMembersAsync();
        /// <summary>
        /// Update a Member object with matching bioguide ID.
        /// </summary>
        /// <param name="memberBioGuideId"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        Task UpdateMemberAsync(string memberBioGuideId, Member member);
        /// <summary>
        /// Delete a Member object with matching bioguide ID.
        /// </summary>
        /// <param name="memberBioGuideId"></param>
        /// <returns></returns>
        Task DeleteMemberAsync(string memberBioGuideId);
        /// <summary>
        /// Add state information and CommitteeAssignment objects to a Member object.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        Task<Member> GetStateAndCommitteeAssignmentsAsync(Member member);
    }
}

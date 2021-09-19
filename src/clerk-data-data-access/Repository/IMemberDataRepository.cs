using clerk_data_data_access.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public interface IMemberDataRepository
    {
        /// <summary>
        /// Create a MemerData object in the database.
        /// </summary>
        /// <param name="publishData"></param>
        /// <param name="titleInfo"></param>
        /// <returns></returns>
        Task CreateMemberData(string publishData, TitleInfo titleInfo);
        /// <summary>
        /// Add an association between a Member and MemberData object in the database.
        /// </summary>
        /// <param name="congressNum"></param>
        /// <param name="bioGuideId"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Task AssociateMemberToMemberDataAsync(int congressNum, string bioGuideId, int session);
        /// <summary>
        /// Add an association between a Committee and MemberData object in the database.
        /// </summary>
        /// <param name="congressNum"></param>
        /// <param name="code"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Task AssociateCommitteeToMemberDataAsync(int congressNum, string code, int session);
        /// <summary>
        /// Search and retrieve all MemberData objects.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MemberData>> SearchMemberDataAsync();
        /// <summary>
        /// Get a MemberData object with matching congress number and session.
        /// </summary>
        /// <param name="congressNum"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<MemberData> GetMemberDataAsync(int congressNum, int session);
        /// <summary>
        /// Get all members that are associated to a MemberData object by congress number and session.
        /// </summary>
        /// <param name="congressNum"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<IEnumerable<Member>> GetAssociatedMembersAsync(int congressNum, int session);
        /// <summary>
        /// Get all Committees that are associated to a MemberData object by congress number and session.
        /// </summary>
        /// <param name="congressNum"></param>
        /// <param name="session"></param>
        /// <returns></returns>
        Task<IEnumerable<Committee>> GetAssociatedCommitteesAsync(int congressNum, int session);
    }
}

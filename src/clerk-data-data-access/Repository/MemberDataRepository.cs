using clerk_data_data_access.Factory;
using clerk_data_data_access.FluentMap;
using clerk_data_data_access.Models;
using clerk_data_data_access.ParameterModel.MemberData;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public class MemberDataRepository : IMemberDataRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MemberDataRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        static MemberDataRepository()
        {
            FluentMapInitializer.EnsureMapsInitialized();
        }

        public async Task AssociateCommitteeToMemberDataAsync(int congressNum, string code, int session)
        {
            var parameters = new MemberDataAssociateCommitteeParameters
            {
                p_congress_num = congressNum,
                p_committee_code = code,
                p_session = session
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            await connection.QueryAsync(
                "data.udf_associate_committee_memberdata",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task AssociateMemberToMemberDataAsync(int congressNum, string bioGuideId, int session)
        {
            var parameters = new MemberDataAssociateMemberParameters
            {
                p_congress_num = congressNum,
                p_member_biograde_id = bioGuideId,
                p_session = session
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            await connection.QueryAsync(
                "data.udf_associate_member_memberdata",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task CreateMemberData(string publishData, TitleInfo titleInfo)
        {
            var parameters = new MemberDataUpsertParameters
            {
                p_publish_date = publishData,
                p_congress_num = titleInfo.CongressNum,
                p_congress_text = titleInfo.CongressText,
                p_session = titleInfo.Session,
                p_majority = titleInfo.Majority,
                p_minority = titleInfo.Minority,
                p_clerk = titleInfo.Clerk,
                p_web_url = titleInfo.WebUrl
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            await connection.QueryAsync(
                "info.udf_create_memberdata",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<MemberData> GetMemberDataAsync(int congressNum, int session)
        {
            var parameters = new MemberDataGetByCongressNumAndSessionParameters
            {
                p_congress_num = congressNum,
                p_session = session
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            return await connection.QuerySingleOrDefaultAsync<MemberData>(
                "info.udf_select_memberdata_by_congress_num_and_session",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<MemberData>> SearchMemberDataAsync()
        {
            using var connection = _connectionFactory.GetDataBaseConnection();
            IEnumerable<MemberDataDb> memberDataDbList = await connection.QueryAsync<MemberDataDb>(
                "info.udf_select_memberdata",
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);

            List<MemberData> results = memberDataDbList.Select(x => x.ConvertToMemberDataWithTitleInfo()).ToList();
            return results;
        }

        public async Task<IEnumerable<Member>> GetAssociatedMembersAsync(int congressNum, int session)
        {
            using var connection = _connectionFactory.GetDataBaseConnection();
            var parameters = new MemberDataGetAssociationParameters
            {
                p_congress_num = congressNum,
                p_session = session
            };
            IEnumerable<MemberDb> members = await connection.QueryAsync<MemberDb>(
                "data.udf_select_memberdata_members_by_congress_num_and_session",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
            IEnumerable<Member> results = members.Select(x => x.ConvertToMemberWithNoCommitteeAndStateFullName());
            return results;
            
        }

        public async Task<IEnumerable<Committee>> GetAssociatedCommitteesAsync(int congressNum, int session)
        {
            using var connection = _connectionFactory.GetDataBaseConnection();
            var parameters = new MemberDataGetAssociationParameters
            {
                p_congress_num = congressNum,
                p_session = session
            };
            IEnumerable<CommitteeDb> committees = await connection.QueryAsync<CommitteeDb>(
                "data.udf_select_memberdata_committees_by_congress_num_and_session",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
            IEnumerable<Committee> results = committees.Select(x => x.ConvertToCommitteeWithEmptySubCommittees());
            return results;
        }
    }
}

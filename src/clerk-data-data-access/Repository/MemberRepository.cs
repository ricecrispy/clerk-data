using clerk_data_data_access.Factory;
using clerk_data_data_access.FluentMap;
using clerk_data_data_access.Models;
using clerk_data_data_access.ParemterModels.Member;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public class MemberRepository : IMemberRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public MemberRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        static MemberRepository()
        {
            FluentMapInitializer.EnsureMapsInitialized();
        }

        public async Task CreateMemberAsync(Member member)
        {
            using (var connection = _connectionFactory.GetDataBaseConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new MemberUpsertParameters
                        {
                            p_state_district = member.StateDistrict,
                            p_bioguide_id = member.MemberInfo.BioGuideId,
                            p_last_name = member.MemberInfo.LastName,
                            p_first_name = member.MemberInfo.FirstName,
                            p_middle_name = member.MemberInfo.MiddleName,
                            p_suffix = member.MemberInfo.Suffix,
                            p_courtesy = member.MemberInfo.Courtesy,
                            p_name_list = member.MemberInfo.NameList,
                            p_sort_name = member.MemberInfo.SortName,
                            p_official_name = member.MemberInfo.OfficialName,
                            p_formal_name = member.MemberInfo.FormalName,
                            p_prior_congress = member.MemberInfo.PriorCongress,
                            p_party = member.MemberInfo.Party,
                            p_caucus = member.MemberInfo.Caucus,
                            p_state = member.MemberInfo.State.PostalCode,
                            p_district = member.MemberInfo.District,
                            p_town_name = member.MemberInfo.TownName,
                            p_office_building = member.MemberInfo.OfficeBuilding,
                            p_office_room = member.MemberInfo.OfficeRoom,
                            p_office_zip = member.MemberInfo.OfficeZip,
                            p_office_zip_suffix = member.MemberInfo.OfficeZipSuffix,
                            p_phone_number = member.MemberInfo.PhoneNumber,
                            p_elected_date = member.MemberInfo.ElectedDate,
                            p_sworn_date = member.MemberInfo.SwornDate
                        };

                        await connection.QueryAsync(
                            "clerkdata.udf_create_member",
                            parameters,
                            commandTimeout: _connectionFactory.CommandTimeout,
                            commandType: CommandType.StoredProcedure);

                        foreach (var committeeAssignment in member.CommitteeAssignments)
                        {
                            bool isSubCommittee = committeeAssignment is SubCommitteeAssignment;
                            var commCode = isSubCommittee ? 
                                ((SubCommitteeAssignment)committeeAssignment).SubCommitteeCode : 
                                committeeAssignment.CommitteeCode;

                            var associationParmaters
                                = new MemberAssociateCommitteeAssignmentParamters
                            {
                                p_bioguide_id = member.MemberInfo.BioGuideId,
                                p_committee_code = commCode,
                                p_is_sub_committee = isSubCommittee,
                                p_rank = committeeAssignment.Rank
                            };

                            await connection.QueryAsync(
                                "clerkdata.udf_associate_member_committeeAssignment",
                                associationParmaters,
                                commandTimeout: _connectionFactory.CommandTimeout,
                                commandType: CommandType.StoredProcedure);
                        }

                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public Task DeleteMemberAsync(string memberBioGuideId)
        {
            throw new NotImplementedException();
        }

        public async Task<Member> GetMemberAsync(string memberBioGuideId)
        {
            var parameters = new MemberGetByBioBuideIdParameters
            {
                p_bioguide_id = memberBioGuideId
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            return await connection.QuerySingleOrDefaultAsync<Member>(
                "clerkdata.udf_select_member_by_bioguide_id",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Member>> SearchMembersAsync()
        {
            using var connection = _connectionFactory.GetDataBaseConnection();
            return await connection.QueryAsync<Member>(
                "clerkdata.udf_select_members",
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public Task UpdateMemberAsync(string memberBioGuideId, Member member)
        {
            throw new NotImplementedException();
        }
    }
}

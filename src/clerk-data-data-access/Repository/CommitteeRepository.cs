using clerk_data_data_access.Factory;
using clerk_data_data_access.FluentMap;
using clerk_data_data_access.Models;
using clerk_data_data_access.ParameterModel.Committee;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace clerk_data_data_access.Repository
{
    public class CommitteeRepository : ICommitteeRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public CommitteeRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        static CommitteeRepository()
        {
            FluentMapInitializer.EnsureMapsInitialized();
        }

        public async Task CreateCommitteeAsync(Committee committee)
        {
            using (var connection = _connectionFactory.GetDataBaseConnection())
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        var parameters = new CommitteeUpsertParameters
                        {
                            p_type = committee.Type,
                            p_code = committee.Code,
                            p_room = committee.Room,
                            p_header_text = committee.HeaderText,
                            p_zip = committee.Zip,
                            p_zip_suffix = committee.ZipSuffix,
                            p_building_code = committee.BuildingCode,
                            p_phone = committee.Phone,
                            p_full_name = committee.FullName,
                            p_majority = committee.Ratio.Majority,
                            p_minority = committee.Ratio.Minority
                        };

                        await connection.QueryAsync(
                            "info.udf_create_committee",
                            parameters,
                            commandTimeout: _connectionFactory.CommandTimeout,
                            commandType: CommandType.StoredProcedure);

                        foreach (var subCommittee in committee.SubCommittees)
                        {
                            var subCommParameters = new SubCommitteeUpsertParameters
                            {
                                p_code = subCommittee.SubComCode,
                                p_room = subCommittee.SubComRoom,
                                p_zip = subCommittee.SubComZip,
                                p_zip_suffix = subCommittee.SubComZipSuffix,
                                p_building_code = subCommittee.SubComBuildingCode,
                                p_phone = subCommittee.SubComPhone,
                                p_full_name = subCommittee.SubComFullName,
                                p_majority = subCommittee.Ratio.Majority,
                                p_minority = subCommittee.Ratio.Minority
                            };

                            await connection.QueryAsync(
                                "info.udf_create_sub_committee",
                                subCommParameters,
                                commandTimeout: _connectionFactory.CommandTimeout,
                                commandType: CommandType.StoredProcedure);

                            var associationParameters = new CommitteeAssociateSubCommitteeParameters
                            {
                                p_committee_code = committee.Code,
                                p_sub_committee_code = subCommittee.SubComCode
                            };

                            await connection.QueryAsync(
                                "data.udf_committee_associate_sub_committee",
                                associationParameters,
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

        public async Task<IEnumerable<Committee>> SearchCommitteesAsync()
        {
            using var connection = _connectionFactory.GetDataBaseConnection();
            return await connection.QueryAsync<Committee>(
                "info.udf_select_committees",
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Committee> GetCommitteeByCommitteeCodeAsync(string code)
        {
            var parameters = new CommitteeGetByCommitteeCodeParameters
            {
                p_committee_code = code
            };

            using var connection = _connectionFactory.GetDataBaseConnection();
            return await connection.QuerySingleOrDefaultAsync<Committee>(
                "info.udf_select_committee_by_committee_code",
                parameters,
                commandTimeout: _connectionFactory.CommandTimeout,
                commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateCommitteeAsync(string code, Committee committee)
        {
            throw new NotImplementedException();
            // using (var connection = _connectionFactory.GetDataBaseConnection())
            // {
            //     connection.Open();
            //     using (var transaction = connection.BeginTransaction())
            //     {
            //         try
            //         {
            //             var parameters = new CommitteeUpsertParameters
            //             {
            //                 p_type = committee.Type,
            //                 p_code = committee.Code,
            //                 p_room = committee.Room,
            //                 p_header_text = committee.HeaderText,
            //                 p_zip = committee.Zip,
            //                 p_zip_suffix = committee.ZipSuffix,
            //                 p_building_code = committee.BuildingCode,
            //                 p_phone = committee.Phone,
            //                 p_full_name = committee.FullName,
            //                 p_majority = committee.Ratio.Majority,
            //                 p_minority = committee.Ratio.Minority
            //             };

            //             await connection.QueryAsync(
            //                 "info.udf_update_committee",
            //                 parameters,
            //                 commandTimeout: _connectionFactory.CommandTimeout,
            //                 commandType: CommandType.StoredProcedure);

            //             foreach (var subCommittee in committee.SubCommittees)
            //             {
            //                 var subCommParameters = new SubCommitteeUpsertParameters
            //                 {
            //                     p_code = subCommittee.SubComCode,
            //                     p_room = subCommittee.SubComRoom,
            //                     p_zip = subCommittee.SubComZip,
            //                     p_zip_suffix = subCommittee.SubComZipSuffix,
            //                     p_building_code = subCommittee.SubComBuildingCode,
            //                     p_phone = subCommittee.SubComPhone,
            //                     p_full_name = subCommittee.SubComFullName,
            //                     p_majority = subCommittee.Ratio.Majority,
            //                     p_minority = subCommittee.Ratio.Minority
            //                 };

            //                 await connection.QueryAsync(
            //                     "info.udf_update_sub_committee",
            //                     subCommParameters,
            //                     commandTimeout: _connectionFactory.CommandTimeout,
            //                     commandType: CommandType.StoredProcedure);

            //                 //var associationParameters = new CommitteeAssociateSubCommitteeParameters
            //                 //{
            //                 //    p_committee_code = committee.Code,
            //                 //    p_sub_committee_code = subCommittee.SubComCode
            //                 //};

            //                 //await connection.QueryAsync(
            //                 //    "clerkdata.udf_committee_associate_sub_committee",
            //                 //    associationParameters,
            //                 //    commandTimeout: _connectionFactory.CommandTimeout,
            //                 //    commandType: CommandType.StoredProcedure);
            //             }

            //             transaction.Commit();
            //         }
            //         catch
            //         {
            //             transaction.Rollback();
            //             throw;
            //         }
            //     }
            // }
        }
    }
}

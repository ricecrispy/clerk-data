using clerk_data_data_access.Models;
using clerk_data_data_access.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace clerk_data_service.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MemberDataController : ControllerBase
    {
        private readonly ILogger<MemberDataController> _logger;
        private readonly IMemberDataRepository _memberDataRepo;
        private readonly IMemberRepository _memberRepo;
        private readonly ICommitteeRepository _committeeRepo;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly XmlSerializer _xmlSerializer;

        public MemberDataController(
            ILogger<MemberDataController> logger, 
            IMemberDataRepository memberDataRepo,
            IMemberRepository memberRepo,
            ICommitteeRepository committeeRepo,
            IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _memberDataRepo = memberDataRepo;
            _memberRepo = memberRepo;
            _committeeRepo = committeeRepo;
            _httpClientFactory = httpClientFactory;
            _xmlSerializer = new XmlSerializer(typeof(MemberData));
        }

        /// <summary>
        /// Create a MemberData object and stores it in the datastore.
        /// </summary>
        /// <param name="xmlUrl">The url of the XML file.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> UploadMemberDataXmlByUrlAsync(string xmlUrl)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, xmlUrl);
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                MemberData content = _xmlSerializer.Deserialize(responseStream) as MemberData;
                await UploadFileContent(content);
                _logger.LogError("{controller}.{method}({xmlUrl}) Succeeded.",
                    nameof(MemberDataController),
                    nameof(UploadMemberDataXmlByUrlAsync),
                    xmlUrl);
                return NoContent();
            }
            else
            {

                _logger.LogError("{controller}.{method}({xmlUrl}) Failed. {responseMessage}",
                    nameof(MemberDataController),
                    nameof(UploadMemberDataXmlByUrlAsync),
                    xmlUrl,
                    response.ReasonPhrase);
                return BadRequest();
            }
        }

        private async Task UploadFileContent(MemberData memberData)
        {
            var committees = memberData.Committees;
            var members = memberData.Members;
            await _memberDataRepo.CreateMemberData(memberData.PublishData, memberData.TitleInfo);
            await AssociateCommitteesToMemberDataAsync(memberData.TitleInfo.CongressNum, committees);
            await AssociateMembersToMemberDataAsync(memberData.TitleInfo.CongressNum, members);
        }

        private async Task AssociateMembersToMemberDataAsync(int congressNum, List<Member> members)
        {
            IEnumerable<Member> existingMembers = await _memberRepo.SearchMembersAsync();
            foreach (var member in members)
            {
                if (!existingMembers.Any(x => x.MemberInfo.BioGuideId == member.MemberInfo.BioGuideId))
                {
                    await _memberRepo.CreateMemberAsync(member);
                }
                else
                {
                    await _memberRepo.UpdateMemberAsync(member.MemberInfo.BioGuideId, member);
                }
                await _memberDataRepo.AssociateMemberToMemberDataAsync(congressNum, member.MemberInfo.BioGuideId);
            }
        }

        private async Task AssociateCommitteesToMemberDataAsync(int congressNum, List<Committee> committees)
        {
            IEnumerable<Committee> existingCommittees = await _committeeRepo.SearchComitteesAsync();
            foreach (var committee in committees)
            {
                if (!existingCommittees.Any(x => x.Code == committee.Code))
                {
                    await _committeeRepo.CreateComitteeAsync(committee);
                }
                else
                {
                    await _committeeRepo.UpdateComitteeAsync(committee.Code, committee);
                }
                await _memberDataRepo.AssociateCommitteeToMemberDataAsync(congressNum, committee.Code);
            }
        }
    }
}

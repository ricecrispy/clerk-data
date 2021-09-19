using clerk_data_data_access.Models;
using clerk_data_data_access.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace clerk_data_service.Controllers
{
    /// <summary>
    /// Controller for memberData.
    /// </summary>
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

        /// <summary>
        /// The constructor for MemberDataController
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="memberDataRepo"></param>
        /// <param name="memberRepo"></param>
        /// <param name="committeeRepo"></param>
        /// <param name="httpClientFactory"></param>
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
        /// <response code="204">MemberData file uploaded</response>
        /// <response code="400">MemberData file cannot be retrieved</response>
        /// <response code="500">Unexpected error</response>
        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<ActionResult> UploadMemberDataXmlByUrlAsync(string xmlUrl)
        {
            if (string.IsNullOrWhiteSpace(xmlUrl) ||
                !Uri.TryCreate(xmlUrl, UriKind.RelativeOrAbsolute, out _))
            {
                _logger.LogError("{controller}.{method}({xmlUrl}) Failed. Invalid url",
                    nameof(MemberDataController),
                    nameof(UploadMemberDataXmlByUrlAsync),
                    xmlUrl);
                return BadRequest();
            }

            var httpClient = _httpClientFactory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Get, xmlUrl);
            var response = await httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                MemberData content = _xmlSerializer.Deserialize(responseStream) as MemberData;
                await UploadFileContent(content);
                _logger.LogInformation("{controller}.{method}({xmlUrl}) Succeeded.",
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

        /// <summary>
        /// Return a list of MemberData
        /// </summary>
        /// <response code="200">List of MemberData retrieved</response>
        /// <response code="404">MemberData not found</response>
        /// <response code="500">Unexpected error</response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MemberData>), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public async Task<ActionResult<IEnumerable<MemberData>>> SearchMemberDataAsync()
        {
            IEnumerable<MemberData> memberDataList = await _memberDataRepo.SearchMemberDataAsync();
            if (memberDataList == null || !memberDataList.Any())
            {
                _logger.LogError("{controller}.{method}() Failed. No such MemberData",
                    nameof(MemberDataController),
                    nameof(SearchMemberDataAsync));
                return NotFound();
            }

            foreach (var memberData in memberDataList)
            {
                IEnumerable<Member> memberList = 
                    await _memberDataRepo.GetAssociatedMembersAsync(memberData.TitleInfo.CongressNum, memberData.TitleInfo.Session);
                if (memberList.Any())
                {
                    memberData.Members = await ProcessMemberList(memberList);
                }
                
                IEnumerable<Committee> committeeList =
                    await _memberDataRepo.GetAssociatedCommitteesAsync(memberData.TitleInfo.CongressNum, memberData.TitleInfo.Session);
                if (committeeList.Any())
                {
                    memberData.Committees = await ProcessCommitteeList(committeeList); ;
                }
            }
            
            return memberDataList.ToList();
        }

        /// <summary>
        /// Return a MemberData by congressNum
        /// </summary>
        /// <param name="congressNum">The congress number in the MemberData's TitleInfo property</param>
        /// <param name="session">The session in the MemberData's TitleInfo property</param>
        /// <response code="200">MemberData retrieved</response>
        /// <response code="404">MemberData not found</response>
        /// <response code="500">Unexpected error</response>
        //[HttpGet("{congressNum}/sessions/{session}")]
        //[ProducesResponseType(typeof(MemberData), 200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(500)]
        //public async Task<ActionResult<MemberData>> GetMemberDataAsync(int congressNum, int session)
        //{
        //    MemberData memberData = await _memberDataRepo.GetMemberDataAsync(congressNum, session);
        //    if (memberData == null)
        //    {
        //        _logger.LogError("{controller}.{method}({congressNum}) Failed. No such MemberData",
        //            nameof(MemberDataController),
        //            nameof(GetMemberDataAsync),
        //            congressNum);
        //        return NotFound();
        //    }
        //    return memberData;
        //}

        private async Task<List<Member>> ProcessMemberList(IEnumerable<Member> members)
        {
            List<Member> results = new List<Member>();
            foreach (var member in members)
            {
                results.Add(await _memberRepo.GetStateAndCommitteeAssignmentsAsync(member));
            }
            return results;
        }

        private async Task<List<Committee>> ProcessCommitteeList(IEnumerable<Committee> committees)
        {
            List<Committee> results = new List<Committee>();
            foreach (var committee in committees)
            {
                results.Add(await _committeeRepo.GetSubCommitteesAsync(committee));
            }
            return results;
        }

        private async Task UploadFileContent(MemberData memberData)
        {
            var committees = memberData.Committees;
            var members = memberData.Members;
            await _memberDataRepo.CreateMemberData(memberData.PublishDate, memberData.TitleInfo);
            await AssociateCommitteesToMemberDataAsync(memberData.TitleInfo.CongressNum, committees, memberData.TitleInfo.Session);
            await AssociateMembersToMemberDataAsync(memberData.TitleInfo.CongressNum, members, memberData.TitleInfo.Session);
        }

        private async Task AssociateMembersToMemberDataAsync(int congressNum, List<Member> members, int session)
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
                await _memberDataRepo.AssociateMemberToMemberDataAsync(congressNum, member.MemberInfo.BioGuideId, session);
            }
        }

        private async Task AssociateCommitteesToMemberDataAsync(int congressNum, List<Committee> committees, int session)
        {
            IEnumerable<Committee> existingCommittees = await _committeeRepo.SearchCommitteesAsync();
            foreach (var committee in committees)
            {
                if (!existingCommittees.Any(x => x.Code == committee.Code))
                {
                    await _committeeRepo.CreateCommitteeAsync(committee);
                }
                else
                {
                    await _committeeRepo.UpdateCommitteeAsync(committee.Code, committee);
                }
                await _memberDataRepo.AssociateCommitteeToMemberDataAsync(congressNum, committee.Code, session);
            }
        }
    }
}

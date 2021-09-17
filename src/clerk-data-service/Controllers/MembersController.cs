using clerk_data_data_access.Models;
using clerk_data_data_access.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace clerk_data_service.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class MembersController : ControllerBase
    {
        //private readonly ILogger<MembersController> _logger;
        //private readonly IMemberRepository _repository;

        //public MembersController(ILogger<MembersController> logger, IMemberRepository repository)
        //{
        //    _logger = logger;
        //    _repository = repository;
        //}

        //[HttpPost]
        //public async Task<ActionResult<Guid>> CreateMemberAsync([FromBody] Member member)
        //{
        //    if (!(isStateDistrictValid(member.StateDistrict) &&
        //        isMemberInfoValid(member.MemberInfo) &&
        //        isCommitteeAssignmentsValid(member.CommitteeAssignments)))
        //    {
        //        _logger.LogInformation("{controller}.{method}({member}) failed. Invalid parameters.",
        //            nameof(MembersController),
        //            nameof(CreateMemberAsync),
        //            member);
        //        return BadRequest();
        //    }

        //    Guid bioGradeId = await _repository.CreateMemberAsync(member);
        //    _logger.LogInformation("{controller}.{method} ({member}) succeeded.",
        //        nameof(MembersController),
        //        nameof(CreateMemberAsync),
        //        member);
        //    return bioGradeId;
        //}

        private bool isCommitteeAssignmentsValid(CommitteeAssignments committeeAssignments)
        {
            throw new NotImplementedException();
        }

        private bool isStateDistrictValid(string stateDistrict)
        {
            throw new NotImplementedException();
        }

        private bool isMemberInfoValid(MemberInfo memberInfo)
        {
            throw new NotImplementedException();
        }
    }
}

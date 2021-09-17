using clerk_data_data_access.Models;
using Microsoft.AspNetCore.Mvc;
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
        [HttpPost]
        public Task<Guid> CreateMemberAsync([FromBody] Member member)
        {

        }
    }
}

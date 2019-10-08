using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InviteesController : ControllerBase
    {
        private readonly IInviteService inviteeService;
        public InviteesController(IInviteService inviteesService)
        {
            this.inviteeService = inviteesService;
        }
        // GET: api/Member
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //   // return mService.GetMember();
        //}

        // GET: api/Invitees/{inviteeId}
        [HttpGet("{emailId}")]
        public async Task<ActionResult<IEnumerable<string>>> GetInvites(string emailId)
        {
            return Ok(await inviteeService.GetInvitesAsync(emailId));
        }

        // // POST: api/Invitees
        // [HttpPost]
        // public async Task<IActionResult> Post([FromBody] Invite invitee)
        // {
        //    var createdInvitee = await inviteeService.CreateInvitee(invitee);
        //     return Created("api/invitees", createdInvitee);

        // }

        // PUT: api/invitees/5
        // [HttpPut("{inviteeId}")]
        // public IActionResult UpdateInvitee(string inviteeId, [FromBody] Invite invitee)
        // {
        //     inviteeService.UpdateInvitee(inviteeId, invitee);
        //     return Ok();
        // }
    }
}
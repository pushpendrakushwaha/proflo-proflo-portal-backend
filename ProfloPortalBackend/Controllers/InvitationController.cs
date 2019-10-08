using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.Model;

namespace ProfloPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvitationController : ControllerBase
    {
        private readonly IInviteService inviteeService;
        private readonly ITeamService tService;
        //private Invite invite;
        string emailId;


        public InvitationController(IInviteService inviteService, ITeamService teamService, IHttpContextAccessor context)
        {
            this.inviteeService = inviteService;
            this.tService = teamService;
            //invite = new Invite();
            emailId = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("Email").Value;
            //Console.WriteLine(invite.ToString());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Team>>> Get()
        {
            try
            {
                var teams = await inviteeService.GetInvitedTeamAsync(emailId);
                return Ok(teams);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError, e.Message);
            }
        }

        // GET: api/Invitation/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invitation
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Invitation/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.Model;
using System.Security.Claims;

namespace ProfloPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly IMemberService mService;
        private readonly Member member;
        public MembersController(IMemberService memberService, IHttpContextAccessor context)
        {
            this.mService = memberService;
            member = new Member();
            member.UserId = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("Uid").Value;
            member.MemberName = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("FirstName").Value;
            member.EmailId = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("Email").Value;
        }
        // GET: api/Member
        //[HttpGet]
        //public ActionResult<IEnumerable<string>> Get()
        //{
        //   // return mService.GetMember();
        //}

        // GET: api/Member/{memberId}
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetMember()
        {
            return Ok(mService.GetMemberByUserIdAsync(member.UserId));
        }

        // GET: api/Member/{userId}
        public async Task<ActionResult<IEnumerable<string>>> GetMembersByUserID(string userId)
        {
            return Ok(await mService.GetMemberByUserIdAsync(userId));
        }

        // POST: api/Member
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Member member)
        {
            var createdMember = await mService.CreateMemberIfNotExistsAsync(member);
            return Created("api/members", createdMember);

        }

        // PUT: api/Member/5
        public async Task<IActionResult> UpdateMember([FromBody] Member member)
        {
           
            await mService.UpdateMemberAsync(member);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService tService;
        private Member member;
        public TeamsController(ITeamService teamService, IHttpContextAccessor context)
        {
            tService = teamService;
            member = new Member();
            member.UserId = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("Uid").Value;
            member.MemberName = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("FirstName").Value;
            member.EmailId = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("Email").Value;
            Console.WriteLine(member.ToString());
        }
        #region Team Operations

        // POST api/team
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Team team)
        {
            Console.WriteLine(team);
            var createdTeam = await tService.CreateTeamAsync(team, member);
            return Created("api/Teams", createdTeam);
        }

        // GET api/team
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            try
            {
                var teams = await tService.GetTeamsAsync(member);
                return Ok(teams);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        
        //GET :api/teams/{teamId}
        [HttpGet("{teamId}")]
        public ActionResult<IEnumerable<string>> Get(string teamId)
        {
            try
            {
                return Ok(tService.GetTeamByID(teamId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // PUT /api/teams/id
        [HttpPut("{teamId}")]
        public IActionResult UpdateTeam(string teamId, [FromBody] Team team)
        {
            try
            {
                tService.UpdateTeam(teamId, team);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/team/5
        [HttpDelete]
        [Route("{teamId}")]
        public IActionResult Delete(string teamId)
        {
            return Ok(tService.RemoveTeamById(teamId));
        }
        #endregion

        #region Team Members

        // // POST api/team/{teamId}/Member
        // [HttpPost("{teamId}/Member")]
        // public IActionResult CreateMembers(string teamId, [FromBody] Member member)
        // {
        //     tService.createMembers(teamId, member);
        //     return Created("api/{teamId}/Member", member);
        // }

        // GET api/team/{teamId}/Member
        [HttpGet("{teamId}/Member")]
        public ActionResult<IEnumerable<string>> GetTeamMembers(string teamId)
        {

            try
            {
                return Ok(tService.GetTeamMembers(teamId));
            }
            catch (Exception e)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
        // GET: api/teams/{teamId}/member/{Mid}
        // [HttpGet("{teamId}/Member/{Mid}")]
        // public ActionResult<IEnumerable<string>> GetCardMemberByMemberId(string teamId, string Mid)
        // {
        //     return Ok(tService.GetMemberByMemberId(teamId, Mid));
        // }

        // PUT /api/teams/{teamId}/Member/{memberID}
        [HttpPut("{teamId}/Member/{Mid}")]
        public IActionResult UpdateMember(string teamId, string Mid, [FromBody] Member member)
        {
            try
            {
                tService.UpdateMembers(teamId, Mid, member);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/team/{teamId}/Member/{mid}
        [HttpDelete]
        [Route("{teamId}/Member/{mid}")]
        public IActionResult RemoveMembersByID(string teamId, string mid)
        {
            return Ok(tService.RemoveMembers(teamId, mid));
        }
        #endregion




        //GET TeamBoards
        //GET api/teams/{teamId}/boards
        [HttpGet]
        [Route("{teamId}/boards")]
        public ActionResult<string> GetTeamBoards(string teamId)
        {
            return Ok(tService.getTeamBoards(teamId));
        }
        //[HttpGet("{teamId}")]
        //public IActionResult GetTeamBoardById(string teamId)
        //{
        //    try
        //    {
        //        return Ok(tService.getTeamBoards(teamId));
        //    }
        //    catch (Exception e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //} 

        ////GET api/team/2/board/3
        //[HttpGet]
        //[Route("{teamId}/board/{boardId}")]
        //public IActionResult GetBoardById(int boardId)
        //{
        //    return Ok(tService.GetAllBoards(boardId));

        //}
        ////POST api/team/2/board
        //[HttpGet]
        //[Route("{teamId}/board")]
        //public IActionResult Post(int teamId, [FromBody] Board board)
        //{
        //    try
        //    {
        //        tService.AddBoard(teamId, board);
        //        return Created("api/team", board);
        //    }
        //    catch (Exception e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //}
        ////DELETE api/team/2/board/3
        //[HttpDelete]
        //[Route("{teamId}/board/{boardid}")]
        //public IActionResult Delete(int teamId, int boardId)
        //{// POST api/team/{teamId}/Member
        // [HttpPost("{teamId}/Member")]
        // public IActionResult CreateMembers(string teamId, [FromBody] Member member)
        // {
        //     tService.createMembers(teamId, member);
        //     return Created("api/{teamId}/Member", member);
        // }
        //    try
        //    {
        //        tService.RemoveBoardIdByTeamId(teamId, boardId);
        //        return Ok();
        //    }
        //    catch (Exception e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //}

    }
}
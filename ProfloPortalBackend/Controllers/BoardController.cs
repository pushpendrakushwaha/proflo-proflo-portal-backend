using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using ProfloPortalBackend.BusinessLayer;
using ProfloPortalBackend.Model;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

using System.Security.Claims;

namespace ProfloPortalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoardsController : ControllerBase
    {
        private readonly IBoardService boardService;

        public BoardsController(IBoardService boardService, IHttpContextAccessor context)
        {
            this.boardService = boardService;
            // var value = (context.HttpContext.User.Identity as ClaimsIdentity).FindFirst("<Email|Name|ID>").Value;
        }


        #region Board Operations
        // GET: api/boards
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(boardService.GetBoards());
        }

        //GET : api/boards/{boardId}
        [HttpGet("{boardId}")]
        public async Task<IActionResult> Get(string boardId)
        {
            try
            {
                var board = await boardService.GetBoardById(boardId);
                return Ok(board);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return NotFound(e.Message);
            }
        }

        [HttpPost("{boardId}/list/move")]
        public async Task<IActionResult> MoveCard([FromBody] MoveListRequest moveListRequest)
        {
            try 
            {
                await boardService.MoveList(moveListRequest);
                return Ok();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                throw e;
            }
        }

        // POST: api/boards
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Board board)
        {

            var createdBoard = await boardService.CreateBoard(board);
            return Created("api/Boards", createdBoard);
        }

        // PUT: api/boards/{boardId}
        [HttpPut("{boardId}")]
        public IActionResult UpdateBoardById(string boardId, [FromBody] Board board)
        {
            try
            {
                return Ok(boardService.UpdateBoard(boardId, board));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        //DELETE :api/Boards/{boardId}
        [HttpDelete("{boardId}")]
        public IActionResult RemoveBoardById(string boardId)
        {
            try
            {
                boardService.RemoveBoard(boardId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion

        #region BoardMember

        // POST: api/Boards/{boardId}/member
        [HttpPost("{boardId}/member")]
        public IActionResult CreateMember(string boardId, [FromBody] Member member)
        {
            boardService.CreateMembers(boardId, member);
            return Created("api/{boardId}/member", member);
        }

        //GET : api/boards/{boardId}/member
        [HttpGet("{boardId}/member")]
        public IActionResult GetMembersOfBoard(string boardId)
        {
            try
            {
                return Ok(boardService.GetBoardMembers(boardId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        // GET: api/boards/{boardId}/member/{Mid}
        [HttpGet("{boardId}/Member/{Mid}")]
        public ActionResult<IEnumerable<string>> GetCardMemberByMemberId(string boardId, string Mid)
        {
            return Ok(boardService.GetMemberByMemberId(boardId, Mid));
        }

        // GET: api/boards/{boardId}/member/{Mid}
        [HttpPut("{boardId}/member/{MId}")]
        public IActionResult UpdateMemberById(string boardId, string MId, [FromBody]Member member)
        {
            try
            {
                return Ok(boardService.UpdateMembers(boardId, MId, member));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/boards/{boardId}/Member/{Mid}
        [HttpDelete("{boardId}/member/{MId}")]
        public IActionResult RemoveMemberById(string boardId, string MId)
        {
            boardService.RemoveMembers(boardId, MId);
            return Ok();
        }
        #endregion

        #region BoardInvite

        // POST: api/boards/{boardId}/Invitee
        [HttpPost("{boardId}/Invitee")]
        public IActionResult CreateInvite(string boardId, [FromBody] Invite invitee)
        {
            boardService.CreateInvite(boardId, invitee);
            return Created("api/board/{boardId}/Invitee", invitee);
        }

        //GET : api/boards/{boardId}/invitee
        [HttpGet("{boardId}/invitee")]
        public IActionResult GetInviteeOfBoard(string boardId)
        {
            try
            {
                return Ok(boardService.GetBoardInvites(boardId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/boards/{boardId}/Invitee/{inviteId}
        [HttpPut("{boardId}/invite/{inviteId}")]
        public IActionResult UpdateInviteById(string boardId, string inviteId, [FromBody]Invite invitee)
        {
            try
            {
                return Ok(boardService.UpdateInvite(boardId, inviteId, invitee));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        // GET: api/Boards/{boardId}/invitee/{inviteId}
        [HttpDelete("{boardID}/invitee/{inviteId}")]
        public IActionResult RemoveInviteById(string boardId, string inviteId)
        {
            try
            {
                boardService.RemoveInvite(boardId, inviteId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        #endregion
        //GET BoardList
        //GET api/boards/{boardId}/Lists
        [HttpGet]
        [Route("{boardId}/Lists")]
        public ActionResult<string> GetBoardLists(string boardId)
        {
            return Ok(boardService.GetBoardLists(boardId));
        }


        ////GET: api/Boards/{boardId}/GetLists
        //[HttpGet("{boardId}/lists")]
        //public IActionResult GetLists(string boardId)
        //{
        //    try
        //    {
        //        Console.WriteLine($"Printing Board Id, {boardId}");
        //        return Ok(boardService.GetLists(boardId));
        //    }
        //    catch (Exception e)
        //    {
        //        return NotFound(e.Message);
        //    }
        //}

    }
}

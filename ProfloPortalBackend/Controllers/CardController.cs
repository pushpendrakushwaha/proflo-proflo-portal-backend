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
    public class CardsController : ControllerBase
    {
        public readonly ICardService _cardService;
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        #region Card Operation

        // POST: api/Cards
        [HttpPost]
        public IActionResult Post([FromBody] Card card)
        {
            _cardService.CreateCard(card);
            return Created("api/Cards", card);
        }

        [HttpPost("move")]
        public async Task<IActionResult> MoveCard([FromBody] MoveCardRequest moveCardRequest)
        {
            await _cardService.MoveCard(moveCardRequest);
            return Ok();
        }

        // GET: api/Cards
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetCards()
        {
            return Ok(_cardService.GetCards());
        }
        //GET :api/cards/{cardId}
        [HttpGet("{cardId}")]
        public ActionResult<IEnumerable<string>>Get(string cardId)
        {
            try
            {
                return Ok(_cardService.GetCardByIDAsync(cardId));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        //GET :api/cards/{cardId}
        [HttpPut("{cardId}")]
        public IActionResult Put(string cardId,[FromBody] Card card)
        {
            try
            {
                return Ok(_cardService.UpdateCard(cardId, card));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        // DELETE api/cards/{cardId}
        [HttpDelete]
        [Route("{cardId}")]
        public IActionResult Delete(string cardId)
        {
            try
            {
                return Ok(_cardService.RemoveCard(cardId));
            }
            catch(Exception e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion

        #region CardMembers

        // POST api/cards/{cardId}/Member
        [HttpPost("{cardId}/Member")]
        public IActionResult CreateMembers(string cardId, [FromBody] Member member)
        {
            _cardService.CreateCardMembers(cardId, member);
            return Created("api/cards/{cardId}/Member", member);
        }
        // GET: api/Cards/{cardId}/member
        [HttpGet("{cardId}/Member")]
        public ActionResult<IEnumerable<string>> GetCardMembers(string cardId)
        {
            return Ok(_cardService.GetCardMembers(cardId));
        }

        // GET: api/Cards/{cardId}/member/{Mid}
        [HttpGet("{cardId}/Member/{Mid}")]
        public ActionResult<IEnumerable<string>> GetCardMemberByMemberId(string cardId,string Mid)
        {
            return Ok(_cardService.GetMemberByMemberId(cardId,Mid));
        }


        // PUT /api/cards/{cardId}/Member/{memberID}
        [HttpPut("{cardId}/Member/{Mid}")]
        public IActionResult UpdateMember(string cardId, string Mid, [FromBody] Member member)
        {
            try
            {
                _cardService.UpdateCardMembers(cardId, Mid, member);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/cards/{cardId}/member/{memberId}
        [HttpDelete("{cardId}/member/{Mid}")]
        public IActionResult DeleteCardMember(string cardId,string Mid)
        {
            try
            {
                return Ok(_cardService.RemoveMembers(cardId,Mid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion

        #region Card Labels
        // POST api/cards/{cardId}/label
        [HttpPost("{cardId}/label")]
        public IActionResult CreateLabels(string cardId, [FromBody] Label label)
        {
            _cardService.CreateLabel(cardId, label);
            return Created("api/cards/{cardId}/label", label);
        }
        // GET: api/Cards/{cardId}/label
        [HttpGet("{cardId}/label")]
        public ActionResult<IEnumerable<string>> GetCardLabels(string cardId)
        {
            return Ok(_cardService.GetCardLabels(cardId));
        }

        // GET: api/Cards/{cardId}/label
        [HttpGet("{cardId}/label/{Lid}")]
        public ActionResult<IEnumerable<string>> GetLabelById(string cardId, string Lid)
        {
            return Ok(_cardService.GetLabelByID(cardId, Lid));
        }


        // PUT /api/cards/{cardId}/Member/{memberID}
        [HttpPut("{cardId}/label/{Lid}")]
        public IActionResult UpdateLabel(string cardId, string Lid, [FromBody] Label label)
        {
            try
            {
                _cardService.UpdateLabel(cardId, Lid, label);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/cards/{cardId}/member/{memberId}
        [HttpDelete("{cardId}/member/{Mid}")]
        public IActionResult DeleteLabel(string cardId, string Lid)
        {
            try
            {
                return Ok(_cardService.RemoveLabel(cardId, Lid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        #endregion

        #region Card Attachments
        // POST api/cards/{cardId}/attachment
        [HttpPost("{cardId}/attachment")]
        public IActionResult CreateAttachment(string cardId, [FromBody] Attachment attachment)
        {
            _cardService.CreateAttachment(cardId, attachment);
            return Created("api/cards/{cardId}/attachment", attachment);
        }
        // GET: api/Cards/{cardId}/attachment
        [HttpGet("{cardId}/attachment")]
        public ActionResult<IEnumerable<string>> GetCardAttachments(string cardId)
        {
            return Ok(_cardService.GetAttachment(cardId));
        }

        // GET: api/Cards/{cardId}/attachment
        [HttpGet("{cardId}/attachment/{Aid}")]
        public ActionResult<IEnumerable<string>> GetCardAttachmentrByAttachmentId(string cardId, string Aid)
        {
            return Ok(_cardService.GetAttachmentByID(cardId, Aid));
        }


        // PUT api/cards/{cardId}/attachment/{attachmentId}
        [HttpPut("{cardId}/attachment/{Aid}")]
        public IActionResult UpdateMember(string cardId, string Aid, [FromBody] Attachment attachment)
        {
            try
            {
                _cardService.UpdateAttachment(cardId, Aid, attachment);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/cards/{cardId}/attachment/{attachmentId}
        [HttpDelete("{cardId}/attachment/{Aid}")]
        public IActionResult DeleteCardAttachment(string cardId, string Aid)
        {
            try
            {
                return Ok(_cardService.RemoveAttachment(cardId, Aid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }


        #endregion

        #region Card Invitee
        // POST api/cards/{cardId}/invitee
        [HttpPost("{cardId}/invitee")]
        public IActionResult CreateInvitee(string cardId, [FromBody] Invite invitee)
        {
            _cardService.CreateInvite(cardId, invitee);
            return Created("api/cards/{cardId}/invitee", invitee);
        }
        // GET: api/Cards/{cardId}/invitee
        [HttpGet("{cardId}/invitee")]
        public ActionResult<IEnumerable<string>> GetCardInvitees(string cardId)
        {
            return Ok(_cardService.GetCardInvites(cardId));
        }
        // DELETE api/cards/{cardId}/invitee/{IId}
        [HttpDelete("{cardId}/invitee/{Iid}")]
        public IActionResult DeleteInvitee(string cardId, string Iid)
        {
            try
            {
                return Ok(_cardService.RemoveInvite(cardId, Iid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        #endregion

        #region Card Comments
        // POST api/cards/{cardId}/comment
        [HttpPost("{cardId}/comment")]
        public IActionResult CreateComment(string cardId, [FromBody] Comment comment)
        {
            try
            {
                _cardService.CreateComments(cardId, comment);
                return Created("api/cards/{cardId}/comment", comment);

            }
            catch(Exception e)
            {
                return StatusCode(200, e.Message);

            }
           
            
        }
        // GET: api/Cards/{cardId}/comment
        [HttpGet("{cardId}/comment")]
        public ActionResult<IEnumerable<string>> GetCardComments(string cardId)
        {
            return Ok(_cardService.GetComments(cardId));
        }

        // GET: api/Cards/{cardId}/comment/{Cid}
        [HttpGet("{cardId}/comment/{Cid}")]
        public ActionResult<IEnumerable<string>> GetCardCommentById(string cardId, string Cid)
        {
            return Ok(_cardService.GetCommentByID(cardId, Cid));
        }


        // PUT api/cards/{cardId}/comment/{Cid}
        [HttpPut("{cardId}/comment/{Cid}")]
        public IActionResult UpdateComment(string cardId, string Cid, [FromBody] Comment comment)
        {
            try
            {
                _cardService.UpdateComments(cardId, Cid, comment);
                return Ok();
            }
            catch (Exception exp)
            {
                return NotFound(exp.Message);
            }
        }
        // DELETE api/cards/{cardId}/card/{CId}
        [HttpDelete("{cardId}/card/{Cid}")]
        public IActionResult DeleteCardComment(string cardId, string Cid)
        {
            try
            {
                return Ok(_cardService.RemoveComments(cardId, Cid));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

        #endregion




    }
}

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
    public class ListsController : ControllerBase
    {
        private readonly IListService _listService;
        public ListsController(IListService listService)
        {
            _listService = listService;
        }

        #region List Operations
        // POST: api/lists
        [HttpPost]
        public IActionResult Post([FromBody] List list)
        {
            _listService.CreateList(list);
            return Created("api/Lists", list);
        }
        // GET: api/lists
        [HttpGet]
       public ActionResult<IEnumerable<string>> Get()
        {
            return Ok(_listService.GetList());
        }
        // GET: api/lists/{listId}
        [HttpGet("{listId}")]
        public ActionResult<IEnumerable<string>> Get(string listId)
        {
            try
            {
                return Ok(_listService.GetListById(listId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
       
        // PUT: api/lists/{listId}
        [HttpPut("{listId}")]
        public IActionResult UpdateListById(string listId, [FromBody] List list)
        {
            try
            {
                return Ok(_listService.UpdateList(listId, list));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        //DELETE: api/lists/{listId}
        [HttpDelete("{listId}")]
        public IActionResult DeleteListById(string listId)
        {
            try
            {
                _listService.RemoveList(listId);
                return Ok();
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }
        #endregion
        //GET ListCards //apo/lists/{listId}/Cards
        //[HttpGet("{listId}/cards")]
        [HttpGet("{listId}/cards")]
        public IActionResult GetListCard(string listId)
        {
            try
            {
                return Ok(_listService.GetListCards(listId));
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }
        }

      
      
        ////[HttpPost("{listId}/cards")]
        //[HttpPost("{listId}/cards")]
        //public void PostListCard(string listId, [FromBody] Card card)
        //{
        //    _listService.CreateListCard(card.ListId, card);
        //}
    }
}

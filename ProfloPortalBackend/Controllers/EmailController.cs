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
    public class EmailController : ControllerBase
    {
        private IEmailNotificationService _emailNS;
        public EmailController(IEmailNotificationService emailNS)
        {
            _emailNS = emailNS;
        }


        // POST: api/Email
        [HttpPost]
        public IActionResult Post([FromBody]Member member)
        {
            _emailNS.SendEmail(member);
            Console.WriteLine("Mail send");
            return Ok("mail send");
        }
    }
}

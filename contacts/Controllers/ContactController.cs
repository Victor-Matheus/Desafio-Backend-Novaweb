using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contacts.Controllers
{
    [ApiController]
    [Route("v1/contacts")]
    public class ContactController : ControllerBase
    {
        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<Models.Contact>>> GetAllContacts([FromServices] Data.DataContext context){
            var contacts = await context.Contacts.ToListAsync();
            return contacts;
        }
    }
}
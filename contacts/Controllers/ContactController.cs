using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using contacts.Enums;
using contacts.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace contacts.Controllers
{


    [ApiController]
    [Route("v1/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly Repositories.ContactRepository _contactRepository;

        [HttpGet]
        [Route("")]
        public async Task<ControllerResponse> GetAllContacts([FromServices] Data.DataContext context)
        {
            try
            {
                var contacts = await _contactRepository._getAllContacts(context);
                return new ControllerResponse(
                    HttpStatusCode.OK,
                    true,
                    "",
                    contacts
                );

            }catch
            {
                return new ControllerResponse(
                    HttpStatusCode.InternalServerError,
                    false,
                    "There was an error in the request",
                    ""
                );
            }
        }

        // [HttpGet]
        // [Route("id:")]

        [HttpPost]
        [Route("")]
        public async Task<ControllerResponse> CreateContact(
            [FromServices] Data.DataContext context,
            [FromBody] Models.Contact model
        )
        {
            var name = new ValueObjects.Name(model.Name.FirstName, model.Name.LastName);
            var email = new ValueObjects.Email(model.Email.Address);
            var numbers = model.PhoneNumbers;
            foreach(var num in numbers)
            {
                var elem = new ValueObjects.PhoneNumber(num.Number);

                if(elem.Invalid) return new ControllerResponse(
                    HttpStatusCode.BadRequest,
                    false,
                    "Invalid phone number",
                    model
                );

                numbers.Add(elem);
            }

            IList<dynamic> notifications = new List<dynamic>();

            notifications.Add(name.Notifications);
            notifications.Add(email.Notifications);

            if(name.Invalid || email.Invalid) return new ControllerResponse(
                HttpStatusCode.BadRequest,
                false,
                "Inavalid name or email",
                notifications
            );

            var contact = new Models.Contact(name, email, numbers);

            var res = await _contactRepository._createContact(context);

            if(res == EDbStatusReturn.DB_SAVED_OK)
            {
                return new ControllerResponse(
                    HttpStatusCode.OK,
                    true,
                    "Contact successfully registered",
                    model
                );
            }
            else
            {
                return new ControllerResponse(
                    HttpStatusCode.InternalServerError,
                    false,
                    "Could not register contact",
                    model
                );
            }
        }
    }
}
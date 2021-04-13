using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Enums;
using Repositories;
using Repositories.Contracts;
using ValueObjects;
using Microsoft.AspNetCore.Mvc;

namespace Controllers
{

    [ApiController]
    [Route("v1/contacts")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository = new ContactRepository();

        [HttpGet]
        [Route("")]
        public async Task<ControllerResponse> GetAllContacts([FromServices] Data.DataContext context)
        {
            try
            {
                var contacts = await _contactRepository._getAllContacts(context);

                List<dynamic> _returnObjects = new List<dynamic>();

                foreach (var contact in contacts.Value)
                {
                    var _aux = new
                    {
                        id = contact.Id,
                        Name = new
                        {
                            firstName = contact.Name.FirstName,
                            lastName = contact.Name.LastName
                        },
                        Email = contact.Email.Address,
                        PhoneNumbers = contact.PhoneNumbers.Select(x => new { number = x.Number })
                    };

                    _returnObjects.Add(_aux);
                }

                return new ControllerResponse(
                    HttpStatusCode.OK,
                    true,
                    "Success",
                    _returnObjects
                );
            }
            catch
            {
                return new ControllerResponse(
                    HttpStatusCode.InternalServerError,
                    false,
                    "There was an error in the request",
                    ""
                );
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ControllerResponse> GetContactById(
            [FromServices] Data.DataContext context,
            int id
        )
        {
            try
            {
                if (id < 1) return new ControllerResponse(
                     HttpStatusCode.BadRequest,
                     false,
                     "Invalid indentifier",
                     ""
                 );

                var _returnContact = await _contactRepository._getContactById(context, id);

                if (_returnContact != null)
                {
                    var _returnObject = new
                    {
                        id = _returnContact.Id,
                        Name = new
                        {
                            firstName = _returnContact.Name.FirstName,
                            lastName = _returnContact.Name.LastName
                        },
                        Email = _returnContact.Email.Address,
                        PhoneNumbers = _returnContact.PhoneNumbers.Select(x => new { number = x.Number })
                    };

                    return new ControllerResponse(
                        HttpStatusCode.OK,
                        true,
                        "Success",
                        _returnObject
                    );
                }

                return new ControllerResponse(
                    HttpStatusCode.NotFound,
                    false,
                    "Contact not found",
                    new { _id = id }
                );
            }
            catch
            {
                return new ControllerResponse(
                    HttpStatusCode.InternalServerError,
                    false,
                    "There was an error in the request",
                    ""
                );
            }

        }

        [HttpPost]
        [Route("")]
        public async Task<ControllerResponse> CreateContact(
            [FromServices] Data.DataContext context,
            [FromBody] Models.ContactRequestModel model
        )
        {
            IList<PhoneNumber> numbers = new List<PhoneNumber>();
            var name = new ValueObjects.Name(model.FirstName, model.LastName);
            var email = new ValueObjects.Email(model.EmailAddress);
            var _numbers = model.PhoneNumbers;

            if (_numbers != null)
            {
                foreach (var num in _numbers)
                {
                    var elem = new ValueObjects.PhoneNumber(num);

                    if (elem.Invalid) return new ControllerResponse(
                         HttpStatusCode.BadRequest,
                         false,
                         "Invalid phone number",
                         model
                     );

                    numbers.Add(elem);
                }
            }

            IList<dynamic> notifications = new List<dynamic>();

            notifications.Add(name.Notifications);
            notifications.Add(email.Notifications);

            if (name.Invalid || email.Invalid) return new ControllerResponse(
                 HttpStatusCode.BadRequest,
                 false,
                 "Inavalid name or email",
                 notifications
             );

            var contact = new Models.Contact(name, email, numbers);
            context.Contacts.Add(contact);

            var res = await _contactRepository._createContact(context);

            var _returnObject = new
            {
                id = contact.Id,
                Name = new
                {
                    firstName = contact.Name.FirstName,
                    lastName = contact.Name.LastName
                },
                Email = contact.Email.Address,
                PhoneNumbers = contact.PhoneNumbers.Select(x => new { number = x.Number })
            };

            if (res == EDbStatusReturn.DB_SAVED_OK)
            {
                return new ControllerResponse(
                    HttpStatusCode.OK,
                    true,
                    "Contact successfully registered",
                    _returnObject
                );
            }

            return new ControllerResponse(
                HttpStatusCode.InternalServerError,
                false,
                "Could not register contact",
                _returnObject
            );
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ControllerResponse> DeleteContact(
            [FromServices] Data.DataContext context,
            int id
        )
        {
            if (id < 1) return new ControllerResponse(
                 HttpStatusCode.BadRequest,
                 false,
                 "Invalid identifier.",
                 ""
             );

            var contactToDelete = await _contactRepository._getContactById(context, id);

            if (contactToDelete == null) return new ControllerResponse(
                 HttpStatusCode.NotFound,
                 false,
                 "Contact not found",
                 ""
             );

            var res = await _contactRepository._deleteContact(context, contactToDelete);

            var returnObject = new
            {
                id = contactToDelete.Id,
                Name = new
                {
                    firstName = contactToDelete.Name.FirstName,
                    lastName = contactToDelete.Name.LastName
                },
                Email = contactToDelete.Email.Address,
                PhoneNumber = contactToDelete.PhoneNumbers.Select(x => new { number = x.Number })
            };

            if (res == EDbStatusReturn.DB_SAVED_OK)
            {
                return new ControllerResponse(
                    HttpStatusCode.OK,
                    true,
                    "Contact deleted successfully.",
                    returnObject
                );
            }

            return new ControllerResponse(
                HttpStatusCode.InternalServerError,
                false,
                "Could not delete contact.",
                returnObject
            );
        }
    }
}
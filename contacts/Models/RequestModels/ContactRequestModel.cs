using System.Collections.Generic;

namespace contacts.Models
{
    public class ContactRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public IList<string> PhoneNumbers { get; set; }
    }
}
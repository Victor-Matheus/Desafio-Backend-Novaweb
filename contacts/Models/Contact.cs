using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using contacts.ValueObjects;

namespace contacts.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        /*
            Use of Value Objects, avoiding excess of primitive types and
            promoting scalability and testing.
        */
        public Name Name { get; set; }
        public Email Email { get; set; }
        
        // List<T> implementation: List -> IList -> ICollection -> IEnumerable
        public IList<PhoneNumber> PhoneNumbers { get; set; }


        public Contact(Name name, Email email, IList<PhoneNumber> phoneNumbers){
            this.Name = name;
            this.Email = email;
            this.PhoneNumbers = phoneNumbers;
        }

        public void AddPhoneNumber(PhoneNumber _phoneNumber){
            PhoneNumbers.Add(_phoneNumber);
        }

    }
}
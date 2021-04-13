using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ValueObjects;

namespace Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public Name Name { get; set; }
        public Email Email { get; set; }
        
        public IList<PhoneNumber> PhoneNumbers { get; set; }


        public Contact(){}

        public Contact(Name name, Email email, IList<PhoneNumber> phoneNumbers){
            this.Name = name;
            this.Email = email;
            PhoneNumbers = phoneNumbers == null ? new List<PhoneNumber>() : phoneNumbers;
        }

        public void AddPhoneNumber(PhoneNumber _phoneNumber){
            PhoneNumbers.Add(_phoneNumber);
        }

    }
}
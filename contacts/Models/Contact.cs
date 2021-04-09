using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using contacts.ValueObjects;

namespace contacts.Models
{
    public class Contact
    {
        [Key]
        public Guid Id { get; set; }

        /*
            Use of Value Objects, avoiding excess of primitive types and
            promoting scalability and testing.
        */
        public Name Name { get; set; }
        public Email Email { get; set; }
        
        // List<T> implementation: List -> IList -> ICollection -> IEnumerable
        public IList<PhoneNumber> PhoneNumber { get; set; }

    }
}
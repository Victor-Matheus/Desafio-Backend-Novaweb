using contacts.ValueObjects.Contracts;
using Flunt.Validations;

namespace contacts.ValueObjects
{
    public class Email : ValueObject
    {
        public Email(string address)
        {
            Address = address;
            Validate();
        }
        public string Address { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .IsEmail(Address, "Email.Address", "Invalid email")
            );
        }
    }
}
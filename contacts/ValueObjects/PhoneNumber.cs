using contacts.ValueObjects.Contracts;
using Flunt.Validations;

namespace contacts.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public PhoneNumber(string number)
        {
            Number = number;
        }

        public string Number { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(Number, 10, "PhoneNumber.Number",
                     "Phone number must contain at least 10 digits") // Ex: '('xx')'xxxxxxxx
            );
        }
    }
}
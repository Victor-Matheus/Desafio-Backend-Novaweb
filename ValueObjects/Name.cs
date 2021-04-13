using ValueObjects.Contracts;
using Flunt.Validations;

namespace ValueObjects
{
    public class Name : ValueObject
    {
        public Name(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
            Validate();
        }

        // public Name(){}

        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public void Validate()
        {
            AddNotifications(
                new Contract()
                    .Requires()
                    .HasMinLen(FirstName, 3, "Name.FirstName", "First name must contain at least 3 characters")
                    .HasMinLen(LastName, 3, "Name.LastName", "Last name must contain at least 3 characters")
                    .HasMaxLen(FirstName, 40, "Name.FirstName", "First name must contain a maximum of 40 characters")
            );
        }

    }
}

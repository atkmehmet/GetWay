namespace Getway.Domain.Entity
{
    // Domain/Users/FullName.cs
    public class FullName
    {
        public string FirstName { get; }
        public string LastName { get; }

        public FullName(string firstName, string lastName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentException("First name required");

            FirstName = firstName;
            LastName = lastName;
        }
    }

}

namespace Getway.Domain.Entity
{
    public class User
    {
        public UserId Id { get; private set; }
        public FullName Name { get; private set; }
        public DateTime RegisteredAt { get; private set; }

        private User() { } // EF için

        public User(FullName name)
        {
            Id = UserId.New();
            Name = name;
            RegisteredAt = DateTime.UtcNow;
        }
    }
}

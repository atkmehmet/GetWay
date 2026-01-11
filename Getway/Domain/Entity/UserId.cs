namespace Getway.Domain.Entity
{
    public sealed class UserId
    {
        public Guid Value { get; }

        private UserId(Guid value)
        {
            if (value == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty");

            Value = value;
        }

        public static UserId New()
            => new UserId(Guid.NewGuid());

        public static UserId From(Guid value)
            => new UserId(value);
    }

}

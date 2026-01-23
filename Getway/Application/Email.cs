namespace Getway.Application
{
    public sealed record Email
    {
        public string Value { get; }

        private static readonly HashSet<string> AllowedDomains =
            new(StringComparer.OrdinalIgnoreCase)
            {
            "gmail.com",
            "hotmail.com"
            };

        public Email(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ValidationException("Email boş olamaz");

            var atIndex = value.IndexOf('@');
            if (atIndex <= 0 || atIndex == value.Length - 1)
                throw new ValidationException("Geçersiz email");

            var domain = value[(atIndex + 1)..];

            if (!AllowedDomains.Contains(domain))
                throw new BusinessRuleException(
                    "Sadece gmail ve hotmail adresleri kabul edilir");

            Value = value.Trim().ToLowerInvariant();
        }

        public override string ToString() => Value;
    }
}

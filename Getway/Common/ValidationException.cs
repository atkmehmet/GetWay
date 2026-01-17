namespace Getway.Common
{
    public class ValidationException:Exception
    {
        public ValidationException(string message,Exception? exception = null) : base(message,exception) { }
    }
}

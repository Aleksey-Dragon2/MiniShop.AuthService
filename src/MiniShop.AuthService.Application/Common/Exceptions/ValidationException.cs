namespace MiniShop.AuthService.Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyDictionary<string, string[]> Errors { get; }
        public ValidationException(IReadOnlyDictionary<string, string[]> errors)
            : base("Validation errors")
        {
            Errors = errors;
        }
    }
}

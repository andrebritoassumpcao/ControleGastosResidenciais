namespace ControleGastosResidenciais.Application.Exceptions;

public abstract class BaseException : Exception
{
    public IEnumerable<ErrorMessage> Errors { get; }

    protected BaseException(string code, string message) : base(message)
    {
        Errors = new[] { new ErrorMessage(code, message) };
    }

    protected BaseException(IEnumerable<ErrorMessage> errors)
        : base(string.Join(", ", errors.Select(e => e.Message)))
    {
        Errors = errors;
    }
}

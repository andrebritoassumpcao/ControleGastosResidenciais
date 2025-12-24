namespace ControleGastosResidenciais.Application.Exceptions;

public class DomainException : BaseException
{
    public DomainException(string code, string message) : base(code, message) { }
}

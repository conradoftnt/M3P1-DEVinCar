namespace DEVinCar.Domain.Exceptions;

public class NoContentException : Exception
{
    public NoContentException(string erro) : base(erro)
    {}
}

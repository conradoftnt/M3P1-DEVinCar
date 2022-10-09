namespace DEVinCar.Domain.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException(string erro) : base(erro)
    {}
}

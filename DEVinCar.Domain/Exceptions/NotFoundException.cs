namespace DEVinCar.Domain.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string erro) : base(erro)
    {}
}

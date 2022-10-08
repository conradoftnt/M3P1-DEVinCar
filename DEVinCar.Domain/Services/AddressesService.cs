using DEVinCar.Domain.Interfaces.Services;
using DEVinCar.Domain.Interfaces.Repositories;
using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Exceptions;

namespace DEVinCar.Domain.Services;

public class AddressesService : IAddressesService
{
    private readonly IAddressesRepository _repository;

    public AddressesService(IAddressesRepository repository)
    {
        _repository = repository;
    }

    public List<Address> Get(int? cityId, int? stateId, string street, string cep)
    {
        IQueryable<Address> addressesList = _repository.GetAll().AsQueryable();

        if (cityId.HasValue)
        {
            addressesList = addressesList.Where(a => a.CityId == cityId);
        }
        if (stateId.HasValue)
        {
            addressesList = addressesList.Where(a => a.City.StateId == stateId);
        }

        if (!string.IsNullOrEmpty(street))
        {
            street = street.ToUpper();
            addressesList = addressesList.Where(a => a.Street.Contains(street));
        }

        if (!string.IsNullOrEmpty(cep))
        {
            addressesList = addressesList.Where(a => a.Cep == cep);
        }

        if (!addressesList.ToList().Any())
        {
            throw new NoContentException();
        }

        return(addressesList.ToList());
    }

    public Address Patch(int addressId, AddressPatchDTO addressPatchDTO)
    {
        Address address = _repository.GetById(addressId);

        if (address == null)
            throw new NotFoundException($"The address with ID: {addressId} not found.");

        string street = addressPatchDTO.Street ?? null;
        string cep = addressPatchDTO.Cep ?? null;
        string complement = addressPatchDTO.Complement ?? null;

        if (street != null)
        {
            if (addressPatchDTO.Street == "")
                throw new BadRequestException("The street name cannot be empty.");
            address.Street = street;
        }

        if (addressPatchDTO.Cep != null)
        {
            if (addressPatchDTO.Cep == "")
                throw new BadRequestException("The cep cannot be empty.");
            if (!addressPatchDTO.Cep.All(char.IsDigit))
                throw new BadRequestException("Every characters in cep must be numeric.");
            address.Cep = cep;
        }

        if (addressPatchDTO.Complement != null)
        {
            if (addressPatchDTO.Complement == "")
                throw new BadRequestException("The complement cannot be empty.");
            address.Complement = complement;
        }

        if (addressPatchDTO.Number != 0)
            address.Number = addressPatchDTO.Number;

        _repository.Update(address);

        _repository.Save();

        return address;
    }

    public void DeleteById(int addressId){
        Address address = _repository.GetById(addressId);

        if (address == null)
        {
            throw new NotFoundException($"The address with ID: {addressId} not found.");
        }

        Delivery relation = _repository.GetRelation(addressId);

        if (relation != null)
        {
            throw new BadRequestException($"The address with ID: {addressId} is related to a delivery.");
        }

        _repository.Delete(addressId);
        _repository.Save();
    }
}

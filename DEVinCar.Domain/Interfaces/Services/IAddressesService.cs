using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;

namespace DEVinCar.Domain.Interfaces.Services;

public interface IAddressesService
{
    List<Address> Get(int? cityId, int? stateId, string street, string cep);

    Address Patch(int addressId, AddressPatchDTO addressPatchDTO);

    void DeleteById(int addressId);
}
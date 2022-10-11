using DEVinCar.Domain.Models;
using DEVinCar.Domain.DTOs;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Domain.ViewModels;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/address")]
[Authorize]
public class AddressesController : ControllerBase
{
    private readonly IAddressesService _service;

    public AddressesController(IAddressesService service)
    {
         _service = service;
    }

    [HttpGet]
    public ActionResult<List<AddressViewModel>> Get([FromQuery] int? cityId,
                                                    [FromQuery] int? stateId,
                                                    [FromQuery] string street,
                                                    [FromQuery] string cep)
    {
        
        List<Address> addressesList = _service.Get(cityId, stateId, street, cep);

        List<AddressViewModel> addressesViewModel = new List<AddressViewModel>();
        addressesList
            .ToList().ForEach(address => {
            addressesViewModel.Add(new AddressViewModel(address.Id,
                                                        address.Street,
                                                        address.CityId,
                                                        address.City.Name,
                                                        address.Number,
                                                        address.Complement,
                                                        address.Cep));
        });
        
        return Ok(addressesViewModel);

    }

    [HttpPatch("{addressId}")]
    public ActionResult<AddressViewModel> Patch([FromRoute] int addressId,
                                       [FromBody] AddressPatchDTO addressPatchDTO)
    {

        Address address = _service.Patch(addressId, addressPatchDTO);

        AddressViewModel addressViewModel = new AddressViewModel(
            address.Id,
            address.Street,
            address.CityId,
            address.City.Name,
            address.Number,
            address.Complement,
            address.Cep
        );

        return Ok(addressViewModel);
    }

    [HttpDelete("{addressId}")]

    public ActionResult DeleteById([FromRoute] int addressId)
    {
        _service.DeleteById(addressId);

        return NoContent();
    }
}

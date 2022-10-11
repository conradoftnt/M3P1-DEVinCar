using DEVinCar.Domain.Models;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/deliver")]
[Authorize]
public class DeliverController : ControllerBase
{
    private readonly IDeliverService _service;

    public DeliverController(IDeliverService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<Delivery>> Get(
    [FromQuery] int? addressId,
    [FromQuery] int? saleId)
    {
        return Ok(_service.Get(addressId, saleId));
    }
}
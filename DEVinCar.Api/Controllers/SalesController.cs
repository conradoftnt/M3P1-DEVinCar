using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DEVinCar.Domain.ViewModels;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/sales")]
[Authorize]
public class SalesController : ControllerBase
{
    private readonly ISalesService _service;

    public SalesController(ISalesService service)
    {
        _service = service;
    }

    [HttpGet("{saleId}")]
    public ActionResult<SaleViewModel> GetItensSale(
        [FromRoute] int saleId)
    {
        Sale sale = _service.GetItensSale(saleId);

        SaleViewModel saleViewModel = new SaleViewModel
        {
            SellerName = sale.UserSeller.Name,
            BuyerName = sale.UserBuyer.Name,
            SaleDate = sale.SaleDate,
            Itens = sale.Cars.Select(sc => new CarViewModel
            {
                Name = sc.Car.Name,
                UnitPrice = sc.UnitPrice,
                Amount = sc.Amount,
                Total = sc.Sum(sc.UnitPrice, sc.Amount)
            }).ToList()
        };

        return Ok(sale);
    }

    [HttpPost("{saleId}/item")]
    [Authorize(Roles = "Gerente, Vendedor")]
    public ActionResult<SaleCar> PostSale(
       [FromBody] SaleCarDTO body,
       [FromRoute] int saleId
       )
    {
        _service.PostSale(body, saleId);

        return Created("api/sales/{saleId}/item", body.CarId);
    }

    [HttpPost("{saleId}/deliver")]
    public ActionResult<DeliveryDTO> PostDeliver(
           [FromRoute] int saleId,
           [FromBody] DeliveryDTO body)
    {
        return Created("{saleId}/deliver", _service.PostDeliver(saleId, body));
    }

    [HttpPatch("{saleId}/car/{carId}/amount/{amount}")]
    public ActionResult<SaleCar> PatchAmount(
            [FromRoute] int saleId,
            [FromRoute] int carId,
            [FromRoute] int amount
            )
    {
        _service.PatchAmount(saleId, carId, amount);

        return NoContent();
    }

    [HttpPatch("{saleId}/car/{carId}/price/{unitPrice}")]
    public ActionResult<SaleCar> PatchUnitPrice(
           [FromRoute] int saleId,
           [FromRoute] int carId,
           [FromRoute] decimal unitPrice
           )
    {
        _service.PatchUnitPrice(saleId, carId, unitPrice);

        return NoContent();
    }

}


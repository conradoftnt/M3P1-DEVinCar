using DEVinCar.Infra.Data;
using DEVinCar.Domain.DTOs;
using DEVinCar.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using DEVinCar.Domain.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;

namespace DEVinCar.Api.Controllers;

[ApiController]
[Route("api/user")]
[Authorize]
public class UserController : ControllerBase
{

    private readonly IUsersService _service;

    public UserController(IUsersService service)
    {
        _service = service;
    }

    [HttpGet]
    public ActionResult<List<User>> Get(
       [FromQuery] string name,
       [FromQuery] DateTime? birthDateMax,
       [FromQuery] DateTime? birthDateMin
   )
    {
        return Ok(_service.Get(name, birthDateMax, birthDateMin));
    }

    [HttpGet("{id}")]
    public ActionResult<User> GetById(
        [FromRoute] int id
    )
    {
        return Ok(_service.GetById(id));
    }

    [HttpGet("{userId}/buy")]
    public ActionResult<List<Sale>> GetUserBuysById([FromRoute] int userId)
    {
        return Ok(_service.GetUserBuysById(userId));
    }

    [HttpGet("{userId}/sales")]
    public ActionResult<List<Sale>> GetSalesBySellerId(
       [FromRoute] int sellerId)
    {
        return Ok(_service.GetSalesBySellerId(sellerId));
    }

    [HttpPost]
    public ActionResult<User> Post(
        [FromBody] UserDTO userDto
    )
    {
        return Created("api/users", _service.Post(userDto).Id);
    }

    [HttpPost("{userId}/sales")]
    public ActionResult<Sale> PostSaleUserId(
           [FromRoute] int userId,
           [FromBody] SaleDTO body)
    {
        return Created("api/sale", _service.PostSaleUserId(userId, body).Id);
    }

    [HttpPost("{userId}/buy")]

    public ActionResult<Sale> PostBuyUserId(
          [FromRoute] int userId,
          [FromBody] BuyDTO body)
    {
        return Created("api/user/{userId}/buy", _service.PostBuyUserId(userId, body).Id);
    }


    [HttpDelete("{userId}")]
    public ActionResult Delete([FromRoute] int userId)
    {
        _service.Delete(userId);

        return NoContent();
    }
}






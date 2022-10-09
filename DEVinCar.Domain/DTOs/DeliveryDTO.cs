using System.ComponentModel.DataAnnotations;

namespace DEVinCar.Domain.DTOs;

public class DeliveryDTO
{
    [Required(ErrorMessage = "The address id is required")]
    public int AddressId { get; set; }
    
    public DateTime? DeliveryForecast { get; set; }
}
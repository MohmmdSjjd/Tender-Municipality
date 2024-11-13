using Domain.Models.User.DTOs;

namespace Domain.Models.Bid.DTOs;

public class BidDto
{
    public decimal Price { get; set; }
    public Guid TenderId { get; set; }
    public UserDto User { get; set; }
}
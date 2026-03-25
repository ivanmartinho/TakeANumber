using TakeANumber.Enums;

namespace TakeANumber.DTOs;
public class TicketNumberRequest
{
    public int TicketGroupId { get; set; }
    public TicketType TicketType { get; set; } = TicketType.Regular;
    public int CompanyId { get; set; }
    public bool Called { get; set; } = false;
    public bool Serviced { get; set; } = false;
    public int SpotId { get; set; } 
}

using TakeANumberShared.Enums;

namespace TakeANumberApi.DTOs;
public class TicketNumberRequest
{
    public int TicketGroupId { get; set; }
    public TicketType TicketType { get; set; } = TicketType.Regular;
    public int CompanyId { get; set; }
    public int SpotId { get; set; } 
}

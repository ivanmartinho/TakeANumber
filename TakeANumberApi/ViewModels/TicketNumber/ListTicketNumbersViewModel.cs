using TakeANumberApi.Enums;

namespace TakeANumberApi.ViewModels;
public class ListTicketNumbersViewModel
{
    public int Id { get; set; }
    public int TicketGroupId { get; set; }
    public string TicketType { get; set; }
    public string CompanyName { get; set; }
    public bool Called { get; set; }
    public bool Serviced { get; set; }
    public DateTime GenerateDate { get; set; }
    public DateTime CalledDate { get; set; }
    public DateTime ServicedDate { get; set; }
}


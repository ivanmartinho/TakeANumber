namespace TakeANumber.DTOs;
public class TicketNumberResponse
{
    public int Id { get; set; }
    public string TicketNumber { get; set; }
    public string SpotName { get; set; }
    public string TicketType { get; set; }

    public TicketNumberResponse(int id, string ticketNumber, string spotName, string ticketType)
    {
        Id = id;
        TicketNumber = ticketNumber;
        SpotName = spotName;
        TicketNumber = ticketNumber;
    }
}

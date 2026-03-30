using TakeANumberApi.Enums;

namespace TakeANumberApi.Models
{
    public class TicketNumber
    {
        public int Id { get; set; }
        public string Ticket { get; set; }
        public TicketGroup TicketGroup { get; set; }
        public Spot Spot { get; set; }
        public Company Company { get; set; }
        public TicketType TicketType { get; set; }
        public bool Called { get; set; }
        public bool Serviced { get; set; }
        public DateTime GenerateDate { get; set; }
        public DateTime? CalledDate { get; set; }
        public DateTime? ServicedDate { get; set; }
    }
}

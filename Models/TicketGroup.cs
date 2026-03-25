namespace TakeANumber.Models
{
    public class TicketGroup
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Acronym { get; set; }
        public bool Enabled { get; set; }
        public TicketGroup ticketGroup { get; set; }

    }
}

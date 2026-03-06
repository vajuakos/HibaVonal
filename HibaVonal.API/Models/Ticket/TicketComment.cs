namespace HibaVonal.API.Models.Ticket
{
    public class TicketComment
    {
        public int Id { get; set; }
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int TicketId { get; set; }
        public MaintenanceTicket? Ticket { get; set; }
    }
}

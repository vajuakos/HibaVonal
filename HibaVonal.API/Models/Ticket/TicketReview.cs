namespace HibaVonal.API.Models.Ticket
{
    public class TicketReview
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public MaintenanceTicket? Ticket { get; set; }

        public int? ReviewerId { get; set; }

        public AppUser? Reviewer { get; set; }

        public int Rating { get; set; }
    }
}

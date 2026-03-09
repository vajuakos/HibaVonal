namespace HibaVonal.API.Models.Ticket
{
    public class TicketFeedback
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int Rating { get; set; }

        public string? FeedbackComment { get; set; }

        public MaintenanceTicket? Ticket { get; set; }

        public int? FeedbackerId { get; set; }

        public AppUser? Feedbacker { get; set; }

        public DateTime CreatedAt { get; set; }

    }
}

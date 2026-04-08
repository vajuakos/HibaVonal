using HibaVonal.Shared.Enum;

namespace HibaVonal.API.Models.Ticket
{
    public class MaintenanceTicket
    {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public int RoomNumber { get; set; }

        public DateTime CreatedAt { get; set; }

        public TicketStatus Status { get; set; }

        public int? AssignedToId { get; set; }

        public AppUser? AssignedTo { get; set; }

        public int CreatedById { get; set; }

        public AppUser CreatedBy { get; set; }

        public ICollection<TicketComment>? Comments { get; set; }

        public int? ReviewId { get; set; }

        public TicketFeedback? Review { get; set; }
    }
}

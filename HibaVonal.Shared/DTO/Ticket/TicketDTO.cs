using HibaVonal.Shared.Enum;

namespace HibaVonal.Shared.DTO.Ticket
{
    public class TicketDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int RoomNumber { get; set; }

        public TicketStatus Status { get; set; }

        public int? Rating { get; set; }

        public string FeedbackComment { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? AssignedToId { get; set; }

        public string? AssignedToEmail { get; set; }

        public string? CreatedByEmail { get; set; }
    }
}
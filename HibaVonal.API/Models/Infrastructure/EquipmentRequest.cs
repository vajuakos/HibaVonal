using HibaVonal.API.Models.Ticket;

namespace HibaVonal.API.Models.Infrastructure
{
    public class EquipmentRequest
    {
        public int Id { get; set; }

        public int TicketId { get; set; }
        public MaintenanceTicket Ticket { get; set; }

        public int EquipmentId { get; set; }
        public Equipment Equipment { get; set; }

        public int Quantity { get; set; }

        public int RequestedById { get; set; }
        public AppUser RequestedBy { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}

namespace HibaVonal.Shared.DTO.Infrastructure
{
    public class EquipmentRequestDTO
    {
        public int Id { get; set; }

        public int TicketId { get; set; }

        public int EquipmentId { get; set; }

        public string? EquipmentName { get; set; }

        public int Quantity { get; set; }

        public int? RequestedById { get; set; }

        public string? RequestedByEmail { get; set; }

        public DateTime RequestedAt { get; set; }
    }
}

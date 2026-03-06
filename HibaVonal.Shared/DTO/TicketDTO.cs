namespace HibaVonal.Shared.DTO
{
    public class TicketDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public int RoomNumber { get; set; }

        public string Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

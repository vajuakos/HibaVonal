namespace HibaVonal.Shared.Enum
{
    public enum TicketStatus
    {
        New,
        InProgress,
        Resolved
    }

    public static class TicketStatusExtensions
    {
        public static string ToDisplayString(this TicketStatus status)
        {
            return status switch
            {
                TicketStatus.New => "Új",
                TicketStatus.InProgress => "Folyamatban",
                TicketStatus.Resolved => "Megoldva",
                _ => status.ToString()
            };
        }
    }
}

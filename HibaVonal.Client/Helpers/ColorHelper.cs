using HibaVonal.Shared.Enum;
using MudBlazor;

namespace HibaVonal.Client.Helpers
{
    public static class ColorHelper
    {
        public static Color GetStatusColor(TicketStatus status) => status switch
        {
            TicketStatus.New => Color.Info,
            TicketStatus.InProgress => Color.Warning,
            TicketStatus.Resolved => Color.Success,
            _ => Color.Default
        };
    }
}

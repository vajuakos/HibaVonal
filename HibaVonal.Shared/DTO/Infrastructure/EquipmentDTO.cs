using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HibaVonal.Shared.DTO.Infrastructure
{
    public class EquipmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty; // Pl: Csaptelep, Izzó, Szekrény
        public string RoomNumber { get; set; } = string.Empty; // Melyik szobához tartozik
    }
}
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Cineplex_Management.VM
{
    public class HallVm
    {
        public int? HallId { get; set; }
        public string? HallName { get; set; }
        public int? Row { get; set; }
        public int? Column { get; set; }
        public List<SeatPlanVm> SeatPlans { get; set; } = new List<SeatPlanVm>();

        public class SeatPlanVm
        {
            public int SeatPlanId { get; set; }
            public string? SeatNumber { get; set; }
            public int? Price { get; set; }
            public int? RowNumber { get; set; }
            public int? HallId { get; set; }
        }
    }
}

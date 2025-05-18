using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cineplex_Management.ViewModels
{
    public class ShowVm
    {
        public int ShowId { get; set; }

        public string? ShowName { get; set; }

        public int? MovieId { get; set; }

        public int? HallId { get; set; }

        public DateTime? ShowStart { get; set; } = DateTime.MinValue;

        public DateTime? ShowEnd { get; set; } = DateTime.MinValue;
        public List<SelectListItem> Movies { get; set; }  = new List<SelectListItem>();  
        public List<SelectListItem> Halls { get; set; } = new List<SelectListItem>();
    }
}

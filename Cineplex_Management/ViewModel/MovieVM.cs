using System.ComponentModel.DataAnnotations;

namespace Cineplex_Management.ViewModel
{
    public class MovieVM
    {
        [Key]
        public int MovieId { get; set; }
        public string MovieName { get; set; }
        public IFormFile? ImageVm { get; set; }
        public int ProducerId { get; set; }
    }
}

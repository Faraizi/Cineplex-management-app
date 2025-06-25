using Cineplex_Management.ViewModels;

namespace Cineplex_Management.ViewModel
{
    public class MovieDetailsVM
    {
        public string MovieName { get; set; }
        public string Image { get; set; }
        public ICollection<ShowDetailVM> ShowDetails { get; set; }
        //public ICollection<HallDetailsVM> HallDetails { get; set; }

    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Cineplex_Management.Models
{
    public class Movie
    {
     [Key]
     public int MovieId { get; set; }
     public string MovieName { get; set; }
     public string Image { get; set; }
     public int ProducerId { get; set; }
    public virtual ICollection<Show> Shows { get; set; } = new List<Show>();
    }
}

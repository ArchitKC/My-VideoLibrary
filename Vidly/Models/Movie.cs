using System; 
using System.ComponentModel.DataAnnotations; 

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string strMovieName { get; set; }    
        public Genre Genre { get; set; }

        [Required]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Range(1,20)]
        public byte NumberInStock { get; set; }
    }
}
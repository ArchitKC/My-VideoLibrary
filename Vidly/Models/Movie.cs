using System; 
using System.ComponentModel.DataAnnotations; 

namespace Vidly.Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)] 
        [Display(Name ="Movie Name")]
        public string strMovieName { get; set; }    
        public Genre Genre { get; set; }

        [Required] 
        [Display(Name ="Genre")]
        public byte GenreId { get; set; }

        public DateTime DateAdded { get; set; }
        
        [Display(Name ="Released Date")]
        public DateTime ReleaseDate { get; set; }
         
        
        [Range(1,20)]
        [Display(Name = "Number of Stocks")]
        public byte NumberInStock { get; set; }
    }
}
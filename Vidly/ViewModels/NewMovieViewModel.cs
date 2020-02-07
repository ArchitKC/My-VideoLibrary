using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class NewMovieViewModel
    {
        public IEnumerable<Genre> Genres { get; set; }


        public int? Id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Movie Name")]
        public string strMovieName { get; set; }
        
        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Released Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number of Stocks")]
        public byte? NumberInStock { get; set; }
        public string Title
        {
            get
            {
                return (Id != 0)? "Edit Movie": "New Movie";
            }
        }

        public NewMovieViewModel()
        {
            Id = 0;
            NumberInStock = 0;
            ReleaseDate = default(DateTime);
        }
        public NewMovieViewModel(Movie movie)
        {
            Id = movie.Id;
            strMovieName = movie.strMovieName;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
            GenreId = movie.GenreId;
        }
    }
}
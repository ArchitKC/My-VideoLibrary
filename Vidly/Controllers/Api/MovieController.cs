using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MovieController : ApiController
    {
        ApplicationDbContext _contextMovie;

        public MovieController()
        {
            _contextMovie = new ApplicationDbContext();
        }

        
        //GET api/Movie
        public IHttpActionResult GetMovie()
        {
            var movie = _contextMovie.Movie.ToList();
            return Ok(movie);
        }

        //Get api/Movie/1  
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _contextMovie.Movie.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        //POST api/Movie
        [HttpPost]
        public IHttpActionResult CreateMovie(Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            _contextMovie.Movie.Add(movie);
            _contextMovie.SaveChanges();
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movie);
        }

        //PUT api/Movie/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, Movie movie)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _contextMovie.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            movieInDB.strMovieName = movie.strMovieName;
            movieInDB.DateAdded = System.DateTime.Today;
            movieInDB.ReleaseDate = movie.ReleaseDate;
            movieInDB.GenreId = movie.GenreId;
            movieInDB.NumberInStock = movie.NumberInStock;
            _contextMovie.SaveChanges();

            return Ok();
        }

        //DELETE api/Movie/1
        [HttpDelete]

        public IHttpActionResult DeleteMovie(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _contextMovie.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            _contextMovie.Movie.Remove(movieInDB);
            _contextMovie.SaveChanges();
            return Ok();
        }
    }
}

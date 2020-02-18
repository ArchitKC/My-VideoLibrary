using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
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
        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _contextMovie.Movie.Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.strMovieName.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        //Get api/Movie/1  
        public IHttpActionResult GetMovie(int id)
        {
            var movie = _contextMovie.Movie.SingleOrDefault(m => m.Id == id);

            if (movie == null)
                return NotFound();

            return Ok(Mapper.Map<Movie,MovieDto>(movie));
        }

        //POST api/Movie
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie = Mapper.Map<MovieDto, Movie>(movieDto);
            _contextMovie.Movie.Add(movie);
            _contextMovie.SaveChanges();
            movieDto.Id = movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + movie.Id), movieDto);
        }

        //PUT api/Movie/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDB = _contextMovie.Movie.SingleOrDefault(m => m.Id == id);

            if (movieInDB == null)
                return NotFound();

            Mapper.Map(movieDto, movieInDB);

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

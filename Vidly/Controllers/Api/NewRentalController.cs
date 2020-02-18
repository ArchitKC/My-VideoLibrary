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
    public class NewRentalController : ApiController
    {
        ApplicationDbContext _contextRentals;

        public NewRentalController()
        {
            _contextRentals = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult CreateRentals(NewRentalDto newRentalDto)
        {
            var customer = _contextRentals.Customers.Single(
                c => c.Id == newRentalDto.CustomerId);

            var movies = _contextRentals.Movie.Where(
                m=>newRentalDto.MovieIds.Contains(m.Id)).ToList();

            foreach (var movie in movies)
            {
                if (movie.NumberAvailable == 0)
                    return BadRequest("Movie not available.");

                movie.NumberAvailable--;
                var rental = new NewRental {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };
                _contextRentals.NewRentals.Add(rental);
            }

            _contextRentals.SaveChanges();

            return Ok();
        }
    }
}

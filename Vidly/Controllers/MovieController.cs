using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{   
    public class MovieController : Controller
    {
        ApplicationDbContext _movieContext; 

        public MovieController()
        {
            _movieContext = new ApplicationDbContext(); 
        }

        protected override void Dispose(bool disposing)
        {
            _movieContext.Dispose(); 
        }

        public ActionResult MovieFormAdd()
        { 
            var genre = _movieContext.Genre.ToList();
            var movieViewModel = new NewMovieViewModel
            { 
                Genres = genre
            };

            return View(movieViewModel);
        }

        [HttpPost]
        public ActionResult Save (Movie movie)
        {
            if (movie.Id == 0)
            {
                movie.DateAdded = System.DateTime.Today;
                _movieContext.Movie.Add(movie);
            }
            else
            {
                var movieInDB = _movieContext.Movie.Single(m => m.Id == movie.Id);
                movieInDB.strMovieName = movie.strMovieName;
                movieInDB.DateAdded = System.DateTime.Today;
                movieInDB.ReleaseDate = movie.ReleaseDate;
                movieInDB.GenreId = movie.GenreId;                
            }
            _movieContext.SaveChanges();

            return RedirectToAction("Index","Movie");
        }

        public ActionResult Edit(int id)
        {
            var movie = _movieContext.Movie.Single(m => m.Id == id);
            if (movie == null)
                return HttpNotFound();
            var movieModelView = new NewMovieViewModel
            {
                Movie = movie,
                //Genres = _movieContext.Genre.ToList()
            };

            return View("MovieFormAdd", movieModelView);
        }
        // GET: Movie
        public ViewResult Index()
        {
            var movies = _movieContext.Movie.Include(m => m.Genre).ToList();

            return View(movies.ToList());
        }     
    }
}
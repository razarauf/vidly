using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext  _context = null; 

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }

        // GET: Movies
        public ActionResult Index()
        {
            var movies = _context.Movies.Include(c => c.Genre).ToList();
            return View(movies);
        }

        public ActionResult Details(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        public ActionResult New()
        {
            var movieViewModel = new MovieViewModel
            {
                Movie = new Movie(),
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", movieViewModel);
        }

        public ActionResult Edit(int id)
        {
            var movie = _context.Movies.Include(c => c.Genre).SingleOrDefault(x => x.Id == id);

            if (movie == null)
            {
                return HttpNotFound();
            }

            var movieViewModel = new MovieViewModel
            {
                Movie = movie,
                Genres = _context.Genres.ToList()
            };

            return View("MovieForm", movieViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(x => x.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.DateAdded = movie.DateAdded;
                movieInDb.NumberInStock = movie.NumberInStock;
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies", null);
        }
    }
}
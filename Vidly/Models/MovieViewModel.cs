using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class MovieViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public byte GenreId { get; set; }

        [Required]
        [Display(Name = "Date Added")]
        public DateTime? DateAdded { get; set; }

        [Required]
        [Display(Name = "Release Date")]
        public DateTime? ReleaseDate { get; set; }

        [Required]
        [Range(1, 20)]
        [Display(Name = "Number In Stock")]
        public byte? NumberInStock { get; set; }

        public MovieViewModel()
        {
            Id = 0;
        }

        public MovieViewModel(Movie movie)
        {
            Id = movie.Id;
            Name = movie.Name;
            GenreId = movie.GenreId;
            DateAdded = movie.DateAdded;
            ReleaseDate = movie.ReleaseDate;
            NumberInStock = movie.NumberInStock;
        }

        public IEnumerable<Genre> Genres { get; set; }

        public string Title {
            get { return Id == 0 ? "New Movie" : "Edit Movie"; }
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Entities
{
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        [Required]
        public DateTime? ReleaseDate { get; set; }
        public string Poster { get; set; }
        public bool InTheater { get; set; }
        public string Trailer { get; set; }
        public string TitleBrief
        {
            get {
                if (string.IsNullOrEmpty(Title))
                {
                    return null;
                }

                if(Title.Length > 60)
                {
                    return Title.Substring(0, 60) + "...";
                }

                return Title; 
            }
        }

        //navigational properties -> allow you to navigate and manage relationships in both directions,
        //returning either a reference object (if the multiplicity is either one or zero-or-one) or a collection (if the multiplicity is many)
        public List<MoviesGenres> MoviesGenres { get; set; } = new List<MoviesGenres>();
        public List<MoviesActors> MoviesActors { get; set; } = new List<MoviesActors>();
    }
}

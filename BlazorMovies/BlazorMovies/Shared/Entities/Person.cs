using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Entities
{
    public class Person
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Biography { get; set; }

        public string Photo { get; set; }

        [Required]
        public DateTime? BirthDate { get; set; }

        // this won't be mapped in Person table in DB
        [NotMapped]
        public string Character { get; set; }

        //navigational properties -> allow you to navigate and manage relationships
        public List<MoviesActors> MoviesActors { get; set; } = new List<MoviesActors>();
    }
}

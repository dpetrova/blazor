using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Entities
{
    //represent many-to-many in-between table
    public class MoviesActors
    {
        public int MovieId { get; set; }

        public int PersonId { get; set; }

        //navigational properties
        public Movie Movie { get; set; }
        public Person Person { get; set; }

        public string Character { get; set; }

        public int Order { get; set; }
    }
}

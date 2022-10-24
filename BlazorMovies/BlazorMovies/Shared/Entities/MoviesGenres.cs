using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.Entities
{
    //represent many-to-many in-between table
    public class MoviesGenres
    {
        public int MovieId { get; set; }

        public int GenreId { get; set; }

        //navigational properties
        //public virtual Movie Movie { get; set; }
        //public virtual Genre Genre { get; set; }
    }
}

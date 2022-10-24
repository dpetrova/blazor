using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.DTO
{
    /* DTO -> data transfer object (used for transfer data between systems) */

    public class IndexPageDTO
    {
        public List<Movie> InTheatres { get; set; }
        public List<Movie> UpcomingReleases { get; set; }
    }
}

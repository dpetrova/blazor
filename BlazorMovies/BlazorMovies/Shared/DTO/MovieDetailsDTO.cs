﻿using BlazorMovies.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.DTO
{
    public class MovieDetailsDTO
    {
        public Movie Movie { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Person> Actors { get; set; }
        public double AverageVote { get; set; }
        public int UserVote { get; set; }
    }
}

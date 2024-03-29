﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorMovies.Shared.DTO
{
    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
        public int TotalPages { get; set; }
    }
}

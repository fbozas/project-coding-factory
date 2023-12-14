using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MoviePostGetDto
    {
        public List<GenreGetDto> Genres { get; set; }
        public List<MovieTheaterGetDto> MovieTheaters { get; set; }
    }
}

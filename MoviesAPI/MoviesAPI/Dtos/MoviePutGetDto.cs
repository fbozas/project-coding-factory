using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MoviePutGetDto
    {
        public MovieGetDto Movie { get; set; }
        public List<GenreGetDto> SelectedGenres { get; set; }
        public List<GenreGetDto> NonSelectedGenres { get; set; }
        public List<MovieTheaterGetDto> SelectedMovieTheaters { get; set; }
        public List<MovieTheaterGetDto> NonSelectedMovieTheaters { get; set; }
        public List<ActorMovieGetDto> Actors { get; set; }
    }
}

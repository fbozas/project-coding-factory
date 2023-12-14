using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MovieGetDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<GenreGetDto> Genres { get; set; }
        public List<MovieTheaterGetDto> MovieTheaters { get; set; }
        public List<ActorMovieGetDto> Actors { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class HomeGetDto
    {
        public List<MovieGetDto> InTheaters { get; set; }
        public List<MovieGetDto> UpcomingReleases { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class FilterMoviesDto
    {
        public int Page { get; set; }
        public int RecordsPerPage { get; set; }
        public PaginationDto PaginationDto
        {
            get { return new PaginationDto() { Page = Page, RecordsPerPage = RecordsPerPage }; }
        }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public bool InTheaters { get; set; }
        public bool UpcomingReleases { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class PaginationDto
    {
        public int Page { get; set; }
        private int _recordsPerPage = 10;
        private readonly int _maxAmount = 50;
        
        public int RecordsPerPage
        {
            get
            {
                return _recordsPerPage;
            }
            set
            {
                _recordsPerPage = (value > _maxAmount) ? _maxAmount : value;
            }
        }
    }
}

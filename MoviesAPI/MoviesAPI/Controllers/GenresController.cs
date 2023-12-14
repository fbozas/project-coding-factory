using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesAPI.Dtos;
using MoviesAPI.Entities;
using MoviesAPI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/genres")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public GenresController(ILogger<GenresController> logger, ApplicationDbContext context, IMapper mapper)
        {
            _logger = logger;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<GenreGetDto>>> Get()
        {
            _logger.LogInformation("Getting all the genres");
            var genres = await _context.Genres.OrderBy(g => g.Name).ToListAsync();
            return _mapper.Map<List<GenreGetDto>>(genres);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<GenreGetDto>> Get(int Id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g => g.Id == Id);

            if (genre == null)
            {
                return NotFound();
            }

            return _mapper.Map<GenreGetDto>(genre);
        }

        [HttpPost]
        public ActionResult Post([FromBody] GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            _context.Add(genre);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GenreDto genreDto)
        {
            var genre = _mapper.Map<Genre>(genreDto);
            genre.Id = id;
            _context.Entry(genre).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(x => x.Id == Id);

            if (genre == null)
            {
                return NotFound();
            }

            _context.Remove(genre);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using MoviesAPI.Entities;
using MoviesAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/actors")]
    [ApiController]
    public class ActorsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorageService;
        private readonly string containerName = "actors";

        public ActorsController(ApplicationDbContext context, IMapper mapper, IFileStorageService fileStorageService)
        {
            _context = context;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ActorGetDto>>> Get([FromQuery] PaginationDto paginationDto)
        {
            var queryable = _context.Actors.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);

            var actors = await queryable.OrderBy(a => a.Name).Paginate(paginationDto).ToListAsync();
            return _mapper.Map<List<ActorGetDto>>(actors);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ActorGetDto>> Get(int id)
        {
            var actor = await _context.Actors.SingleOrDefaultAsync(a => a.Id == id);

            if (actor == null)
                return NotFound();

            return _mapper.Map<ActorGetDto>(actor);
        }

        [HttpPost("searchByName")]
        public async Task<ActionResult<List<ActorMovieGetDto>>> SearchByName([FromBody] string name)
        {
            if (string.IsNullOrWhiteSpace(name)) { return new List<ActorMovieGetDto>(); }
            return await _context.Actors
                .Where(x => x.Name.Contains(name))
                .OrderBy(x => x.Name)
                .Select(x => new ActorMovieGetDto { Id = x.Id, Name = x.Name, Picture = x.Picture })
                .Take(5)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorDto actorDto)
        {
            var actor = _mapper.Map<Actor>(actorDto);

            if (actorDto.Picture != null)
            {
                actor.Picture = await _fileStorageService.SaveFile(containerName, actorDto.Picture);
            }

            _context.Add(actor);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorDto actorDto)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            actor = _mapper.Map(actorDto, actor);

            if (actorDto.Picture != null)
            {
                actor.Picture = await _fileStorageService.EditFile(containerName,
                    actorDto.Picture, actor.Picture);
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);

            if (actor == null)
            {
                return NotFound();
            }

            _context.Remove(actor);
            await _context.SaveChangesAsync();

            await _fileStorageService.DeleteFile(actor.Picture, containerName);

            return NoContent();
        }
    }
}

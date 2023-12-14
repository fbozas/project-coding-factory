using AutoMapper;
using MoviesAPI.Dtos;
using MoviesAPI.Entities;
using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public class AutomapperProfiles : Profile
    {
        public AutomapperProfiles(GeometryFactory geometryFactory)
        {
            CreateMap<GenreGetDto, Genre>().ReverseMap();
            CreateMap<GenreDto, Genre>();

            CreateMap<ActorGetDto, Actor>().ReverseMap();
            CreateMap<ActorDto, Actor>();
            CreateMap<ActorUpdateDto, Actor>();

            CreateMap<MovieTheater, MovieTheaterGetDto>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));

            CreateMap<MovieTheaterDto, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto =>
                geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));

            CreateMap<MovieDto, Movie>()
             .ForMember(x => x.Poster, options => options.Ignore())
             .ForMember(x => x.MoviesGenres, options => options.MapFrom(MapMoviesGenres))
             .ForMember(x => x.MovieTheatersMovies, options => options.MapFrom(MapMovieTheatersMovies))
             .ForMember(x => x.MoviesActors, options => options.MapFrom(MapMoviesActors));

            CreateMap<Movie, MovieGetDto>()
                .ForMember(x => x.Genres, options => options.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheaters, options => options.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.Actors, options => options.MapFrom(MapMoviesActors));

        }


        private List<MoviesGenres> MapMoviesGenres(MovieDto movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesGenres>();

            if (movieCreationDTO.GenresIds == null) { return result; }

            foreach (var id in movieCreationDTO.GenresIds)
            {
                result.Add(new MoviesGenres() { GenreId = id });
            }

            return result;
        }

        private List<MovieTheatersMovies> MapMovieTheatersMovies(MovieDto movieCreationDTO,
            Movie movie)
        {
            var result = new List<MovieTheatersMovies>();

            if (movieCreationDTO.MovieTheatersIds == null) { return result; }

            foreach (var id in movieCreationDTO.MovieTheatersIds)
            {
                result.Add(new MovieTheatersMovies() { MovieTheaterId = id });
            }

            return result;
        }

        private List<MoviesActors> MapMoviesActors(MovieDto movieCreationDTO, Movie movie)
        {
            var result = new List<MoviesActors>();

            if (movieCreationDTO.Actors == null) { return result; }

            foreach (var actor in movieCreationDTO.Actors)
            {
                result.Add(new MoviesActors() { ActorId = actor.Id, Character = actor.Character });
            }

            return result;
        }


        private List<GenreGetDto> MapMoviesGenres(Movie movie, MovieGetDto moviedto)
        {
            var result = new List<GenreGetDto>();

            if (movie.MoviesGenres != null)
            {
                foreach (var genre in movie.MoviesGenres)
                {
                    result.Add(new GenreGetDto() { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

            return result;
        }

        private List<MovieTheaterGetDto> MapMovieTheatersMovies(Movie movie, MovieGetDto movieDTO)
        {
            var result = new List<MovieTheaterGetDto>();

            if (movie.MovieTheatersMovies != null)
            {
                foreach (var movieTheaterMovies in movie.MovieTheatersMovies)
                {
                    result.Add(new MovieTheaterGetDto()
                    {
                        Id = movieTheaterMovies.MovieTheaterId,
                        Name = movieTheaterMovies.MovieTheater.Name,
                        Latitude = movieTheaterMovies.MovieTheater.Location.Y,
                        Longitude = movieTheaterMovies.MovieTheater.Location.X
                    });
                }
            }

            return result;
        }

        private List<ActorMovieGetDto> MapMoviesActors(Movie movie, MovieGetDto movieDTO)
        {
            var result = new List<ActorMovieGetDto>();

            if (movie.MoviesActors != null)
            {
                foreach (var moviesActors in movie.MoviesActors)
                {
                    result.Add(new ActorMovieGetDto()
                    {
                        Id = moviesActors.ActorId,
                        Name = moviesActors.Actor.Name,
                        Character = moviesActors.Character,
                        Picture = moviesActors.Actor.Picture,
                        Order = moviesActors.Order
                    });
                }
            }

            return result;
        }
    }
}

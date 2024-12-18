using AutoMapper;
using Movies.Backend.Models;
using Movies.Backend.Models.DataTransferObjects;

namespace Movies.Backend
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<MovieDto, Movie>();
                config.CreateMap<Movie, MovieDto>();
            });

            return mappingConfig;
        }
    }
}

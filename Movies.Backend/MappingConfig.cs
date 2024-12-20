using AutoMapper;
using Movies.Backend.Models;
using Movies.Backend.Models.DataTransferObjects;

namespace Movies.Backend
{
    public class MappingConfig
    {
        //Mapper will help assign a MovieDto to a Movie object and the opposite when it is needed
        //MovieDto and ResponseDto have the same fields so i dont have to configure the matching of the fields
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

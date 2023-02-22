using AutoMapper;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Mapper
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerCreateDTO>().ReverseMap();
        }
    }

    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<Actor, ActorCreateDTO>().ReverseMap();
            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorDTO, ActorCreateDTO>().ReverseMap();
        }
    }

    public class DirectorProfile : Profile
    {
        public DirectorProfile()
        {
            CreateMap<Director, DirectorCreateDTO>().ReverseMap();
            CreateMap<Director, DirectorDTO>().ReverseMap();
            CreateMap<DirectorDTO, DirectorCreateDTO>().ReverseMap();
        }
    }

    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<Movie, MovieDTO>().ReverseMap();
            CreateMap<Movie, MovieCreateDTO>().ReverseMap();
            CreateMap<MovieDTO, MovieCreateDTO>().ReverseMap();
        }
    }
    public class OrderMovieProfile : Profile
    {
        public OrderMovieProfile()
        {
            CreateMap<Order, OrderCreateDTO>().ReverseMap();
        }
    }
}

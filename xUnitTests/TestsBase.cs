using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PatikaFinalProject.DataAccess;
using PatikaFinalProject.Services.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xUnitTests
{
    public class TestsBase
    {
        protected MyDbContext _dbContext;
        protected IMapper mapper;

        public TestsBase()
        {
            _dbContext = new MyDbContext(new DbContextOptionsBuilder<MyDbContext>().UseSqlServer("Data Source=Dell; Initial Catalog=patikaFinalProject; Integrated Security=true; TrustServerCertificate=True;").Options);
            MapperConfiguration configuration = new MapperConfiguration(opt => {
                opt.AddProfile(new CustomerProfile());
                opt.AddProfile(new ActorProfile());
                opt.AddProfile(new DirectorProfile());
                opt.AddProfile(new MovieProfile());
                opt.AddProfile(new OrderMovieProfile());
            });
            mapper = configuration.CreateMapper();
        }
    }
}

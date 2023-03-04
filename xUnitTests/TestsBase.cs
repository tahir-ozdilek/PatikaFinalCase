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
                opt.AddProfile(new ShoppingListProfile());
            });
            mapper = configuration.CreateMapper();
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace PatikaFinalProject.DataAccess
{
    public class DirectorDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class DirectorCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class ActorDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }    
    public class ActorCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class MovieDTO
    {
        public int ID { get; set; }
        public string MovieName { get; set; }
        public int MovieTypeID { get; set; }
        [ForeignKey("MovieTypeID")]
        public MovieType? MovieType { get; set; }
        public DateTime MovieYear { get; set; }
        public int DirectorID { get; set; }
        [ForeignKey("DirectorID")]
        public Director? Director { get; set; }
        public int Price { get; set; }
        public bool IsSold { get; set; } = false;
    }
    public class MovieCreateDTO
    {
        public string MovieName { get; set; }
        public int MovieTypeID { get; set; }
        public DateTime MovieYear { get; set; }
        public int DirectorID { get; set; }
        public int Price { get; set; }
    }

    public class MovieTypeDTO
    {
        //enum MovieTypes { Comedy = 1, Horror, Action};
        public int ID { get; set; }
        public string Type { get; set; }
    }


    public class CustomerCreateDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }

    public class OrderCreateDTO
    {
        public DateTime OrderDate { get; set; }
        public int MovieID { get; set; }
        public int CustomerID { get; set; }
        public int Price { get; set; }
    }

    public class ActorMovieDTO
    {
        public int ActorID { get; set; }
        [ForeignKey("ActorID")]
        public Actor? Actor { get; set; }
        public int MovieID { get; set; }
        [ForeignKey("MovieID")]
        public Movie? Movie { get; set; }
    }

    public class CustomerFavTypeDTO
    {
        public int CustomerID { get; set; }
        [ForeignKey("CustomerID")]
        public Customer? Customer { get; set; }
        public int FavMovieTypeID { get; set; }
        [ForeignKey("FavMovieTypeID")]
        public MovieType? MovieType { get; set; }
    }
}

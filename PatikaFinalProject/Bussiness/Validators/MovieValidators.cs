using FluentValidation;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Validators
{
    public class MovieDTOValidator : AbstractValidator<MovieDTO>
    {
        public MovieDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MovieName).NotEmpty().Length(2, 50);
            RuleFor(x => x.MovieYear).LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.MovieYear).GreaterThanOrEqualTo(new DateTime(1800, 01, 01));
        }
    }

    public class MovieCreateDTOValidator : AbstractValidator<MovieCreateDTO>
    {
        public MovieCreateDTOValidator()
        {
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.MovieName).NotEmpty().Length(2, 50);
            RuleFor(x => x.MovieYear).LessThanOrEqualTo(DateTime.Now);
            RuleFor(x => x.MovieYear).GreaterThanOrEqualTo(new DateTime(1800,01,01));
        }
    }
}

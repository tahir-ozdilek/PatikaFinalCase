using FluentValidation;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Validators
{
    public class DirectorDTOValidator : AbstractValidator<DirectorDTO>
    {
        public DirectorDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Length(2,50);
            RuleFor(x => x.Surname).NotEmpty().Length(2, 50);
        }
    }

    public class DirectorCreateDTOValidator : AbstractValidator<DirectorCreateDTO>
    {
        public DirectorCreateDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 50);
            RuleFor(x => x.Surname).NotEmpty().Length(2, 50);
        }
    }
}

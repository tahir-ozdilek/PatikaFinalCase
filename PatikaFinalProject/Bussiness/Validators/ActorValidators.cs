using FluentValidation;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Validators
{
    public class ActorDTOValidator : AbstractValidator<ActorDTO>
    {
        public ActorDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.Name).NotEmpty().Length(2,50);
            RuleFor(x => x.Surname).NotEmpty().Length(2, 50);
        }
    }

    public class ActorCreateDTOValidator : AbstractValidator<ActorCreateDTO>
    {
        public ActorCreateDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 50);
            RuleFor(x => x.Surname).NotEmpty().Length(2, 50);
        }
    }
}

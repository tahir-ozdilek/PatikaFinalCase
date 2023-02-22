using FluentValidation;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Validators
{
    public class CustomerCreateDTOValidator : AbstractValidator<CustomerCreateDTO>
    {
        public CustomerCreateDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().Length(2, 50);
            RuleFor(x => x.Surname).NotEmpty().Length(2, 50);
        }
    }
}

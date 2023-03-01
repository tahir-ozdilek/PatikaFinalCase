using FluentValidation;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.DataAccess;

namespace PatikaFinalProject.Services.Validators
{
    public class ShoppingListDTOValidator : AbstractValidator<ShoppingListDTO>
    {
        public ShoppingListDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            //RuleFor(x => x.CreationDate).LessThanOrEqualTo(DateTime.Now);
            //RuleFor(x => x.CompletedDate).GreaterThanOrEqualTo(DateTime.Now);
        }
    }
    public class ShoppingListCreateDTOValidator : AbstractValidator<ShoppingListCreateDTO>
    {
        public ShoppingListCreateDTOValidator()
        {
            //RuleFor(x => x.CreationDate).LessThanOrEqualTo(DateTime.Now);
            //RuleFor(x => x.CompletedDate).GreaterThanOrEqualTo(DateTime.Now);
        }
    }



    public class CategoryDTOValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.Name).Length(0,25);
            RuleFor(x => x.Description).Length(0, 100);
        }
    }
    public class CategoryCreateDTOValidator : AbstractValidator<CategoryCreateDTO>
    {
        public CategoryCreateDTOValidator()
        {
            RuleFor(x => x.Name).Length(0, 25);
            RuleFor(x => x.Description).Length(0, 100);
        }
    }



    public class ProductDTOValidator : AbstractValidator<ProductDTO>
    {
        public ProductDTOValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
            RuleFor(x => x.ShoppingListID).NotEmpty();
            RuleFor(x => x.Name).Length(0, 20);
            RuleFor(x => x.Unit).Length(0, 20);
            RuleFor(x => x.Amount).InclusiveBetween(Int32.MinValue, Int32.MaxValue);
        }
    }
    public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
    {
        public ProductCreateDTOValidator()
        {
            // RuleFor(x => x.ShoppingListID).NotEmpty();
            RuleFor(x => x.Name).Length(0, 20);
            RuleFor(x => x.Unit).Length(0, 20);
            RuleFor(x => x.Amount).InclusiveBetween(Int32.MinValue, Int32.MaxValue);
        }
    }

    public class LoginRequestModelValidator : AbstractValidator<LoginRequestModel>
    {
        public LoginRequestModelValidator()
        {
            // It may be wise idea to forbid some characters such as !,=,|,<,> for preventing injections.
            RuleFor(x => x.UserName).Length(3, 30).WithMessage("Lenght of user name can be min 3 max 30 characters");
        }
    }

    public class RegistrationModelValidator : AbstractValidator<RegistrationRequestModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.UserName).Length(3, 30).WithMessage("Lenght of user name can be min 3 max 30 characters");
            RuleFor(x => x.UserType).Must(x => x.Equals("Member") || x.Equals("Admin")).WithMessage("User type can be only 'Member' or 'Admin'");
            RuleFor(x => x.Password).MinimumLength(8).WithMessage("Your password length must be at least 8.")
                                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
        }
    }
}

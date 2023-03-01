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

        }
    }

    public class RegistrationModelValidator : AbstractValidator<RegistrationRequestModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.UserName).Length(3, 30).WithMessage("Lenght of user name can be min 3 max 30 characters"); ;
            RuleFor(x => x.UserType).Must(x => x.Equals("Member") || x.Equals("Admin")).WithMessage("User type can be only 'Member' or 'Admin'");
            RuleFor(x => x.Password).Length(8,20).WithMessage("Lenght of password must be between 8 and 20 characters");
            //additional conditions for password is a must
        }
    }
}

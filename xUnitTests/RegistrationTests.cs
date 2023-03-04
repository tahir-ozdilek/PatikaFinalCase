namespace xUnitTests
{
    public class LogInTests : TestsBase
    {
        private RegisterLoginService movieService;

        public LogInTests()
        {
            movieService = new RegisterLoginService(_dbContext, mapper, new LoginRequestModelValidator(), new RegistrationModelValidator());
        }
        
        public static IEnumerable<object[]> InvalidInputList =>  new List<object[]>
                                                                 {
                                                                        new object[] { "a" , "123qwe!'^QWE", "Admin" },
                                                                        new object[] { "1234567890123456789012345678901", "123qwe!'^QWE", "Admin" },
                                                                        new object[] { "aaa", "123qwe!'^QWE", "CustomerUserType" },
                                                                        new object[] { "aaa", "12qw!'Q", "Member" },
                                                                        new object[] { "aaa", "123qwe!'^QWE", "admin" }
                                                                 };

        [Theory, MemberData(nameof(InvalidInputList))]
        public void MovieInvalidInputTests(string userName, string password, string userType)
        {
            RegistrationRequestModel dto = new RegistrationRequestModel();
            
            dto.UserName = userName;
            dto.Password = password;
            dto.UserType = userType;

            //Different approach attempts
            /*Func<Task> act = async () => await movieService.Create(dto);
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("'Price' must be greater than or equal to '0'.s");*/
            //FluentActions.Invoking(() => movieService.Create(dto)).Should().ThrowAsync<ArgumentNullException>().Result.And.Message;

            RegistrationModelValidator regVal = new RegistrationModelValidator();
            ValidationResult valRes = regVal.Validate(dto);
            valRes.Errors.Count.Should().BeGreaterThan(0);
        }




        public static IEnumerable<object[]> ValidInputList => new List<object[]>
                                                                 {
                                                                        new object[] { "111" , "123qwe!QWE", "Admin" },
                                                                        new object[] { "123456789012345678901234567890", "^123qwe!'^QQWE", "Admin" },
                                                                        new object[] { "aaa", "1!'^+%qweQWE", "Member" },
                                                                 };

        [Theory, MemberData(nameof(ValidInputList))]
        public void MovieValidInputTests(string userName, string password, string userType)
        {
            RegistrationRequestModel dto = new RegistrationRequestModel();

            dto.UserName = userName;
            dto.Password = password;
            dto.UserType = userType;

            RegistrationModelValidator regVal = new RegistrationModelValidator();
            ValidationResult valRes = regVal.Validate(dto);
            valRes.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}
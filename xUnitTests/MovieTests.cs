using AutoMapper;
using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PatikaFinalProject.Bussiness.Services;
using PatikaFinalProject.DataAccess;
using PatikaFinalProject.Services.Mapper;
using PatikaFinalProject.Services.Validators;
using Xunit.Abstractions;

namespace xUnitTests
{
    public class MovieTests : TestsBase
    {
        private ShoppingListService movieService;

        public MovieTests()
        {
            movieService = new MovieService(_dbContext, mapper, new MovieCreateDTOValidator(), new MovieDTOValidator());
        }
        
        public static IEnumerable<object[]> InvalidInputList =>  new List<object[]>
                                                                 {
                                                                        new object[] { "a" , 1, new DateTime(2020,01,01) },
                                                                        new object[] { "aaa" , -1, new DateTime(2020,01,01) },
                                                                        new object[] { "aaa", 2, new DateTime(2025,01,01) },
                                                                        new object[] { "aaa", 1, new DateTime(1799,01,01) }
                                                                 };

        [Theory, MemberData(nameof(InvalidInputList))]
        public void MovieInvalidInputTests(string name, int price, DateTime date)
        {
            MovieCreateDTO dto = new MovieCreateDTO();

            dto.MovieName = name;
            dto.Price = price;
            dto.MovieYear = date;

            //Different approach attempts
            /*Func<Task> act = async () => await movieService.Create(dto);
            act.Should().ThrowAsync<InvalidOperationException>().WithMessage("'Price' must be greater than or equal to '0'.s");*/

            //FluentActions.Invoking(() => movieService.Create(dto)).Should().ThrowAsync<ArgumentNullException>().Result.And.Message;

            MovieCreateDTOValidator mcv = new MovieCreateDTOValidator();
            ValidationResult valRes = mcv.Validate(dto);
            valRes.Errors.Count.Should().BeGreaterThan(0);
            //valRes.Errors.Should().Contain(new ValidationFailure("Price", "'Price' must be greater than or equal to '0'."));
        }




        public static IEnumerable<object[]> ValidInputList => new List<object[]>
                                                                 {
                                                                        new object[] { "aaa" , 0, new DateTime(2020,01,01) },
                                                                        new object[] { "00", 1, DateTime.Now },
                                                                        new object[] { "aa", 1, new DateTime(1800,01,01) }
                                                                 };

        [Theory, MemberData(nameof(ValidInputList))]
        public void MovieValidInputTests(string name, int price, DateTime date)
        {
            MovieCreateDTO dto = new MovieCreateDTO();

            dto.MovieName = name;
            dto.Price = price;
            dto.MovieYear = date;


            MovieCreateDTOValidator mcv = new MovieCreateDTOValidator();
            ValidationResult valRes = mcv.Validate(dto);
            valRes.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }
    }
}
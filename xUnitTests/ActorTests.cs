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
    //Director and Customer Tests will be pretty much similar too, I just skip them.
    public class ActorTests : TestsBase
    {
        private ActorService movieService;

        public ActorTests()
        {
            movieService = new ActorService(_dbContext, mapper, new ActorCreateDTOValidator(), new ActorDTOValidator());
        }
        
        public static IEnumerable<object[]> InvalidInputList =>  new List<object[]>
                                                                 {
                                                                        new object[] { "a" , "aa" },
                                                                        new object[] { "aa" , "a"},
                                                                 };

        [Theory, MemberData(nameof(InvalidInputList))]
        public void ActorInvalidInputTests(string name, string surname)
        {
            ActorCreateDTO dto = new ActorCreateDTO();
            
            dto.Name = name;
            dto.Surname = surname;

            ActorCreateDTOValidator mcv = new ActorCreateDTOValidator();
            ValidationResult valRes =  mcv.Validate(dto);
            valRes.Errors.Count.Should().BeGreaterThan(0);
        }


        public static IEnumerable<object[]> ValidInputList => new List<object[]>
                                                                 {
                                                                        new object[] { "aa" , "aa" },
                                                                        new object[] { "00" , "00"},
                                                                 };

        [Theory, MemberData(nameof(ValidInputList))]
        public void ActorValidInputTests(string name, string surname)
        {
            ActorCreateDTO dto = new ActorCreateDTO();

            dto.Name = name;
            dto.Surname = surname;


            ActorCreateDTOValidator mcv = new ActorCreateDTOValidator();
            ValidationResult valRes = mcv.Validate(dto);
            valRes.Errors.Count.Should().BeLessThanOrEqualTo(0);
        }


    }
}
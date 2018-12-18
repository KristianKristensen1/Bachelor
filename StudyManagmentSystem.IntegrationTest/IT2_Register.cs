using System.IO;
using NUnit.Framework;
using BachelorBackEnd;
using Microsoft.EntityFrameworkCore;
using FrontEndBA.Controllers;
using FrontEndBA.Models.ResearcherModel.AccountViewModels;
using System.Linq;
using Castle.Core.Configuration;
using FrontEndBA;
using JwtAuthenticationHelper;
using JwtAuthenticationHelper.Abstractions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using StudyManagementSystem.Configs;

namespace StudyManagmentSystem.IntegrationTest
{
    class IT2_Register
    {

        DbContextOptionsBuilder<bachelordbContext> builder = new DbContextOptionsBuilder<bachelordbContext>();
        ResearcherRegisterViewModel rrvm = new ResearcherRegisterViewModel();
        int noOfReseachersBefore = new int();
        int noOfReseachersAfter = new int();

        [SetUp]
        public void SetUp()
        {
            //Creates a researcher model for testing            
            rrvm = new ResearcherRegisterViewModel()
            {
                Email = "inteegrationtest@register.com",
                Firstname = "Nicola",
                Lastname = "integrationTestla",
                Password = "test1234"
            };

            // Initialize the Connectionstring.
            ConfigStrings.Connectionstring =
                "Server=35.228.116.222;Database=bachelordb;user=Admin;pwd=admin1234;";
        }

        [Test]
        public void RegisterResearcher_SuccesfulRegistration_ResearcherIsInDB()
        {
            builder.UseMySql(ConfigStrings.Connectionstring);

            //Retrieves the data from the database and saves in it a context
            bachelordbContext _dbContext = new bachelordbContext(builder.Options);

            //Loads the Researcher table into the context's local-instance
            _dbContext.Researcher.Load();

            //Saves the current number of researchers in the table
            noOfReseachersBefore = _dbContext.Researcher.Local.Count;

            //Executes the registration directly to the database through the controller and to the handler


           
            RegisterController rc = new RegisterController();
            rc.CreateResearcher(rrvm);
            _dbContext.Researcher.Load();

            //As the context keeps up to date with the database, the new count is one higher than before
            //(manually verified by debugging and checking the database)
            noOfReseachersAfter = _dbContext.Researcher.Local.Count;

            //Attaches the context to the database in order to be able to (in this instance) remove an entry directly.
            _dbContext.Database.EnsureCreated();
            //Removes researcher from database again to avoid clutter
            _dbContext.Remove(_dbContext.Researcher.Last(a => a.LastName == "integrationTestla"));
            _dbContext.SaveChanges();

            //Asserts that the number of researchs in the database is 1 higher after adding one.
            //Not affected by the remove-call, as the counts have already been saved.
            Assert.AreEqual(noOfReseachersAfter, noOfReseachersBefore + 1);
        }
    }
}

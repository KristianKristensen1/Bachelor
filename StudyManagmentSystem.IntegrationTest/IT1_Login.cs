//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Security.Claims;
//using BachelorBackEnd;
//using Castle.Core.Configuration;
//using FrontEndBA.Controllers;
//using FrontEndBA.Models;
//using JwtAuthenticationHelper.Abstractions;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Moq;
//using NUnit.Framework;

//namespace StudyManagmentSystem.IntegrationTest
//{
//    public class IT1_LoginHandler
//    {
//        readonly string connectionString = "Server = 35.228.116.222; Database = bachelordb; Uid = Admin; Pwd = admin1234";
//        DbContextOptionsBuilder<bachelordbContext> builder = new DbContextOptionsBuilder<bachelordbContext>();
//        private Researcher researcher = new Researcher();
        
//        private readonly Microsoft.Extensions.Configuration.IConfiguration configuration;
//        private readonly IJwtTokenGenerator tokenGenerator;



//        private static IEnumerable<Claim> AddMyClaims(UserInfo userInfo)
//        {
//            var myClaims = new List<Claim>
//            {
//                new Claim("HasAdminRights", userInfo.hasAdminRights ? "Y" : "N"),
//                new Claim("HasResearcherRights", userInfo.hasResearcherRights ? "Y" : "N"),
//                new Claim("HasParticipantRights", userInfo.hasParticipantRights ? "Y" : "N"),
//                new Claim("userID", userInfo.userID)
//            };
//            return myClaims;
//        }



//        [SetUp]
//        public void SetUp()
//        {
//            //Creates a researcher model for testing            
//            researcher = new Researcher()
//            {
//                Email = "test@register.com",
//                Password = "test1234",
//                Isadmin = true,
//                Isverified = true,
//                FirstName = "Nicola",
//                LastName = "Testla",
//            };
//        }

//        [Test]
//        public void LoginController_SuccesfulLogin_ResearcherIsInDBAsync()
//        {
//            builder.UseMySql(connectionString);

//            //Retrieves the data from the database and saves in it a context
//            bachelordbContext _dbContext = new bachelordbContext(builder.Options);
//            _dbContext.Researcher.Load();
//            _dbContext.Researcher.Add(researcher);
//            _dbContext.SaveChanges();


//            WelcomeController wc = new WelcomeController(tokenGenerator, configuration);
//            //await wc.LoginResearcher(researcher);

//            var result = wc.LoginResearcher(researcher) as RedirectToRouteResult;

//            //Attaches the context to the database in order to be able to (in this instance) remove an entry directly.
//            _dbContext.Database.EnsureCreated();
//            //Removes researcher from database again to avoid clutter
//            _dbContext.Remove(_dbContext.Researcher.Single(a => a.LastName == "Testla"));
//            _dbContext.SaveChanges();

//            Assert.AreEqual(result.RouteValues["action"].ToString(), "researcher");
//        }
//    }
//}

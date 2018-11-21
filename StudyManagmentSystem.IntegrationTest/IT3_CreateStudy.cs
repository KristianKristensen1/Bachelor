//using BachelorBackEnd;
//using Castle.Core.Configuration;
//using FrontEndBA.Controllers;
//using FrontEndBA.Models.CreateStudy;
//using FrontEndBA.Models.ResearcherModel.CreateStudyModel;
//using FrontEndBA.Models.SharedModels;
//using JwtAuthenticationHelper.Abstractions;
//using Microsoft.EntityFrameworkCore;
//using NUnit.Framework;
//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace StudyManagmentSystem.IntegrationTest
//{
//    class IT3_CreateStudy
//    {
//        //Connection string hardcoded (for now(?)), as there's some trouble reaching the config file.
//        readonly string connectionString = "Server = 35.228.116.222; Database = bachelordb; Uid = Admin; Pwd = admin1234";
//        DbContextOptionsBuilder<bachelordbContext> builder = new DbContextOptionsBuilder<bachelordbContext>();
//        CreateStudyModel csm;
//        StudyModel sm;
//        InclusioncriteriaModel icm;
//        int noOfInclusionCriteriaBefore = new int();
//        int noOfInclusionCriteriaAfter = new int();
//        int noOfStudiesBefore = new int();
//        int noOfStudiesAfter = new int();

//        private readonly IConfiguration configuration;
//        private readonly IJwtTokenGenerator tokenGenerator;

//        //public WelcomeController(IJwtTokenGenerator tokenGenerator, IConfiguration configuration)
//        //{
//        //    this.tokenGenerator = tokenGenerator;
//        //    this.configuration = configuration;
//        //}

//        [SetUp]
//        public void SetUp()
//        {
//            //OBS - Skal vi have en verificeret researcher for at kunne gemme et studie??? 

//            //Creates a study model for testing            
//            sm = new StudyModel()
//            {
//                IdResearcher = 0,
//                Abstract = "This is a test abstract",
//                Description = "This is a test description",
//                Duration = "This is a test duration",
//                Isdraft = false,
//                Location = "This is a test location",
//                Name = "This is a test study name",
//                Pay = 101,
//                Preparation = "This is a test preperation",
//                DateCreated = DateTime.Now,
//                DirecetStudyLink = "This is a test study link"
//            };

//            //Creates an inclusion criteria model for testing            
//            icm = new InclusioncriteriaModel()
//            {
//                English = false,
//                IsFemale = true,
//                IsMale = true,
//                MaxAge = 1001,
//                MinAge = 10                
//            };

//            //Creates a create study model for testing            
//            csm = new CreateStudyModel()
//            {
//                currentStudy = sm,
//                inclusioncriteria = icm
//            };

//            //Create an object with userinfo about the researcher
//            //TRYING IT OUT <--- OBS
//            var userInfo = new UserInfo
//            {
//                hasAdminRights = true,
//                hasResearcherRights = true,
//                hasParticipantRights = false,
//                userID = "" + 0
//            };

//            //Generates token with claims defined from the userinfo object.
//            var accessTokenResult = tokenGenerator.GenerateAccessTokenWithClaimsPrincipal(
//            researcher.Email,
//            AddMyClaims(userInfo));
//            await HttpContext.SignInAsync(accessTokenResult.ClaimsPrincipal,
//                accessTokenResult.AuthProperties);
//        }

//        [Test]
//        public void RegisterResearcher_SuccesfulRegistration_ResearcherIsInDB()
//        {
//            builder.UseMySql(connectionString);

//            //Retrieves the data from the database and saves in it a context
//            bachelordbContext _dbContext = new bachelordbContext(builder.Options);

//            //Loads the Researcher table into the context's local-instance
//            _dbContext.Study.Load();
//            _dbContext.Inclusioncriteria.Load();

//            //Saves the current number of researchers in the table
//            noOfStudiesBefore = _dbContext.Study.Local.Count;
//            noOfInclusionCriteriaBefore = _dbContext.Inclusioncriteria.Local.Count;

//            //Executes the registration directly to the database through the controller and to the handler
//            CreateStudyController rc = new CreateStudyController();
//            rc.Create(csm);
//            _dbContext.Researcher.Load();

//            //As the context keeps up to date with the database, the new count is one higher than before
//            //(manually verified by debugging and checking the database)
//            noOfStudiesAfter = _dbContext.Study.Local.Count;
//            noOfInclusionCriteriaAfter = _dbContext.Study.Local.Count;

//            //Attaches the context to the database in order to be able to (in this instance) remove an entry directly.
//            _dbContext.Database.EnsureCreated();
//            //Removes researcher from database again to avoid clutter
//            _dbContext.Remove(_dbContext.Study.Single(a => a.Name == "This is a test study name"));
//            _dbContext.Remove(_dbContext.Inclusioncriteria.Single(a => a.MaxAge == 1001));
//            _dbContext.SaveChanges();

//            //Asserts that the number of researchs in the database is 1 higher after adding one.
//            //Not affected by the remove-call, as the counts have already been saved.
//            Assert.AreEqual(noOfStudiesAfter, noOfStudiesBefore + 1);
//            Assert.AreEqual(noOfStudiesAfter, noOfInclusionCriteriaBefore + 1);
//        }
//    }
//}
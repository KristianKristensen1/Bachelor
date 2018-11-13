using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using NUnit.Framework;
using Microsoft.EntityFrameworkCore;
using Moq;
using BachelorBackEnd;

namespace Tests
{
    public class T3_ManageStudyHandler_P
    {
        public IManageStudyHandler uut;
     
        public Study study;
        public IQueryable studies;


        [SetUp]
    public void SetUp()
        {
 
            studies = new List<Study>
            {
                new Study
                {
                    Name = "New Study for people!",
                    IdStudy = 5,
                    Description = "Ladies and gentlemen, this is study no. 5",
                    Isdraft = true,
                    IdResearcher = 1,
                }
            }.AsQueryable();

       
        }
    }
}

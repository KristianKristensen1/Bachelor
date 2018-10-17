using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("To register participant press p, to register researcher press r");
            string x = Console.ReadLine();
            if (x == "p")
            {
                //Test of register participant
                RegisterHandler rh = new RegisterHandler();
                Participant part = new Participant();
                part.Age = DateTime.Parse("1994-01-01");
                part.English = true;
                part.Gender = true;
                Console.WriteLine("Input email: ");
                part.Email = Console.ReadLine();
                Console.WriteLine("Input password: ");
                part.Password = Console.ReadLine();
                rh.RegisterParticipantDB(part, out string ErrorMessage);
            }
            if (x == "r")
            {
                //Test of register researcher
                RegisterHandler rh = new RegisterHandler();
                Researcher res = new Researcher();
                Console.WriteLine("Input name: ");
                res.Name = Console.ReadLine();
                Console.WriteLine("Input email: ");
                res.Email = Console.ReadLine();
                Console.WriteLine("Input password: ");
                res.Password = Console.ReadLine();
                //rh.RegisterResearcherDB(res);
            }
            if (x == "h")
            {
                //CREATE RESEARCHER                
                Researcher res = new Researcher();
                res.IdResearcher = 117; res.Name = "Casper Testesen"; res.Email = "Tjololo@hep.com"; res.Password = "howsa";
                //RegisterHandler rh = new RegisterHandler(); rh.RegisterResearcherDB(res, out string ErrorMessage);

                //CREATE PARTICIPANT
                Participant part = new Participant(); part.IdParticipant = 119; part.Age = DateTime.Parse("1995-01-01"); part.English = true; part.Gender = true; part.Email = "HamDenLange@wow.com"; part.Password = "senjorsnek";

                //CREATE STUDY AND CRITERIA
                Study stud = new Study(); stud.IdStudy = 118; stud.Description = "Test Study"; stud.IdResearcher = res.IdResearcher; stud.Isdraft = true; stud.Tag = "TestTag";
                Inclusioncriteria crit = new Inclusioncriteria(); crit.MinAge = 15; crit.MaxAge = 25; crit.Male = 0; crit.Female = 1; crit.English = 0; crit.IdStudy = stud.IdStudy;
                ManageStudyHandler msh = new ManageStudyHandler(); //msh.CreateStudyDB(stud, crit);

                //CREATE STUDY-PARTICIPANT LINK
                //msh.AddParticipantDB(part.Email, stud); //not-implemented

                ////ALL STUDIES TEST
                //List<Study> studList = msh.GetAllStudiesDB();

                ////MY STUDIES RESEARCHER TEST
                //List<Study> studList = msh.GetMyResearcherStudiesDB(res.IdResearcher);

                ////MY STUDIES PARTICIPANT TEST
                //List<Study> studList = msh.GetMyParticipantStudiesDB(part.IdParticipant); //Impeded

                ////RELEVANT STUDIES TEST
                List<Study> studList = msh.GetRelevantStudiesDB(part);

                if ((studList != null) && (!studList.Any() && studList.Count !=0))
                {
                    foreach (var s in studList)
                    {
                        Console.WriteLine("___Description: " + s.Description + "___IDResearcher: " + s.IdResearcher + "___IsDraft: " + s.Isdraft + "___Tag: " + s.Tag + "\n");
                    }
                }
                else { Console.WriteLine("No Available Studies"); }
                Console.ReadLine();
            }
        }
    }
}

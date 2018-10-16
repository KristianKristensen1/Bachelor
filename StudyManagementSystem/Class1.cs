﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudyManagementSystem.Models;

namespace BachelorBackEnd
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("To register participant press p, to register researcher press r");
            string x = Console.ReadLine();
            if(x == "p")
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
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class LoginStatus
    {
        public bool IsSuccess { set; get; }
        public string ErrorMessage { set; get; }
        public Researcher researcher { get; set; }
        public Participant participant { get; set; }
    }

    public class LoginEntity
    {
        public LoginStatus LoginStatus { set; get; }

        public LoginEntity()
        {
            if (LoginStatus == null)
                LoginStatus = new LoginStatus();
        }
    }
}
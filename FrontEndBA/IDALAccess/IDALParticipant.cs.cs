﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FrontEndBA.IDALAccess
{
    public interface IDALParticipant
    {
        void SaveRegisterDto(BachelorBackEnd.Participants participantobj);
    }
}

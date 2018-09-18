using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class Participants
    {
        public int IdParticipant { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public sbyte Pause { get; set; }
    }
}

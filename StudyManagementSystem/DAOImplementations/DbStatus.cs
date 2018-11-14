using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public class DbStatus
    {
        public bool success { get; set; }

        public string errormessage { get; set; }

        public string participantEmail { get; set; }

        public Participant participant { get; set; }

        public Researcher researcher { get; set; }
    }
}

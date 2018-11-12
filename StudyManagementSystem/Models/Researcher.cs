using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class Researcher
    {
        public Researcher()
        {
            Study = new HashSet<Study>();
        }

        public int IdResearcher { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Isverified { get; set; }
        public bool Isadmin { get; set; }

        public ICollection<Study> Study { get; set; }
    }
}

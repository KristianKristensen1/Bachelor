using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{
    public interface IResearcherDAO
    {
        void AddResearcher();

        void DeleteResearcher();

        void GetResearcher();

        void GetAllResearchers();

        void VerifyResearcher();
    }
}

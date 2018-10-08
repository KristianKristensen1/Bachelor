using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BachelorBackEnd
{

    public class OperationStatus
    {
        public bool IsSuccess { set; get; }
        public string ErrorMessage { set; get; }
        public Researcher researcher { get; set; }
        public Participant participant { get; set; }
    }



    public class BaseEntity
    {
        public OperationStatus OperationStatus { set; get; }

        public BaseEntity()
        {
            if (OperationStatus == null)
                OperationStatus = new OperationStatus();
        }
    }
}

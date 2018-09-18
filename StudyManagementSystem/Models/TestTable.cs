using System;
using System.Collections.Generic;

namespace BachelorBackEnd
{
    public partial class TestTable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public sbyte Smoker { get; set; }
        public DateTime Timestamp { get; set; }
    }
}

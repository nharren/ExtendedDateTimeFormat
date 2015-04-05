using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Calculation
    {
        public Calculation(Func<string> execute, string description)
        {
            Execute = execute;
            Description = description;
        }

        public Func<string> Execute { get; set; }

        public string Description { get; set; }
    }
}

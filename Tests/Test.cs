using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class Test
    {
        private string results;

        public List<string> Strings { get; set; }

        public string Name { get; set; }

        public string Results
        {
            get
            {
                if (results == null)
                {
                    results = TestProcessor.Process(Strings);
                }

                return results;
            }
        }
    }
}

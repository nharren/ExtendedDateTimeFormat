using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public class CalculationTest : Test
    {
        public CalculationTest(string name, IEnumerable<Calculation> calculations)
        {
            Name = name;
            Calculations = calculations;
            Category = "Calculation";
        }

        public IEnumerable<Calculation> Calculations { get; set; }

        public override void Begin()
        {
            Worker.DoWork += (o, e) =>
            {
                var stringBuilder = new StringBuilder();

                foreach (var calculation in Calculations)
                {
                    stringBuilder.AppendLine("Description:").AppendLine();
                    stringBuilder.AppendLine(calculation.Description);
                    stringBuilder.AppendLine().Append("Result: ").AppendLine(calculation.Execute()).AppendLine().AppendLine(); 
                }

                e.Result = stringBuilder.ToString();
            };


            Worker.RunWorkerAsync();
        }
    }
}

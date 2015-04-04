using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public interface ITest
    {
        string Name { get; }
        BackgroundWorker Worker { get; }
        void Begin();
    }
}

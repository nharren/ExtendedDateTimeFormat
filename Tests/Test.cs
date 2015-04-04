using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    public abstract class Test
    {
        public Test()
        {
            Worker = new BackgroundWorker { WorkerReportsProgress = true };
        }

        public string Name { get; protected set; }
        public string Category { get; protected set; }
        public BackgroundWorker Worker { get; private set; }
        public abstract void Begin();
    }
}

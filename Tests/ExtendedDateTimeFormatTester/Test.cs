using System.ComponentModel;

namespace ExtendedDateTimeFormatTester
{
    public abstract class Test
    {
        protected string _category;
        protected string _name;
        protected BackgroundWorker _worker;

        public Test()
        {
            _worker = new BackgroundWorker();
            _worker.WorkerReportsProgress = true;
        }

        public string Category
        {
            get
            {
                return _category;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public BackgroundWorker Worker
        {
            get
            {
                return _worker;
            }
        }
    }
}
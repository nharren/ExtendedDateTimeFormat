using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Tests
{
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty TestsProperty = DependencyProperty.Register("Tests", typeof(List<Test>), typeof(MainWindow));

        public MainWindow()
        {
            Tests = new List<Test>();

            this.Tests.Add(new Test { Name = "Specification Strings", Strings = TestStrings.SpecificationStrings });
            this.Tests.Add(new Test { Name = "Malformed Strings", Strings = TestStrings.MalformedStrings });
            this.Tests.Add(new Test { Name = "Other Strings", Strings = TestStrings.OtherStrings });
            this.Tests.Add(new Test { Name = "All", Strings = TestStrings.SpecificationStrings.Concat(TestStrings.MalformedStrings).Concat(TestStrings.OtherStrings).ToList() });

            InitializeComponent();
        }

        public List<Test> Tests
        {
            get
            {
                return (List<Test>)GetValue(TestsProperty);
            }
            set
            {
                SetValue(TestsProperty, value);
            }
        }
    }
}
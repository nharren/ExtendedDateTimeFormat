using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Tests
{
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty TestsProperty = DependencyProperty.Register("Tests", typeof(List<ITest>), typeof(MainWindow));

        public MainWindow()
        {
            TestCollection = new List<ITest>();

            this.TestCollection.Add(new ParsingTest("Parsing Specification Strings", TestStrings.SpecificationStrings));
            this.TestCollection.Add(new ParsingTest("Parsing Malformed Strings", TestStrings.MalformedStrings));
            this.TestCollection.Add(new ParsingTest("Parsing Other Strings", TestStrings.OtherStrings));
            this.TestCollection.Add(new HashCodeTest(new ExtendedDateTime(1600, 1, 1, 0, 0, 0, TimeSpan.Zero), new ExtendedDateTime(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), 100));

            foreach (var test in TestCollection)
            {
                test.Worker.ProgressChanged += (o, e) =>
                {
                    if (progress.Visibility == Visibility.Collapsed)
                    {
                        progress.Visibility = Visibility.Visible;
                    }

                    progress.Value = e.ProgressPercentage;
                };

                test.Worker.RunWorkerCompleted += (o, e) =>
                {
                    progress.Visibility = Visibility.Collapsed;

                    TextBlockInput.SetFormattedText(resultsBox, (string)e.Result);
                };
            }

            InitializeComponent();
        }

        public List<ITest> TestCollection
        {
            get
            {
                return (List<ITest>)GetValue(TestsProperty);
            }
            set
            {
                SetValue(TestsProperty, value);
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (ITest test in e.AddedItems)
            {
                test.Begin();
            }
        }
    }
}
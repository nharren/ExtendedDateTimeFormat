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
        public static readonly DependencyProperty TestCollectionProperty = DependencyProperty.Register("TestCollection", typeof(List<Test>), typeof(MainWindow));

        public MainWindow()
        {
            TestCollection = new List<Test>();
            TestCollection.Add(new ParsingTest("Date Strings", TestStrings.DateStrings));
            TestCollection.Add(new ParsingTest("Date and Time Strings", TestStrings.DateAndTimeStrings));
            TestCollection.Add(new ParsingTest("Interval Strings", TestStrings.IntervalStrings));
            TestCollection.Add(new ParsingTest("Level Zero Strings", TestStrings.LevelZeroStrings));
            TestCollection.Add(new ParsingTest("Uncertain or Approximate Strings", TestStrings.UncertainOrApproximateStrings));
            TestCollection.Add(new ParsingTest("Unspecified Strings", TestStrings.UnspecifiedStrings));
            TestCollection.Add(new ParsingTest("Level One Extended Interval Strings", TestStrings.L1ExtendedIntervalStrings));
            TestCollection.Add(new ParsingTest("Year Exceeding Four Digits Strings", TestStrings.YearExceedingFourDigitsStrings));
            TestCollection.Add(new ParsingTest("Season Strings", TestStrings.SeasonStrings));
            TestCollection.Add(new ParsingTest("Level One Extension Strings", TestStrings.LevelOneExtensionStrings));
            TestCollection.Add(new ParsingTest("Partial Uncertain Or Approximate Strings", TestStrings.PartialUncertainOrApproximateStrings));
            TestCollection.Add(new ParsingTest("Partial Unspecified Strings", TestStrings.PartialUnspecifiedStrings));
            TestCollection.Add(new ParsingTest("One Of A Set Strings", TestStrings.OneOfASetStrings));
            TestCollection.Add(new ParsingTest("Multiple Date Strings", TestStrings.MultipleDateStrings));
            TestCollection.Add(new ParsingTest("Masked Precision Strings", TestStrings.MaskedPrecisionStrings));
            TestCollection.Add(new ParsingTest("Level Two Extended Interval Strings", TestStrings.LevelTwoExtendedIntervalStrings));
            TestCollection.Add(new ParsingTest("Exponential Form of Year Exceeding Four Digits Strings", TestStrings.ExponentialFormOfYearsExeedingFourDigitsStrings));
            TestCollection.Add(new ParsingTest("Level Two Extension Strings", TestStrings.LevelTwoExtensionStrings));
            TestCollection.Add(new ParsingTest("Specification Strings", TestStrings.SpecificationStrings));
            TestCollection.Add(new ParsingTest("Malformed Strings", TestStrings.MalformedStrings));
            TestCollection.Add(new ParsingTest("Other Strings", TestStrings.OtherStrings));
            TestCollection.Add(new HashCodeTest(new ExtendedDateTime(1600, 1, 1, 0, 0, 0, TimeSpan.Zero), new ExtendedDateTime(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), 100));

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

        public List<Test> TestCollection
        {
            get
            {
                return (List<Test>)GetValue(TestCollectionProperty);
            }
            set
            {
                SetValue(TestCollectionProperty, value);
            }
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Test test in e.AddedItems)
            {
                test.Begin();
            }
        }
    }
}
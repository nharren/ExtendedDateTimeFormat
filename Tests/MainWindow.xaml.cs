using System;
using System.Collections.Generic;
using System.ExtendedDateTimeFormat;
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
            TestCollection.Add(new ParsingTest("Date", TestStrings.DateStrings));
            TestCollection.Add(new ParsingTest("Date and Time", TestStrings.DateAndTimeStrings));
            TestCollection.Add(new ParsingTest("Intervals", TestStrings.IntervalStrings));
            TestCollection.Add(new ParsingTest("L0 Features", TestStrings.LevelZeroStrings));
            TestCollection.Add(new ParsingTest("Uncertain and Approximate", TestStrings.UncertainOrApproximateStrings));
            TestCollection.Add(new ParsingTest("Unspecified Dates", TestStrings.UnspecifiedStrings));
            TestCollection.Add(new ParsingTest("L1 Extended Intervals", TestStrings.L1ExtendedIntervalStrings));
            TestCollection.Add(new ParsingTest("Years Exceeding Four Digits", TestStrings.YearExceedingFourDigitsStrings));
            TestCollection.Add(new ParsingTest("Seasons", TestStrings.SeasonStrings));
            TestCollection.Add(new ParsingTest("L1 Extensions", TestStrings.LevelOneExtensionStrings));
            TestCollection.Add(new ParsingTest("Partially Uncertain and Approximate", TestStrings.PartialUncertainOrApproximateStrings));
            TestCollection.Add(new ParsingTest("Partially Unspecified Dates", TestStrings.PartialUnspecifiedStrings));
            TestCollection.Add(new ParsingTest("One of a Set", TestStrings.OneOfASetStrings));
            TestCollection.Add(new ParsingTest("Multiple Dates", TestStrings.MultipleDateStrings));
            TestCollection.Add(new ParsingTest("Masked Precision", TestStrings.MaskedPrecisionStrings));
            TestCollection.Add(new ParsingTest("L2 Extended Intervals", TestStrings.LevelTwoExtendedIntervalStrings));
            TestCollection.Add(new ParsingTest("Exponential Years", TestStrings.ExponentialFormOfYearsExeedingFourDigitsStrings));
            TestCollection.Add(new ParsingTest("L2 Extensions", TestStrings.LevelTwoExtensionStrings));
            TestCollection.Add(new ParsingTest("All Specification Features", TestStrings.SpecificationStrings));
            TestCollection.Add(new ParsingTest("Malformed Strings", TestStrings.MalformedStrings));
            TestCollection.Add(new ParsingTest("Other Strings", TestStrings.OtherStrings));
            TestCollection.Add(new SerializationTest("Date", TestStrings.DateStrings));
            TestCollection.Add(new SerializationTest("Date and Time", TestStrings.DateAndTimeStrings));
            TestCollection.Add(new SerializationTest("Intervals", TestStrings.IntervalStrings));
            TestCollection.Add(new SerializationTest("L0 Features", TestStrings.LevelZeroStrings));
            TestCollection.Add(new SerializationTest("Uncertain and Approximate", TestStrings.UncertainOrApproximateStrings));
            TestCollection.Add(new SerializationTest("Unspecified Dates", TestStrings.UnspecifiedStrings));
            TestCollection.Add(new SerializationTest("L1 Extended Intervals", TestStrings.L1ExtendedIntervalStrings));
            TestCollection.Add(new SerializationTest("Years Exceeding Four Digits", TestStrings.YearExceedingFourDigitsStrings));
            TestCollection.Add(new SerializationTest("Seasons", TestStrings.SeasonStrings));
            TestCollection.Add(new SerializationTest("L1 Extensions", TestStrings.LevelOneExtensionStrings));
            TestCollection.Add(new SerializationTest("Partially Uncertain and Approximate", TestStrings.PartialUncertainOrApproximateStrings));
            TestCollection.Add(new SerializationTest("Partially Unspecified Dates", TestStrings.PartialUnspecifiedStrings));
            TestCollection.Add(new SerializationTest("One of a Set", TestStrings.OneOfASetStrings));
            TestCollection.Add(new SerializationTest("Multiple Dates", TestStrings.MultipleDateStrings));
            TestCollection.Add(new SerializationTest("Masked Precision", TestStrings.MaskedPrecisionStrings));
            TestCollection.Add(new SerializationTest("L2 Extended Intervals", TestStrings.LevelTwoExtendedIntervalStrings));
            TestCollection.Add(new SerializationTest("Exponential Years", TestStrings.ExponentialFormOfYearsExeedingFourDigitsStrings));
            TestCollection.Add(new SerializationTest("L2 Extensions", TestStrings.LevelTwoExtensionStrings));
            TestCollection.Add(new SerializationTest("All Specification Features", TestStrings.SpecificationStrings));
            TestCollection.Add(new SerializationTest("Other Strings", TestStrings.OtherStrings));
            TestCollection.Add(new HashCodeTest(new ExtendedDateTime(1600, 1, 1, 0, 0, 0, TimeSpan.Zero), new ExtendedDateTime(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), 100));
            TestCollection.Add(new CalculationTest("Total Months", Calculations.TotalMonthsCalculations));
            TestCollection.Add(new CalculationTest("Total Years", Calculations.TotalYearsCalculations));

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
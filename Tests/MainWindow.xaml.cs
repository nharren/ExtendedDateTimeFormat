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
            TestCollection.Add(new StringSerializationTest("Date", TestStrings.DateStrings));
            TestCollection.Add(new StringSerializationTest("Date and Time", TestStrings.DateAndTimeStrings));
            TestCollection.Add(new StringSerializationTest("Intervals", TestStrings.IntervalStrings));
            TestCollection.Add(new StringSerializationTest("L0 Features", TestStrings.LevelZeroStrings));
            TestCollection.Add(new StringSerializationTest("Uncertain and Approximate", TestStrings.UncertainOrApproximateStrings));
            TestCollection.Add(new StringSerializationTest("Unspecified Dates", TestStrings.UnspecifiedStrings));
            TestCollection.Add(new StringSerializationTest("L1 Extended Intervals", TestStrings.L1ExtendedIntervalStrings));
            TestCollection.Add(new StringSerializationTest("Years Exceeding Four Digits", TestStrings.YearExceedingFourDigitsStrings));
            TestCollection.Add(new StringSerializationTest("Seasons", TestStrings.SeasonStrings));
            TestCollection.Add(new StringSerializationTest("L1 Extensions", TestStrings.LevelOneExtensionStrings));
            TestCollection.Add(new StringSerializationTest("Partially Uncertain and Approximate", TestStrings.PartialUncertainOrApproximateStrings));
            TestCollection.Add(new StringSerializationTest("Partially Unspecified Dates", TestStrings.PartialUnspecifiedStrings));
            TestCollection.Add(new StringSerializationTest("One of a Set", TestStrings.OneOfASetStrings));
            TestCollection.Add(new StringSerializationTest("Multiple Dates", TestStrings.MultipleDateStrings));
            TestCollection.Add(new StringSerializationTest("Masked Precision", TestStrings.MaskedPrecisionStrings));
            TestCollection.Add(new StringSerializationTest("L2 Extended Intervals", TestStrings.LevelTwoExtendedIntervalStrings));
            TestCollection.Add(new StringSerializationTest("Exponential Years", TestStrings.ExponentialFormOfYearsExeedingFourDigitsStrings));
            TestCollection.Add(new StringSerializationTest("L2 Extensions", TestStrings.LevelTwoExtensionStrings));
            TestCollection.Add(new StringSerializationTest("All Specification Features", TestStrings.SpecificationStrings));
            TestCollection.Add(new StringSerializationTest("Other Strings", TestStrings.OtherStrings));
            TestCollection.Add(new XmlSerializationTest("Date", TestStrings.DateStrings));
            TestCollection.Add(new XmlSerializationTest("Date and Time", TestStrings.DateAndTimeStrings));
            TestCollection.Add(new XmlSerializationTest("Intervals", TestStrings.IntervalStrings));
            TestCollection.Add(new XmlSerializationTest("L0 Features", TestStrings.LevelZeroStrings));
            TestCollection.Add(new XmlSerializationTest("Uncertain and Approximate", TestStrings.UncertainOrApproximateStrings));
            TestCollection.Add(new XmlSerializationTest("Unspecified Dates", TestStrings.UnspecifiedStrings));
            TestCollection.Add(new XmlSerializationTest("L1 Extended Intervals", TestStrings.L1ExtendedIntervalStrings));
            TestCollection.Add(new XmlSerializationTest("Years Exceeding Four Digits", TestStrings.YearExceedingFourDigitsStrings));
            TestCollection.Add(new XmlSerializationTest("Seasons", TestStrings.SeasonStrings));
            TestCollection.Add(new XmlSerializationTest("L1 Extensions", TestStrings.LevelOneExtensionStrings));
            TestCollection.Add(new XmlSerializationTest("Partially Uncertain and Approximate", TestStrings.PartialUncertainOrApproximateStrings));
            TestCollection.Add(new XmlSerializationTest("Partially Unspecified Dates", TestStrings.PartialUnspecifiedStrings));
            TestCollection.Add(new XmlSerializationTest("One of a Set", TestStrings.OneOfASetStrings));
            TestCollection.Add(new XmlSerializationTest("Multiple Dates", TestStrings.MultipleDateStrings));
            TestCollection.Add(new XmlSerializationTest("Masked Precision", TestStrings.MaskedPrecisionStrings));
            TestCollection.Add(new XmlSerializationTest("L2 Extended Intervals", TestStrings.LevelTwoExtendedIntervalStrings));
            TestCollection.Add(new XmlSerializationTest("Exponential Years", TestStrings.ExponentialFormOfYearsExeedingFourDigitsStrings));
            TestCollection.Add(new XmlSerializationTest("L2 Extensions", TestStrings.LevelTwoExtensionStrings));
            TestCollection.Add(new XmlSerializationTest("All Specification Features", TestStrings.SpecificationStrings));
            TestCollection.Add(new XmlSerializationTest("Other Strings", TestStrings.OtherStrings));
            TestCollection.Add(new HashCodeTest(new ExtendedDateTime(1600, 1, 1, 0, 0, 0, TimeSpan.Zero), new ExtendedDateTime(2000, 1, 1, 0, 0, 0, TimeSpan.Zero), 100));
            TestCollection.Add(new CalculationTest("Total Months", Calculations.TotalMonthsCalculations));
            TestCollection.Add(new CalculationTest("Total Years", Calculations.TotalYearsCalculations));
            TestCollection.Add(new CalculationTest("Differences", Calculations.DifferenceCalculations));

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

                    resultsBox.Text = (string)e.Result;
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
using System;
using System.Collections.Generic;
using System.EDTF;
using System.Windows;
using System.Windows.Controls;

namespace ExtendedDateTimeFormatTester
{
    public partial class MainWindow : Window
    {
        public static readonly DependencyProperty TestCollectionProperty = DependencyProperty.Register("TestCollection", typeof(List<Test>), typeof(MainWindow));

        public MainWindow()
        {
            TestCollection = new List<Test>();
            TestCollection.Add(new ParsingTest("Date", ParsingTestEntries.DateEntries));
            TestCollection.Add(new ParsingTest("Date and Time", ParsingTestEntries.DateAndTimeEntries));
            TestCollection.Add(new ParsingTest("Intervals", ParsingTestEntries.IntervalEntries));
            TestCollection.Add(new ParsingTest("L0 Features", ParsingTestEntries.L0Entries));
            TestCollection.Add(new ParsingTest("Uncertain and Approximate", ParsingTestEntries.UncertainOrApproximateEntries));
            TestCollection.Add(new ParsingTest("Unspecified Dates", ParsingTestEntries.UnspecifiedEntries));
            TestCollection.Add(new ParsingTest("L1 Extended Intervals", ParsingTestEntries.L1ExtendedIntervalEntries));
            TestCollection.Add(new ParsingTest("Years Exceeding Four Digits", ParsingTestEntries.YearExceedingFourDigitsEntries));
            TestCollection.Add(new ParsingTest("Seasons", ParsingTestEntries.SeasonEntries));
            TestCollection.Add(new ParsingTest("L1 Extensions", ParsingTestEntries.L1ExtensionEntries));
            TestCollection.Add(new ParsingTest("Partially Uncertain and Approximate", ParsingTestEntries.PartialUncertainOrApproximateEntries));
            TestCollection.Add(new ParsingTest("Partially Unspecified Dates", ParsingTestEntries.PartialUnspecifiedEntries));
            TestCollection.Add(new ParsingTest("One of a Set", ParsingTestEntries.OneOfASetEntries));
            TestCollection.Add(new ParsingTest("Multiple Dates", ParsingTestEntries.MultipleDateEntries));
            TestCollection.Add(new ParsingTest("Masked Precision", ParsingTestEntries.MaskedPrecisionEntries));
            TestCollection.Add(new ParsingTest("L2 Extended Intervals", ParsingTestEntries.L2ExtendedIntervalEntries));
            TestCollection.Add(new ParsingTest("Exponential Years", ParsingTestEntries.ExponentialFormOfYearsExceedingFourDigitsEntries));
            TestCollection.Add(new ParsingTest("L2 Extensions", ParsingTestEntries.L2ExtensionEntries));
            TestCollection.Add(new ParsingTest("All Specification Features", ParsingTestEntries.SpecificationFeatures));
            TestCollection.Add(new StringSerializationTest("Date", StringSerializationTestEntries.DateEntries));
            TestCollection.Add(new StringSerializationTest("Date and Time", StringSerializationTestEntries.DateAndTimeEntries));
            TestCollection.Add(new StringSerializationTest("Intervals", StringSerializationTestEntries.IntervalEntries));
            TestCollection.Add(new StringSerializationTest("L0 Features", StringSerializationTestEntries.L0Entries));
            TestCollection.Add(new StringSerializationTest("Uncertain and Approximate", StringSerializationTestEntries.UncertainOrApproximateEntries));
            TestCollection.Add(new StringSerializationTest("Unspecified Dates", StringSerializationTestEntries.UnspecifiedEntries));
            TestCollection.Add(new StringSerializationTest("L1 Extended Intervals", StringSerializationTestEntries.L1ExtendedIntervalEntries));
            TestCollection.Add(new StringSerializationTest("Long Years", StringSerializationTestEntries.YearExceedingFourDigitsEntries));
            TestCollection.Add(new StringSerializationTest("Seasons", StringSerializationTestEntries.SeasonEntries));
            TestCollection.Add(new StringSerializationTest("L1 Extensions", StringSerializationTestEntries.L1ExtensionEntries));
            TestCollection.Add(new StringSerializationTest("Partially Uncertain and Approximate", StringSerializationTestEntries.PartialUncertainOrApproximateEntries));
            TestCollection.Add(new StringSerializationTest("Partially Unspecified", StringSerializationTestEntries.PartialUnspecifiedEntries));
            TestCollection.Add(new StringSerializationTest("One of a Set", StringSerializationTestEntries.OneOfASetEntries));
            TestCollection.Add(new StringSerializationTest("Multiple Dates", StringSerializationTestEntries.MultipleDateEntries));
            TestCollection.Add(new StringSerializationTest("L2 Extended Intervals", StringSerializationTestEntries.L2ExtendedIntervalEntries));
            TestCollection.Add(new StringSerializationTest("Exponential Years", StringSerializationTestEntries.ExponentialFormOfYearsExceedingFourDigitsEntries));
            TestCollection.Add(new StringSerializationTest("L2 Extensions", StringSerializationTestEntries.L2ExtensionEntries));
            TestCollection.Add(new StringSerializationTest("All Specification Features", StringSerializationTestEntries.SpecificationFeatures));
            TestCollection.Add(new XmlSerializationTest("Intervals", XmlSerializationTestEntries.Intervals));
            TestCollection.Add(new XmlSerializationTest("Collections", XmlSerializationTestEntries.Collections));
            TestCollection.Add(new XmlSerializationTest("Possibility Collections", XmlSerializationTestEntries.PossibilityCollections));
            TestCollection.Add(new XmlSerializationTest("Extended Datetimes", XmlSerializationTestEntries.ExtendedDateTimes));
            TestCollection.Add(new XmlSerializationTest("Unspecified Extended Datetimes", XmlSerializationTestEntries.UnspecifiedExtendedDateTimes));
            TestCollection.Add(new HashCodeTest(new ExtendedDateTime(-1000, 1, 1, 0, 0, 0, TimeSpan.Zero), new ExtendedDateTime(1000, 1, 1, 0, 0, 0, TimeSpan.Zero), 97));
            TestCollection.Add(new CalculationTest("Total Months", CalculationTestEntries.TotalMonths));
            TestCollection.Add(new CalculationTest("Total Years", CalculationTestEntries.TotalYears));
            TestCollection.Add(new CalculationTest("Difference", CalculationTestEntries.Difference));
            TestCollection.Add(new CalculationTest("Add Months", CalculationTestEntries.AddMonths));
            TestCollection.Add(new CalculationTest("Add Years", CalculationTestEntries.AddYears));

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
                    results.ItemsSource = (IEnumerable<TestResult>)e.Result;
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
                test.Worker.RunWorkerAsync();
            }
        }
    }
}
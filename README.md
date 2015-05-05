## Introduction

The [Extended Date Time Format (EDTF)](http://www.loc.gov/standards/datetime/pre-submission.html) is a proposed extension to the standard datetime format (ISO 8601). It includes support for:

- Uncertain or approximate dates (e.g. "Around 1995" or "1995, though it can't be confirmed.'")
- Sets of possible dates (e.g. "1992, 1994, *or* 1996")
- Sets of dates (e.g, "1992, 1994, *and* 1996")
- Intervals (e.g. "From 1992 to 1999");
- Partially unspecified dates. (e.g. "Only the first two digits of the year have been supplied so far.");
- Masked precision (e.g. "Some date in the 1950s.")
- Seasons (e.g. "Spring of 1992")

This library implements all of the EDTF features in .NET.

## Examples

#### Dates and Times

```csharp
var year   = new ExtendedDateTime(2015);
var month  = new ExtendedDateTime(2015, 4);
var day    = new ExtendedDateTime(2015, 4, 16);
var hour   = new ExtendedDateTime(2015, 4, 16, 3);
var minute = new ExtendedDateTime(2015, 4, 16, 3, 26);
var second = new ExtendedDateTime(2015, 4, 16, 3, 26, 25);
``` 

#### UTC Offsets

```csharp
var local = new ExtendedDateTime(2015, 4, 16, 3, 26, 25);
var utc   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, TimeSpan.Zero);
var est   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, TimeSpan.FromHours(-5);
var nst   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, new TimeSpan(-3, -30, 0);
``` 

#### Approximation and Uncertainty

```csharp
var uncertainMonth       = new ExtendedDateTime(2015, 4, MonthFlags.Uncertain);
var approximateMonth     = new ExtendedDateTime(2015, 4, MonthFlags.Approximate);
var uncertainApproxMonth = new ExtendedDateTime(2015, 4, MonthFlags.Uncertain | MonthFlags.Approximate);
```

#### Seasons

```csharp
var season            = ExtendedDateTime.FromSeason(2015, Season.Spring);
var qualifiedSeason   = ExtendedDateTime.FromSeason(2105, Season.Spring, "NorthernHemisphere");
var approximateSeason = ExtendedDateTime.FromSeason(2105, Season.Spring, SeasonFlags.Approximate);
```

#### Long Years

```csharp
var longYear = ExtendedDateTime.FromLongYear(-2000000);
```

#### Long Years in Scientific Notation

```csharp
var longScientificYear              = ExtendedDateTime.FromScientificNotation(-2, 6);
var longScientificYearWithPrecision = ExtendedDateTime.FromScientificNotation(-2, 6, 1);
```

#### Date and Time Arithmetic

```csharp
var difference = new ExtendedDateTime(2006) - new ExtendedDateTime(2003);
var difference = new ExtendedDateTime(2006) - new TimeSpan(5, 5, 5, 5);
var difference = new ExtendedDateTime(2006).SubtractYears(5);
var difference = new ExtendedDateTime(2006).SubtractMonths(5);
var sum        = new ExtendedDateTime(2006) + new TimeSpan(5, 5, 5, 5);
var sum        = new ExtendedDateTime(2006).AddYears(5);
var sum        = new ExtendedDateTime(2006).AddMonths(5);
```

#### Other Calculations

```csharp
var daysInMonth = ExtendedDateTimeCalculator.DaysInMonth(2015, 4);
var daysInYear  = ExtendedDateTimeCalculator.DaysInYear(2015);
var isLeapYear  = ExtendedDateTimeCalculator.IsLeapYear(2015);
var totalMonths = (new ExtendedDateTime(2006, 1, 1) - new ExtendedDateTime(2003, 2, 2)).TotalMonths;
var totalYears  = (new ExtendedDateTime(2006, 1, 1) - new ExtendedDateTime(2003, 2, 2)).TotalYears;
```

#### Collections of Dates and Times
```csharp
var collection          = new ExtendedDateTimeCollection { new ExtendedDateTime(1560), new ExtendedDateTime(1570), new ExtendedDateTime(1590) };
var collectionWithRange = new ExtendedDateTimeCollection { new ExtendedDateTime(1560), new ExtendedDateTimeRange(new ExtendedDateTime(1570), new ExtendedDateTime(1590)) };
```

#### Collection of Possible Dates and Times
```csharp
var collection          = new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1560), new ExtendedDateTime(1570), new ExtendedDateTime(1590) };
var collectionWithRange = new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(1560), new ExtendedDateTimeRange(new ExtendedDateTime(1570), new ExtendedDateTime(1590)) };
```

#### Extremities

```csharp
var earliestDateTime = ExtendedDateTimeInterval.Parse("[1560,1570,1590]/[1760,1770,1775]").Earliest();
var latestDateTime   = ExtendedDateTimeInterval.Parse("[1560,1570,1590]/[1760,1770,1775]").Latest();
```

#### Partially-Unspecified Dates

```csharp
var partUnspecifiedYear         = new UnspecifiedExtendedDateTime("19uu");
var partUnspecifiedYearAndMonth = new UnspecifiedExtendedDateTime("19uu", "u2");
var partUnspecifiedDate         = new UnspecifiedExtendedDateTime("19uu", "u2", "1u");
```

#### Parsing

```csharp
var datetime      = ExtendedDateTime.Parse("2015-04");
var interval      = ExtendedDateTimeInterval.Parse("2015-01/2015-04");
var collection    = ExtendedDateTimeCollection.Parse("{2015-01..2015-02,2015-04}");
var possibilities = ExtendedDateTimePossibilityCollection.Parse("[2015-01..2015-02,2015-04]");
var unspecified   = UnspecifiedExtendedDateTime.Parse("20uu-uu");
var anyOfTheAbove = ExtendedDateTimeFormatParser.Parse("2015-01/2015-04")
```

#### Serializing to String

```csharp
var datetime      = new ExtendedDateTime(2015).ToString();
var interval      = new ExtendedDateTimeInterval(new ExtendedDateTime(2013), new ExtendedDateTime(2015)).ToString();
var collection    = new ExtendedDateTimeCollection { new ExtendedDateTime(2013), new ExtendedDateTime(2015) }.ToString();
var possibilities = new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(2013), new ExtendedDateTime(2015) }.ToString();
var unspecified   = new UnspecifiedExtendedDateTime("20uu", "uu").ToString();
```

#### Serializing to XML

```csharp
private string ToXml(IExtendedDateTimeIndependentType extendedDateTimeIndependentType)
{
    using (var stringWriter = new StringWriter())
    {
        var xmlSerializer = new XmlSerializer(extendedDateTimeIndependentType.GetType());
        xmlSerializer.Serialize(stringWriter, extendedDateTimeIndependentType);

        return stringWriter.ToString();
    }
}
```

## Contributing

To get an overview of the basic structure of this library, see the "Primary Classes" diagram in the "Diagrams" folder. To see which features are in line to be implemented, see "Roadmap.txt" in the "Roadmap" folder. If you decide you would like to contribute, you can contact me at the email address on my profile page.

## License

ExtendedDateTimeFormat is released under the [MIT License](http://www.opensource.org/licenses/MIT).

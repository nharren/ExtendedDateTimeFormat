## EDTF Examples

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
var utc   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25);
var est   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, -5);
var nst   = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, -3, -30);
``` 

#### Approximation and Uncertainty

```csharp
var uncertainMonth = new ExtendedDateTime(2015, 4);
uncertainMonth.MonthFlags = MonthFlags.Uncertain;

var approximateMonth = new ExtendedDateTime(2015, 4);
approximateMonth.MonthFlags = MonthFlags.Approximate;

var uncertainApproxMonth = new ExtendedDateTime(2015, 4);
uncertainApproxMonth.MonthFlags = MonthFlags.Uncertain | MonthFlags.Approximate;
```

#### Seasons

```csharp
var season            = ExtendedDateTime.FromSeason(2015, Season.Spring);
var qualifiedSeason   = ExtendedDateTime.FromSeason(2105, Season.Spring, "NorthernHemisphere");
var approximateSeason = ExtendedDateTime.FromSeason(2105, Season.Spring);
approximateSeason.SeasonFlags = SeasonFlags.Approximate;
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

#### Serialization to String

```csharp
var datetime      = new ExtendedDateTime(2015).ToString();
var interval      = new ExtendedDateTimeInterval(new ExtendedDateTime(2013), new ExtendedDateTime(2015)).ToString();
var collection    = new ExtendedDateTimeCollection { new ExtendedDateTime(2013), new ExtendedDateTime(2015) }.ToString();
var possibilities = new ExtendedDateTimePossibilityCollection { new ExtendedDateTime(2013), new ExtendedDateTime(2015) }.ToString();
var unspecified   = new UnspecifiedExtendedDateTime("20uu", "uu").ToString();
```

#### Serialization to XML

```csharp
private string ToXml(IExtendedDateTimeIndependentType extendedDateTimeIndependentType)
{
    using (var stringWriter = new StringWriter())
    {
        var xmlSerializationr = new XmlSerializationr(extendedDateTimeIndependentType.GetType());
        xmlSerializationr.Serialization(stringWriter, extendedDateTimeIndependentType);

        return stringWriter.ToString();
    }
}
```
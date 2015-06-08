## Introduction

The ExtendedDateTimeFormat library is an implementation of the ISO 8601 standard and the Extended Date/Time Format (EDTF) extensions. Because EDTF is a proposed extension to the ISO 8601 standard, the features of EDTF are subject to change, and consequently, so are the features of this library. This library will likely undergo a series of breaking transformations; however, there will be stable releases along the way.

#### ISO 8601

From [Date and time format - ISO 8601](http://www.iso.org/iso/home/standards/iso8601.htm):

>##### What is ISO 8601?
>
>ISO 8601 describes an internationally accepted way to represent dates and times using numbers.
>
>When dates are represented with numbers they can be interpreted in different ways. For example, 01/05/12 could mean January 5, 2012, or May 1, 2012. On an individual level this uncertainty can be very frustrating, in a business context it can be very expensive. Organizing meetings and deliveries, writing contracts and buying airplane tickets can be very difficult when the date is unclear.
>
>ISO 8601 tackles this uncertainty by setting out an internationally agreed way to represent dates:
>
>YYYY-MM-DD
>
>For example, September 27, 2012 is represented as 2012-09-27.
>
>##### What can ISO 8601 do for me?
>
>ISO 8601 can be used by anyone who wants to use a standardized way of presenting dates and times. It helps cut out the uncertainty and confusion when communicating internationally.
>
>The full standard covers ways to write:
>
>- Date
>- Time of day
>- Coordinated universal time (UTC)
>- Local time with offset to UTC
>- Date and time
>- Time intervals
>- Recurring time intervals

[The ISO 8601 Standard](http://www.iso.org/iso/home/store/catalogue_tc/catalogue_detail.htm?csnumber=40874)

#### EDTF

The Extended Date Time Format is a proposed extension to the standard datetime format (ISO 8601). It includes support for:

- Uncertain or approximate dates (e.g. "Around 1995" or "1995, though it can't be confirmed.'")
- Sets of possible dates (e.g. "1992, 1994, *or* 1996")
- Sets of dates (e.g, "1992, 1994, *and* 1996")
- Intervals (e.g. "From 1992 to 1999");
- Partially unspecified dates. (e.g. "Only the first two digits of the year have been supplied so far.");
- Masked precision (e.g. "Some date in the 1950s.")
- Seasons (e.g. "Spring of 1992")

[The Extended Date/Time Format Standard](http://www.loc.gov/standards/datetime/pre-submission.html)

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

## Roadmap

#### Version 1

##### ISO-8601:2004(E)

- [x] Calendar dates 
	- [x] Deserialization from string
	- [x] Serialization to string
	- [ ] Conversion to OrdinalDates
	- [ ] Conversion to WeekDates
- [x] Ordinal dates 
	- [x] Deserialization from string
	- [x] Serialization to string
	- [ ] Conversion to CalendarDate
	- [ ] Conversion to WeekDate
- [x] Week dates
	- [x] Deserialization from string
	- [x] Serialization to string
	- [ ] Conversion to CalendarDate
	- [ ] Conversion to OrdinalDate
- [x] Time
	- [x] Deserialization from string
	- [x] Serialization to string
- [x] Calendar datetimes
	- [x] Deserialization from string
	- [x] Serialization to string
	- [ ] Conversion to OrdinalDateTime
	- [ ] Conversion to WeekDateTime
- [x] Ordinal datetimes
	- [x] Deserialization from string
	- [x] Serialization to string
	- [x] Conversion to CalendarDateTime
	- [x] Conversion to WeekDateTime
- [x] Week datetimes
	- [x] Deserialization from string
	- [x] Serialization to string
	- [x] Conversion to CalendarDateTime
	- [x] Conversion to OrdinalDateTime
- [x] Designated durations
	- [x] Deserialization from string
	- [x] Serialization to string
- [x] Designated week durations
	- [x] Deserialization from string
	- [x] Serialization to string
- [x] Calendar date durations
	- [x] Deserialization from string
	- [x] Serialization to string
	- [x] Conversion to OrdinalDateDuration
- [x] Ordinal date durations
	- [x] Deserialization from string
	- [x] Serialization to string
	- [x] Conversion to CalendarDateDuration
- [x] Time durations
	- [x] Deserialization from string
	- [x] Serialization to string
- [x] Calendar datetime durations
	- [x] Deserialization from string
	- [x] Serialization to string
- [x] Ordinal datetime durations
	- [x] Deserialization from string
	- [x] Serialization to string
- [ ] Start-end time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Duration-context time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Start-duration time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Duration-end time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Recurring start-end time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Recurring duration-context time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Recurring start-duration time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string
- [ ] Recurring duration-end time intervals
	- [ ] Deserialization from string
	- [ ] Serialization to string

#### Future Ideas

- Robust diagnostics
- Optimistic parsing.
- Descriptive, helpful exceptions.
- Thorough performance tests.
- Better documentation and examples.
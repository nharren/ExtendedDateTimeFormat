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

## Progress

#### Version 1.0
- [ ] Add ISO-8601:2004(E) features.
	- [x] Calendar dates in basic format (YY[YY][MM][DD]).
	- [x] Calendar dates in extended format (YYYY-MM-DD).
	- [x] Expanded representations of calendar dates in basic format (±YYYYMMDD, ±YYYY-MM, ±YYYY, ±YY).
	- [x] Expanded representations of calendar dates in extended format (±YYYY-MM-DD).
	- [x] Ordinal dates in basic format (YYYYDDD).
	- [x] Ordinal dates in extended format (YYYY-DDD).
	- [x] Expanded representations of ordinal dates in basic format (±YYYYDDD).
	- [x] Expanded representations of ordinal dates in extended format (±YYYY-DDD).
	- [x] Week dates in basic format (YYYYWww[D]).
	- [x] Week dates in extended format (YYYY-Www[-D]).
	- [x] Expanded representations of week dates in basic format (±YYYYWwwD, ±YYYYWww).
	- [x] Expanded representations of week dates in extended format (±YYYY-Www-D, ±YYYY-Www).
	- [x] Local time in basic format ([T]hh[mm][ss]).
	- [x] Local time in extended format ([T]hh:mm[:ss]).
	- [x] Local time with decimal fraction in basic format (hhmmss,ss, hhmmss.ss, hhmm,mm, hhmm.mm, hh,hh, hh.hh).
	- [x] Local time with decimal fraction in extended format (hh:mm:ss,ss, hh:mm:ss.ss, hh:mm,mm, hh:mm.mm).
	- [x] Midnight in basic format (000000, 240000).
	- [x] Midnight in extended format (00:00:00, 24:00:00).
	- [x] UTC in basic format (hhmmssZ, hhmmZ, hhZ).
	- [x] UTC in extended format (hh:mm:ssZ, hh:mmZ).
	- [x] Local time and UTC offset in basic format (±hhmm, ±hh).
	- [x] Local time and UTC offset in extended format (±hh:mm).
	- [x] Calender dates and time in basic format (YYYYMMDDThh[mm][ss?][Z][hh][mm]).
	- [x] Calender dates and time in extended format (YYYY-MM-DDThh[:mm][:ss][Z][hh][:mm]).
	- [x] Ordinal dates and time in basic format (YYYYDDDThh[mm][ss?][Z][hh][mm]).
	- [x] Ordinal dates and time in extended format (YYYY-DDDThh[:mm][:ss][Z][hh][:mm]).
	- [x] Week dates and time in basic format (YYYYWwwDThh[mm][ss?][Z][hh][mm]).
	- [x] Week dates and time in extended format (YYYY-Www-DThh[:mm][:ss][Z][hh][:mm]).
	- [ ] Duration in alternative basic format (PYYYYMMDDThhmmss, PYYYYDDDThhmmss).
	- [ ] Duration in alternative extended format (PYYYY-MM-DDThh:mm:ss, PYYYY-DDDThh:mm:ss).
	- [ ] Time intervals by start and end in basic format (YYYYMMDDThhmmss/YYYYMMDDThhmmss).
	- [ ] Time intervals by start and end in extended format (YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss)
	- [ ] Time intervals by duration and context (PnnY[nnM][nnD][TnnH][nnM][nnS], PnnW).
	- [ ] Time intervals by start and duration in basic format (YYYYMMDDThhmmss/PnnY[nnM][nnDT][nnH][nnM][nnS], YYYYMMDDThhmmss/PYYYYMMDDThhmmss).
	- [ ] Tim intervals by start and duration in extended format (YYYY-MM-DDThh:mm:ss/PnnY[nnM][nnD][TnnH][nnM][nnS], YYYY-MM-DDThh:mm:ss/PYYYY-MM-DDThh:mm:ss).
	- [ ] Time intervals by duration and end in basic format (PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYYMMDDThhmmss, PYYYYMMDDThhmmss/YYYYMMDDThhmmss) and extended format (PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYY-MM-DDThh:mm:ss, PYYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss).
	- [ ] Other time interval combinations allowed by sections 4.4.4.5 and 4.4.5 of the standard.
	- [ ] Recurring time intervals by start and end in basic format (R[n]/YYYYMMDDThhmmss/YYYYMMDDThhmmss).
	- [ ] Recurring time intervals by start and end in extended format (R[n]/YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss).
	- [ ] Recurring time intervals by duration and context in basic format (R[n]/PnnY[nnM][nnD][TnnH][nnM][nnS]).
	- [ ] Recurring time intervals by start and duration in basic format (R[n]/YYYYMMDDThhmmss/PnnY[nnM][nnD][TnnH][nnM][nnS]).
	- [ ] Recurring time intervals by start and duration in extended format (R[n]/YYYY-MM-DDThh:mm:ss/PnnY[nnM][nnD][TnnH][nnM][nnS]).
	- [ ] Recurring time intervals by duration and end in basic format (R[n]PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYYMMDDThhmmss).
	- [ ] Recurring time intervals by start and duration in basic format (R[n]/PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYY-MM-DDThh:mm:ss).
- [ ] Add ExtendedDate.
- [ ] Modify ExtendedDateTime to utilize ExtendedDate and Time.
- [ ] Add ISO-8601:2004(E) tests.
	- [ ] Conversion tests.
	- [ ] Serialization tests.
	- [ ] Parsing tests.
	- [ ] Calculation tests. 
designator).
- [ ] Add XML and binary serialization support for ISO-8601:2004(E) objects.
- [ ] Update EDTF tests.
- [ ] Update EDTF readme examples with new features.

* = 

#### General
- Optimize (e.g. string literals, redundant code, etc.).
- Improve robustness through tests.
- Performance testing.

#### Undecided
- Add support for seasons in date arithmetic. (More difficult than it seems.)
- Add support for leap seconds (Is it even calculable?).

## License

The MIT License (MIT)

Copyright (c) 2015 Nathan Harrenstein

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.

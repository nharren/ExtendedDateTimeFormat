## Introduction

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

The Extended Date Time Format (EDTF) is a proposed extension to the standard datetime format (ISO 8601). It includes support for:

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

#### Specific
- [ ] Add ISO-8601:2004(E) features.
	- [x] Representation of dates in basic format (YYYYMMDD)
	- [x] Representations of dates with reduced accuracy (YYYY-MM, YYYY, YY)
	- [x] Expanded representations of dates in basic format (±Y*YYYYMMDD, ±Y*YYYY-MM, ±Y*YYYY, ±Y*YY) and extended format (±Y*YYYY-MM-DD)
	- [x] Representations of ordinal dates in basic format (YYYYDDD) and extended format (YYYY-DDD).
    - [x] Expanded representations of ordinal dates in basic format (±Y*YYYYDDD) and extended format (±Y*YYYY-DDD).	 - [x] Representations of week dates in basic format (YYYYWwwD) and extended format (YYYY-Www-D).
    - [x] Representations of week dates with reduced accuracy in basic format (YYYYWww) and extended format (YYYY-Www).
	- [x] Expanded representations of week dates in basic format (±Y*YYYYWwwD, ±Y*YYYYWww) and extended format (±Y*YYYY-Www-D, ±Y*YYYY-Www).
	- [x] Representations of local time in basic format (hhmmss) and extended format (hh:mm:ss).
	- [x] Representations of local time with reduced accuracy in basic format (hhmm, hh) and extended format (hh:mm).
	- [x] Representations of local time with decimal fraction in basic format (hhmmss,sss*, hhmmss.sss*, hhmm,mmm*, hhmm.mmm*, hh,hhh*, hh.hhh*).
	- [x] Representations of local time with time designator ("T" in front of any of the local time representations above).
	- [x] Representations of midnight in basic format (240000) or extended format (24:00:00) and in all local time representations.
	- [x] Representations of calender dates and time in basic format (year – month – day of the month – time designator – hour – minute – second – zone designator).
	- [x] Representations of ordinal dates and time in basic and extended formats (year – day of the year – time designator – hour – minute – second – zone designator).
	- [x] Representations of week dates and time in basic and extended formats (year – week designator – week – day of the week – time designator – hour – minute – second – zone).
	- [x] All other date and time representations allowed (See section 4.3.3 of the standard).
	- [ ] Representations of time intervals by duration (Pnnn*Ynnn*Mnnn*DTnnn*Hnnn*Mnnn*S, Pnnn*W).
	- [ ] Reduced accuracy of representations of time intervals by duration where the lowest order components are omitted to represent reduced accuracy. Lowest order components may have a decimal fraction ("," is preferred, otherwise "."). "T" is absent if all components are absent.
	- [ ] Alternative format for the representation of time intervals by duration in basic format (PYYYYMMDDThhmmss, PYYYYDDDThhmmss) or extended format (PYYYY-MM-DDThh:mm:ss, PYYYY-DDDThh:mm:ss).
	- [ ] Representations of time intervals by start and duration in basic format (YYYYMMDDThhmmss/Pnnn*Ynnn*Mnnn*DTnnn*Hnnn*Mnnn*S, YYYYMMDDThhmmss/PYYYYMMDDThhmmss) and extended format (YYYY-MM-DDThh:mm:ss/Pnnn*Ynnn*Mnnn*DTnnn*Hnnn*Mnnn*S, YYYY-MM-DDThh:mm:ss/PYYYY-MM-DDThh:mm:ss).
	- [ ] Representations of time intervals by duration and end in basic format (Pnnn*Ynnn*Mnnn*DTnnn*Hnnn*Mnnn*S/YYYYMMDDThhmmss, PYYYYMMDDThhmmss/YYYYMMDDThhmmss) and extended format (Pnnn*Ynnn*Mnnn*DTnnn*Hnnn*Mnnn*S/YYYY-MM-DDThh:mm:ss, PYYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss).
	- [ ] All other time interval representations allowed (See sections 4.4.4.5 and 4.4.5 of the standard).
	- [ ] Representations of recurring time intervals in basic format (e.g. Rnn*/YYYYMMDDThhmmss/YYYYMMDDThhmmss) or extended format (Rnn*/YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss).
	- [ ] Representation other than complete of recurring time intervals (See section 4.5.4 of the standard).
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
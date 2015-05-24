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

## Roadmap

#### Version 1α

##### ISO-8601:2004(E)

- [x] Calendar dates 
	- [x] Conversion from string
		- [x] Basic format (YY|YYYY-MM|YYYY[MMDD])
		- [x] Expanded basic format (+**Y**YY|+**Y**YYYY-MM|+**Y**YYYY[MMDD]|-**Y**Y|-**Y**YYY-MM|-**Y**YYY[MMDD])
		- [x] Extended format (YYYY-MM-DD)
		- [x] Expanded extended format (+**Y**YYYY-MM-DD|-**Y**YYY-MM-DD)
	- [x] Conversion to string
	- [x] Conversion to ordinal dates
	- [x] Conversion to week dates
- [x] Ordinal dates 
	- [x] Conversion from string
		- [x] Basic format (YYYYDDD)
		- [x] Expanded basic format (+**Y**YYYYDDD|-**Y**YYYDDD)
		- [x] Extended format (YYYY-DDD)
		- [x] Expanded extended format (+**Y**YYYY-DDD|-**Y**YYY-DDD)
	- [x] Conversion to string
	- [x] Conversion to calendar dates
	- [x] Conversion to week dates
- [x] Week dates
	- [x] Conversion from string
		- [x] Basic format (YYYYWww[D])
		- [x] Expanded basic format (+**Y**YYYYWww[D]|-**Y**YYYWww[D])
		- [x] Extended format (YYYY-Www[-D])
		- [x] Expanded extended format (+**Y**YYYY-Www[-D], -**Y**YYY-Www[-D])
	- [x] Conversion to string
	- [x] Conversion to calendar dates
	- [x] Conversion to ordinal dates
- [x] Time
	- [x] Conversion from string
		- [x] Basic format ([T]hh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]])
		- [x] Extended format ([T]hh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]])
		- [x] Midnight in basic format ([T]00[.**0**|,**0**|00[.**0**|,**0**|00[.**0**|,**0**]]]|[T]24[.**0**|,**0**|00[.**0**|,**0**|00[.**0**|,**0**]]])
		- [x] Midnight in extended format ([T]00[.**0**|,**0**|:00[.**0**|,**0**|:00[.**0**|,**0**]]]|[T]24[.**0**|,**0**|:00[.**0**|,**0**|:00[.**0**|,**0**]]])
	- [x] Conversion to string
- [x] Offset time
	- [x] Conversion from string
		- [x] Basic format ([T]hh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]]Z|[T]hh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]]±hh[mm])
		- [x] Extended format ([T]hh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]]Z|[T]hh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]]±hh:mm)
	- [x] Conversion to string
- [x] Calender datetimes
	- [x] Conversion from string
		- [x] Basic format (YYYYMMDDThh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]][±hh[mm]])
		- [x] Extended format (YYYY-MM-DDThh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]][±hh[:mm]])
	- [x] Conversion to string
- [x] Ordinal datetimes
	- [x] Conversion from string
		- [x] Basic format (YYYYDDDThh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]][±hh[mm]])
		- [x] Extended format (YYYY-DDDThh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]][±hh[:mm]])
	- [x] Conversion to string
- [x] Week datetimes
	- [x] Conversion from string
		- [x] Basic format (YYYYWwwDThh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]][±hh[mm]])
		- [x] Extended format (YYYY-Www-DThh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]][±hh[:mm]])
	- [x] Conversion to string
- [ ] Designated durations
	- [ ] Conversion from string
		- [ ] Basic and extended formats (PnnY[nnM[nnD[TnnH[nnM[nnS]]]]]|PnnM[nnD[TnnH[nnM[nnS]]]]|PnnD[TnnH[nnM[nnS]]]|PTnnH[nnM[nnS]]|PTnnM[nnS]|PTnnS|PnnW)
	- [ ] Conversion to string
- [x] Calendar date durations
	- [x] Conversion from string
		- [x] Basic format (PYYYY[MM[DD]])
		- [x] Extended format (PYYYY[-MM[-DD]])
	- [x] Conversion to string
- [x] Ordinal date durations
	- [x] Conversion from string
		- [x] Basic format (PYYYYDDD)
		- [x] Extended format (PYYYY-DDD)
	- [x] Conversion to string
- [x] Time durations
	- [x] Conversion from string
		- [x] Basic format (P[T]hh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]])
		- [x] Extended format (P[T]hh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]])
	- [x] Conversion to string
- [x] Calendar datetime durations
	- [x] Conversion from string
		- [x] Basic format (PYYYYMMDDThh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]])
		- [x] Extended format (PYYYY-MM-DDThh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]])
	- [x] Conversion to string
- [x] Ordinal datetime durations
	- [x] Conversion from string
		- [x] Basic format (PYYYYDDDThh[,**h**|.**h**|mm[,**m**|.**m**|ss[,**s**|.**s**]]])
		- [x] Extended format (PYYYY-DDDThh[,**h**|.**h**|:mm[,**m**|.**m**|:ss[,**s**|.**s**]]])
	- [x] Conversion to string
- [ ] Start-end time intervals
	- [ ] Conversion from string
		- [ ] Basic format (YYYYMMDDThhmmss/YYYYMMDDThhmmss)
		- [ ] Extended format (YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss)
	- [ ] Conversion to string
- [ ] Duration-context time intervals
	- [ ] Conversion from string
		- [ ] Basic and extended formats (PnnY[nnM[nnD[TnnH[nnM[nnS]]]]]|PnnM[nnD[TnnH[nnM[nnS]]]]|PnnD[TnnH[nnM[nnS]]]|PTnnH[nnM[nnS]]|PTnnM[nnS]|PTnnS|PnnW)
	- [ ] Conversion to string
- [ ] Start-duration time intervals
	- [ ] Conversion from string
		- [ ] Basic format (YYYYMMDDThhmmss/PnnY[nnM][nnDT][nnH][nnM][nnS], YYYYMMDDThhmmss/PYYYYMMDDThhmmss)
		- [ ] Extended format (YYYY-MM-DDThh:mm:ss/PnnY[nnM][nnD][TnnH][nnM][nnS], YYY-MM-DDThh:mm:ss/PYYYY-MM-DDThh:mm:ss)
	- [ ] Conversion to string
- [ ] Duration-end time intervals
	- [ ] Conversion from string
		- [ ] Basic format (PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYYMMDDThhmmss, PYYYYMMDDThhmmss/YYYYMMDDThhmmss)
		- [ ] Extended format (PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYY-MM-DDThh:mm:ss, PYYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss)
	- [ ] Conversion to string
- [ ] Recurring start-end time intervals
	- [ ] Conversion from string
		- [ ] Basic format (R[n]/YYYYMMDDThhmmss/YYYYMMDDThhmmss)
		- [ ] Extended format (R[n]/YYYY-MM-DDThh:mm:ss/YYYY-MM-DDThh:mm:ss)
	- [ ] Conversion to string
- [ ] Recurring duration-context time intervals
	- [ ] Conversion from string
		- [ ] Basic format (R[n]/PnnY[nnM][nnD][TnnH][nnM][nnS])
	- [ ] Conversion to string
- [ ] Recurring start-duration time intervals
	- [ ] Conversion from string
		- [ ] Basic format (R[n]/YYYYMMDDThhmmss/PnnY[nnM][nnD][TnnH][nnM][nnS])
		- [ ] Extended format (R[n]/YYYY-MM-DDThh:mm:ss/PnnY[nnM][nnD][TnnH][nnM][nnS])
	- [ ] Conversion to string
- [ ] Recurring duration-end time intervals
	- [ ] Conversion from string
		- [ ] Basic format (R[n]PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYYMMDDThhmmss)
		- [ ] Extended format (R[n]/PnnY[nnM][nnD][TnnH][nnM][nnS]/YYYY-MM-DDThh:mm:ss)
	- [ ] Conversion to string

###### Notes:

1. Bold means one or many values. 
2. "|" symbolizes the logical "or".
3. Contents between "[" and "]" are optional. 

##### EDTF

#### Version 1β

##### ISO-8601:2004(E) Unit Tests

##### EDTF Unit Tests

#### Version 1 

##### ISO-8601:2004(E) Usability Tests

##### ISO-8601:2004(E) Performance Tests

##### EDTF Usability Tests

##### EDTF Performance Tests

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

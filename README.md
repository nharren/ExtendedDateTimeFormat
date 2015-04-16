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
var hour   = new ExtendedDateTime(2015, 4, 16, 3, TimeZoneInfo.Local.BaseUtcOffset);
var minute = new ExtendedDateTime(2015, 4, 16, 3, 26, TimeZoneInfo.Local.BaseUtcOffset);
var second = new ExtendedDateTime(2015, 4, 16, 3, 26, 25, TimeZoneInfo.Local.BaseUtcOffset);
``` 

#### Approximation and Uncertainty

```csharp
var uncertainMonth = new ExtendedDateTime(2015, 4, monthFlags: ExtendedDateTimeFlags.Uncertain);
var approximateMonth = new ExtendedDateTime(2015, 4, monthFlags: ExtendedDateTimeFlags.Approximate);
var uncertainAndApproximateMonth = new ExtendedDateTime(2015, 4, monthFlags: ExtendedDateTimeFlags.Uncertain | ExtendedDateTimeFlags.Approximate);
```

#### Parsing

```csharp
var datetime = ExtendedDateTime.Parse("2015-04");
var interval = ExtendedDateTimeInterval.Parse("2015-01/2015-04");
var collection = ExtendedDateTimeCollection.Parse("{2015-01..2015-02,2015-04}");
var possibilityCollection = ExtendedDateTimePossibilityCollection.Parse("[2015-01..2015-02,2015-04]");
var unspecifiedDatetime = UnspecifiedExtendedDateTime.Parse("20uu-uu");
var anyOfTheAbove = ExtendedDateTimeFormatParser.Parse("2015-01/2015-04")
```

#### More examples coming soon...

## Contributing

To get an overview of the basic structure of this library, see the "Primary Classes" diagram in the "Diagrams" folder.

A list of things you can do to help improve this library can be found in "Roadmap.txt" in the "Roadmap" folder.

Alternatively, if you see something you would like to fix or a feature you would like to implement, feel free to do so.

## License

ExtendedDateTimeFormat is released under the [MIT License](http://www.opensource.org/licenses/MIT).

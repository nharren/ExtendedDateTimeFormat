## Introduction

The [Extended Date Time Format (EDTF)](http://www.loc.gov/standards/datetime/pre-submission.html) is a proposed extension to the standard datetime format (ISO 8601). It includes support for:

- Uncertain or approximate dates (e.g. "Around 1995" or "1995, though it can't be confirmed.'")
- Sets of possible dates (e.g. "1992, 1994, *or* 1996")
- Sets of dates (e.g, "1992, 1994, *and* 1996")
- Intervals (e.g. "From 1992 to 1999");
- Partially unspecified dates. (e.g. "Only the first two digits of the year have been supplied so far.");
- Masked precision (e.g. "Some date in the 1950s.")
- Seasons (e.g. "Spring of 1992")

This library is intended to bring this proposed standard and all its features to .NET.

## Getting Started

#### Parsing

To parse an Extended Date Time Format string into the appropriate type, use the methods in the static class "ExtendedDateTimeFormatParser".

If you will not know beforehand what type of object the string represents, then use the generic "Parse" method. This method will return a "IExtendedDateTimeIndependentType" interface which will be one of the following types:

- ExtendedDateTimeInterval: Two dates separated by '/'.
- ExtendedDateTimeCollection: Multiple dates between '{' and '}'
- ExtendedDateTimePossibilityCollection: A collection of possible dates between '[' and ']'.
- ExtendedDateTime: A datetime.
- PartialExtendedDateTime: A datetime utilizing masked precision ('x') or unspecification ('u').

#### Serializing

To serialize an ExtendedDateTimeFormat structure into a string, simply call the "ToString" method.

#### Validating

To validate a string, wrap the appropriate parse method from ExtendedDateTimeFormatParser in a Try-Catch statement, and catch the ParseException. The error messages from this exception can then be displayed to the user. See the "Validator" sample inside to see specifically how to do this.

## Contributing

To get an overview of the basic structure of this library, see the "Primary Classes" diagram in the "Diagrams" folder.

A list of things you can do to help improve this library can be found in "Roadmap.txt" in the "Roadmap" folder.

Alternatively, if you see something you would like to fix or a feature you would like to implement, feel free to do so (no permission is required).

## License

ExtendedDateTimeFormat is released under the [MIT License](http://www.opensource.org/licenses/MIT).

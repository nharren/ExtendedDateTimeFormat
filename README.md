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

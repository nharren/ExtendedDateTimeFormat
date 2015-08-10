## Introduction

The ExtendedDateTimeFormat library is an implementation the Extended Date/Time Format (EDTF) extensions. Because EDTF is a proposed extension to the ISO 8601 standard, the features of EDTF are subject to change, and consequently, so are the features of this library. This library will likely undergo a series of breaking transformations; however, there will be stable releases along the way.

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

## More Information

[Examples](doc/EDTF Examples.md)
[Features](doc/Features.md)
[EBNF Description](doc/EDTF.ebnf)
[The MIT License](LICENSE.txt)

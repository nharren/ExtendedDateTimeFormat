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

[EDTF Examples](doc/EDTF Examples.md)

## Features

See [Features.md](doc/Features.md)

#### License

[The MIT License](LICENSE.txt)

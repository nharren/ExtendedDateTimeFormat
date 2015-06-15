## ISO 8601 Examples

#### Calendar Dates

```csharp
var year  = new CalendarDate(2015);
var month = new CalendarDate(2015, 4);
var day   = new CalendarDate(2015, 4, 16);
```

#### Ordinal Dates

```csharp
var year = new OrdinalDate(2015);
var day  = new OrdinalDate(2015, 150);
```

#### Week Dates

```csharp
var year = new WeekDate(2015);
var week = new WeekDate(2015, 12);
var day  = new WeekDate(2015, 12, 5);
```

#### Local Times

```csharp
var hour   = new Time(3);
var minute = new Time(3, 26);
var second = new Time(3, 26, 25);
```

#### Local Times with UTC Offsets

```csharp
var utc = new Time(3, 26, 25) { UtcOffset = new UtcOffset(0, 0) };
var est = new Time(3, 26, 25) { UtcOffset = new UtcOffset(-5, 0) };
var nst = new Time(3, 26, 25) { UtcOffset = new UtcOffset(-3, 30) };
``` 

#### Calendar Date/Times

```csharp
var hour   = new CalendarDateTime(new CalendarDate(2015, 4, 16), new Time(3));
var minute = new CalendarDateTime(new CalendarDate(2015, 4, 16), new Time(3, 26));
var second = new CalendarDateTime(new CalendarDate(2015, 4, 16), new Time(3, 26, 25));
```

#### Ordinal Date/Times

```csharp
var hour   = new OrdinalDateTime(new OrdinalDate(2015, 100), new Time(3));
var minute = new OrdinalDateTime(new OrdinalDate(2015, 100), new Time(3, 26));
var second = new OrdinalDateTime(new OrdinalDate(2015, 100), new Time(3, 26, 25));
```

#### Week Date/Times

```csharp
var hour   = new WeekDateTime(new WeekDate(2015, 12, 5), new Time(3));
var minute = new WeekDateTime(new WeekDate(2015, 12, 5), new Time(3, 26));
var second = new WeekDateTime(new WeekDate(2015, 12, 5), new Time(3, 26, 25));
```

#### Time Intervals

```csharp
var startEnd = new StartEndTimeInterval(
	new CalendarDateTime(new CalendarDate(2012, 2, 12), new Time(0, 0, 0)),
	new CalendarDateTime(new CalendarDate(2015, 4, 16), new Time(3, 26, 25)));

var durationContext = new DurationContextTimeInterval(
	new OrdinalDateDuration(112, 2)));

var startDuration = new StartDurationTimeInterval(
	new CalendarDateTime(new CalendarDate(2012, 2, 12), new Time(0, 0, 0)),
	new CalendarDateTimeDuration(new CalendarDateDuration(54, 2, 2), new TimeDuration(1, 2, 2)));

var durationEnd = new StartDurationTimeInterval(
	new CalendarDateTimeDuration(new CalendarDateDuration(54, 2, 2), new TimeDuration(1, 2, 2)),
	new CalendarDateTime(new CalendarDate(2012, 2, 12), new Time(0, 0, 0)));

var recurring = new RecurringTimeInterval(
	new StartEndTimeInterval(
		new CalendarDateTime(new CalendarDate(2012, 2, 12), new Time(0, 0, 0)),
		new CalendarDateTime(new CalendarDate(2015, 4, 16), new Time(3, 26, 25))), 
	10);
```
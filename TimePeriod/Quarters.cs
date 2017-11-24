// -- FILE ------------------------------------------------------------------
// name       : Quarters.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Linq;

// Modified to replace for-loops, with Enumerable-foreach loops; part of issue #17 debugging.

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    public sealed class Quarters : QuarterTimeRange {

        // ----------------------------------------------------------------------
        public Quarters (DateTime moment, YearQuarter startYearQuarter, int count):
            this (moment, startYearQuarter, count, new TimeCalendar ()) { } // Quarters

        // ----------------------------------------------------------------------
        public Quarters (DateTime moment, YearQuarter startYearQuarter, int count, ITimeCalendar calendar):
            this (TimeTool.GetYearOf (calendar.YearBaseMonth, calendar.GetYear (moment), calendar.GetMonth (moment)),
                startYearQuarter, count, calendar) { } // Quarters

        // ----------------------------------------------------------------------
        public Quarters (int startYear, YearQuarter startYearQuarter, int quarterCount):
            this (startYear, startYearQuarter, quarterCount, new TimeCalendar ()) { } // Quarters

        // ----------------------------------------------------------------------
        public Quarters (int startYear, YearQuarter startYearQuarter, int quarterCount, ITimeCalendar calendar):
            base (startYear, startYearQuarter, quarterCount, calendar) { } // Quarters

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetQuarters () {
            TimePeriodCollection quarters = new TimePeriodCollection ();
            foreach (var i in Enumerable.Range (0, QuarterCount)) {
                int year;
                YearQuarter quarter;
                TimeTool.AddQuarter (BaseYear, StartQuarter, i, out year, out quarter);
                quarters.Add (new Quarter (year, quarter, Calendar));
            }
            return quarters;
        } // GetQuarters

        // ----------------------------------------------------------------------
        protected override string Format (ITimeFormatter formatter) {
            return formatter.GetCalendarPeriod (StartQuarterOfYearName, EndQuarterOfYearName,
                formatter.GetShortDate (Start), formatter.GetShortDate (End), Duration);
        } // Format

    } // class Quarters

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
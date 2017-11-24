// -- FILE ------------------------------------------------------------------
// name       : BroadcastYear.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2013.09.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Globalization;
using System.Linq;

// Modified to replace for-loops, with Enumerable-foreach loops; part of issue #17 debugging.

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    public sealed class BroadcastYear : CalendarTimeRange {

        // ----------------------------------------------------------------------
        public BroadcastYear ():
            this (new TimeCalendar ()) { } // BroadcastYear

        // ----------------------------------------------------------------------
        public BroadcastYear (ITimeCalendar calendar):
            this (ClockProxy.Clock.Now, calendar) { } // BroadcastYear

        // ----------------------------------------------------------------------
        public BroadcastYear (DateTime moment):
            this (moment, new TimeCalendar ()) { } // BroadcastYear

        // ----------------------------------------------------------------------
        public BroadcastYear (DateTime moment, ITimeCalendar calendar):
            this (GetYearOf (moment), calendar) { } // BroadcastYear

        // ----------------------------------------------------------------------
        public BroadcastYear (int year):
            this (year, new TimeCalendar ()) { } // BroadcastYear

        // ----------------------------------------------------------------------
        public BroadcastYear (int year, ITimeCalendar calendar):
            base (GetPeriodOf (year), calendar) {
                this.year = year;
            } // BroadcastYear

        // ----------------------------------------------------------------------
        public int Year {
            get { return year; }
        } // Year

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetWeeks () {
            TimePeriodCollection weeks = new TimePeriodCollection ();
            foreach(var week in Enumerable.Range(1,BroadcastCalendarTool.GetWeeksOfYear(year))){
                weeks.Add (new BroadcastWeek (year, week));
            }
            return weeks;
        } // GetWeeks

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetMonths () {
            TimePeriodCollection months = new TimePeriodCollection ();
            foreach (var month in Enumerable.Range(1,TimeSpec.MonthsPerYear)) {
                months.Add (new BroadcastMonth (year, (YearMonth) month));
            }
            return months;
        } // GetMonths

        // ----------------------------------------------------------------------
        public BroadcastYear GetPreviousYear () {
            return AddYears (-1);
        } // GetPreviousYear

        // ----------------------------------------------------------------------
        public BroadcastYear GetNextYear () {
            return AddYears (1);
        } // GetNextYear

        // ----------------------------------------------------------------------
        public BroadcastYear AddYears (int count) {
            return new BroadcastYear (Year + count, Calendar);
        } // AddYears

        // ----------------------------------------------------------------------
        protected override string Format (ITimeFormatter formatter) {
            return formatter.GetCalendarPeriod (Year.ToString (CultureInfo.InvariantCulture),
                formatter.GetShortDate (Start), formatter.GetShortDate (End), Duration);
        } // Format

        // ----------------------------------------------------------------------
        private static int GetYearOf (DateTime moment) {
            int year;
            BroadcastCalendarTool.GetYearOf (moment, out year);
            return year;
        } // GetYearOf

        // ----------------------------------------------------------------------
        private static ITimeRange GetPeriodOf (int year) {
            return new CalendarTimeRange (
                BroadcastCalendarTool.GetStartOfYear (year),
                BroadcastCalendarTool.GetStartOfYear (year + 1));
        } // GetPeriodOf

        // ----------------------------------------------------------------------
        // members
        private readonly int year;

    } // class BroadcastYear

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
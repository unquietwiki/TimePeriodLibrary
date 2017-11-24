// -- FILE ------------------------------------------------------------------
// name       : YearTimeRange.cs
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
    public abstract class YearTimeRange : CalendarTimeRange {

        // ----------------------------------------------------------------------
        protected YearTimeRange (int startYear, int yearCount, ITimeCalendar calendar):
            base (GetPeriodOf (calendar, startYear, yearCount), calendar) {
                this.startYear = startYear;
                this.yearCount = yearCount;
                endYear = End.Year;
            } // YearTimeRange

        // ----------------------------------------------------------------------
        public int YearCount {
            get { return yearCount; }
        } // YearCount

        // ----------------------------------------------------------------------
        public override int BaseYear {
            get { return startYear; }
        } // BaseYear

        // ----------------------------------------------------------------------
        public int StartYear {
            get { return Calendar.GetYear (startYear, (int) YearBaseMonth); }
        } // StartYear

        // ----------------------------------------------------------------------
        public int EndYear {
            get { return Calendar.GetYear (endYear, (int) YearBaseMonth); }
        } // EndYear

        // ----------------------------------------------------------------------
        public string StartYearName {
            get { return Calendar.GetYearName (StartYear); }
        } // StartYearName

        // ----------------------------------------------------------------------
        public string EndYearName {
            get { return Calendar.GetYearName (StartYear + YearCount - 1); }
        } // EndYearName

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetHalfyears () {
            TimePeriodCollection halfyears = new TimePeriodCollection ();
            foreach (var i in Enumerable.Range (0, yearCount)) {
                foreach (var halfyear in Enumerable.Range (0, TimeSpec.HalfyearsPerYear)) {
                    int year;
                    YearHalfyear yearHalfyear;
                    TimeTool.AddHalfyear (startYear, YearHalfyear.First, (i * TimeSpec.HalfyearsPerYear) + halfyear, out year, out yearHalfyear);
                    halfyears.Add (new Halfyear (year, yearHalfyear, Calendar));
                }
            }
            return halfyears;
        } // GetHalfyears

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetQuarters () {
            TimePeriodCollection quarters = new TimePeriodCollection ();
            foreach (var i in Enumerable.Range (0, yearCount)) {
                foreach (var quarter in Enumerable.Range (0, TimeSpec.QuartersPerYear)) {
                    int year;
                    YearQuarter yearQuarter;
                    TimeTool.AddQuarter (startYear, YearQuarter.First, (i * TimeSpec.QuartersPerYear) + quarter, out year, out yearQuarter);
                    quarters.Add (new Quarter (year, yearQuarter, Calendar));
                }
            }
            return quarters;
        } // GetQuarters

        // ----------------------------------------------------------------------
        public ITimePeriodCollection GetMonths () {
            TimePeriodCollection months = new TimePeriodCollection ();
            foreach (var i in Enumerable.Range (0, yearCount)) {
                foreach (var month in Enumerable.Range (0, TimeSpec.MonthsPerYear)) {
                    int year;
                    YearMonth yearMonth;
                    TimeTool.AddMonth (startYear, YearBaseMonth, (i * TimeSpec.MonthsPerYear) + month, out year, out yearMonth);
                    months.Add (new Month (year, yearMonth, Calendar));
                }
            }
            return months;
        } // GetMonths

        // ----------------------------------------------------------------------
        protected override bool IsEqual (object obj) {
            return base.IsEqual (obj) && HasSameData (obj as YearTimeRange);
        } // IsEqual

        // ----------------------------------------------------------------------
        private bool HasSameData (YearTimeRange comp) {
            return
            startYear == comp.startYear &&
                endYear == comp.endYear &&
                yearCount == comp.yearCount;
        } // HasSameData

        // ----------------------------------------------------------------------
        protected override int ComputeHashCode () {
            return HashTool.ComputeHashCode (base.ComputeHashCode (), startYear, startYear, yearCount);
        } // ComputeHashCode

        // ----------------------------------------------------------------------
        private static DateTime GetStartOfYear (ITimeCalendar calendar, int year) {
            DateTime startOfYear;

            switch (calendar.YearType) {
                case YearType.FiscalYear:
                    startOfYear = FiscalCalendarTool.GetStartOfYear (year, calendar.YearBaseMonth,
                        calendar.FiscalFirstDayOfYear, calendar.FiscalYearAlignment);
                    break;
                default:
                    startOfYear = new DateTime (year, (int) calendar.YearBaseMonth, 1);
                    break;
            }
            return startOfYear;
        } // GetStartOfYear

        // ----------------------------------------------------------------------
        private static TimeRange GetPeriodOf (ITimeCalendar calendar, int year, int yearCount) {
            if (yearCount < 1) {
                throw new ArgumentOutOfRangeException ("yearCount");
            }

            DateTime start = GetStartOfYear (calendar, year);
            DateTime end = GetStartOfYear (calendar, year + yearCount);
            return new TimeRange (start, end);
        } // GetPeriodOf

        // ----------------------------------------------------------------------
        // members
        private readonly int startYear;
        private readonly int yearCount;
        private readonly int endYear; // cache

    } // class YearTimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
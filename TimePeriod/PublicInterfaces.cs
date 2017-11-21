// -- FILE ------------------------------------------------------------------
// name       : PublicInterfaces.cs
// project    : Itenso Time Period
// original   : Jani Giannoudis - 2011 - 2013
// refactor   : Michael Adams - 2017.11.20
// language   : C# 4.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Globalization;

#if (!PCL)
using System.ComponentModel;
#endif

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    public interface ICalendarPeriodCollectorFilter : ICalendarVisitorFilter {

        // ----------------------------------------------------------------------
        IList<MonthRange> CollectingMonths { get; }

        // ----------------------------------------------------------------------
        IList<DayRange> CollectingDays { get; }

        // ----------------------------------------------------------------------
        IList<HourRange> CollectingHours { get; }

    } // interface ICalendarPeriodCollectorFilter

    // ------------------------------------------------------------------------
    public interface ICalendarTimeRange : ITimeRange {

        // ----------------------------------------------------------------------
        ITimeCalendar Calendar { get; }

    } // interface ICalendarTimeRange

    // ------------------------------------------------------------------------
    public interface ICalendarVisitorContext { } // interface ICalendarVisitorContext

    // ------------------------------------------------------------------------
    public interface ICalendarVisitorFilter {

        // ----------------------------------------------------------------------
        ITimePeriodCollection ExcludePeriods { get; }

        // ----------------------------------------------------------------------
        IList<int> Years { get; }

        // ----------------------------------------------------------------------
        IList<YearMonth> Months { get; }

        // ----------------------------------------------------------------------
        IList<int> Days { get; }

        // ----------------------------------------------------------------------
        IList<DayOfWeek> WeekDays { get; }

        // ----------------------------------------------------------------------
        IList<int> Hours { get; }

        // ----------------------------------------------------------------------
        void AddWorkingWeekDays ();

        // ----------------------------------------------------------------------
        void AddWeekendWeekDays ();

        // ----------------------------------------------------------------------
        void Clear ();

    } // interface ICalendarVisitorFilter

    // ------------------------------------------------------------------------
    // see http://stackoverflow.com/questions/43711/whats-a-good-way-to-overwrite-datetime-now-during-testing
    public interface IClock {

        // ----------------------------------------------------------------------
        DateTime Now { get; }

    } // interface IClock

    // ------------------------------------------------------------------------
    public interface IDateTimeSet : ICollection<DateTime> {

        // ----------------------------------------------------------------------
        DateTime this [int index] { get; }

        // ----------------------------------------------------------------------
        DateTime? Min { get; }

        // ----------------------------------------------------------------------
        DateTime? Max { get; }

        // ----------------------------------------------------------------------
        TimeSpan? Duration { get; }

        // ----------------------------------------------------------------------
        bool IsEmpty { get; }

        // ----------------------------------------------------------------------
        bool IsMoment { get; }

        // ----------------------------------------------------------------------
        bool IsAnytime { get; }

        // ----------------------------------------------------------------------
        int IndexOf (DateTime moment);

        // ----------------------------------------------------------------------
        new bool Add (DateTime moment);

        // ----------------------------------------------------------------------
        void AddAll (IEnumerable<DateTime> moments);

        // ----------------------------------------------------------------------
        IList<TimeSpan> GetDurations (int startIndex, int count);

        // ----------------------------------------------------------------------
        DateTime? FindPrevious (DateTime moment);

        // ----------------------------------------------------------------------
        DateTime? FindNext (DateTime moment);

    } // interface IDateTimeSet

    // ------------------------------------------------------------------------
    public interface IDurationProvider {

        // ----------------------------------------------------------------------
        TimeSpan GetDuration (DateTime start, DateTime end);

    } // interface IDurationProvider

    // ------------------------------------------------------------------------
    public interface ITimeBlock : ITimePeriod {

        // ----------------------------------------------------------------------
        new DateTime Start { get; set; }

        // ----------------------------------------------------------------------
        new DateTime End { get; set; }

        // ----------------------------------------------------------------------
        new TimeSpan Duration { get; set; }

        // ----------------------------------------------------------------------
        void Setup (DateTime newStart, TimeSpan newDuration);

        // ----------------------------------------------------------------------
        void Move (TimeSpan delta);

        // ----------------------------------------------------------------------
        void DurationFromStart (TimeSpan newDuration);

        // ----------------------------------------------------------------------
        void DurationFromEnd (TimeSpan newDuration);

        // ----------------------------------------------------------------------
        ITimeBlock Copy (TimeSpan delta);

        // ----------------------------------------------------------------------
        ITimeBlock GetPreviousPeriod (TimeSpan offset);

        // ----------------------------------------------------------------------
        ITimeBlock GetNextPeriod (TimeSpan offset);

        // ----------------------------------------------------------------------
        ITimeBlock GetIntersection (ITimePeriod period);

    } // interface ITimeBlock

    // ------------------------------------------------------------------------
    public interface ITimeCalendar : ITimePeriodMapper {

        // ----------------------------------------------------------------------
        CultureInfo Culture { get; }

        // ----------------------------------------------------------------------
        YearType YearType { get; }

        // ----------------------------------------------------------------------
        TimeSpan StartOffset { get; }

        // ----------------------------------------------------------------------
        TimeSpan EndOffset { get; }

        // ----------------------------------------------------------------------
        YearMonth YearBaseMonth { get; }

        // ----------------------------------------------------------------------
        YearMonth FiscalYearBaseMonth { get; }

        // ----------------------------------------------------------------------
        DayOfWeek FiscalFirstDayOfYear { get; }

        // ----------------------------------------------------------------------
        FiscalYearAlignment FiscalYearAlignment { get; }

        // ----------------------------------------------------------------------
        FiscalQuarterGrouping FiscalQuarterGrouping { get; }

        // ----------------------------------------------------------------------
        DayOfWeek FirstDayOfWeek { get; }

        // ----------------------------------------------------------------------
        YearWeekType YearWeekType { get; }

        // ----------------------------------------------------------------------
        int GetYear (DateTime time);

        // ----------------------------------------------------------------------
        int GetMonth (DateTime time);

        // ----------------------------------------------------------------------
        int GetHour (DateTime time);

        // ----------------------------------------------------------------------
        int GetMinute (DateTime time);

        // ----------------------------------------------------------------------
        int GetDayOfMonth (DateTime time);

        // ----------------------------------------------------------------------
        DayOfWeek GetDayOfWeek (DateTime time);

        // ----------------------------------------------------------------------
        int GetDaysInMonth (int year, int month);

        // ----------------------------------------------------------------------
        int GetYear (int year, int month);

        // ----------------------------------------------------------------------
        string GetYearName (int year);

        // ----------------------------------------------------------------------
        string GetHalfyearName (YearHalfyear yearHalfyear);

        // ----------------------------------------------------------------------
        string GetHalfyearOfYearName (int year, YearHalfyear yearHalfyear);

        // ----------------------------------------------------------------------
        string GetQuarterName (YearQuarter yearQuarter);

        // ----------------------------------------------------------------------
        string GetQuarterOfYearName (int year, YearQuarter yearQuarter);

        // ----------------------------------------------------------------------
        string GetMonthName (int month);

        // ----------------------------------------------------------------------
        string GetMonthOfYearName (int year, int month);

        // ----------------------------------------------------------------------
        string GetWeekOfYearName (int year, int weekOfYear);

        // ----------------------------------------------------------------------
        string GetDayName (DayOfWeek dayOfWeek);

        // ----------------------------------------------------------------------
        int GetWeekOfYear (DateTime time);

        // ----------------------------------------------------------------------
        DateTime GetStartOfYearWeek (int year, int weekOfYear);

    } // interface ITimeCalendar

    // ------------------------------------------------------------------------
    public interface ITimeFormatter {

        // ----------------------------------------------------------------------
        CultureInfo Culture { get; }

        // ----------------------------------------------------------------------
        string ListSeparator { get; }

        // ----------------------------------------------------------------------
        string ContextSeparator { get; }

        // ----------------------------------------------------------------------
        string StartEndSeparator { get; }

        // ----------------------------------------------------------------------
        string DurationSeparator { get; }

        // ----------------------------------------------------------------------
        string DurationItemSeparator { get; }

        // ----------------------------------------------------------------------
        string DurationLastItemSeparator { get; }

        // ----------------------------------------------------------------------
        string DurationValueSeparator { get; }

        // ----------------------------------------------------------------------
        string IntervalStartClosed { get; }

        // ----------------------------------------------------------------------
        string IntervalStartOpen { get; }

        // ----------------------------------------------------------------------
        string IntervalStartOpenIso { get; }

        // ----------------------------------------------------------------------
        string IntervalEndClosed { get; }

        // ----------------------------------------------------------------------
        string IntervalEndOpen { get; }

        // ----------------------------------------------------------------------
        string IntervalEndOpenIso { get; }

        // ----------------------------------------------------------------------
        string DateTimeFormat { get; }

        // ----------------------------------------------------------------------
        string ShortDateFormat { get; }

        // ----------------------------------------------------------------------
        string LongTimeFormat { get; }

        // ----------------------------------------------------------------------
        string ShortTimeFormat { get; }

        // ----------------------------------------------------------------------
        DurationFormatType DurationType { get; }

        // ----------------------------------------------------------------------
        bool UseDurationSeconds { get; }

        // ----------------------------------------------------------------------
        string GetCollection (int count);

        // ----------------------------------------------------------------------
        string GetCollectionPeriod (int count, DateTime start, DateTime end, TimeSpan duration);

        // ----------------------------------------------------------------------
        string GetDateTime (DateTime dateTime);

        // ----------------------------------------------------------------------
        string GetShortDate (DateTime dateTime);

        // ----------------------------------------------------------------------
        string GetLongTime (DateTime dateTime);

        // ----------------------------------------------------------------------
        string GetShortTime (DateTime dateTime);

        // ----------------------------------------------------------------------
        string GetPeriod (DateTime start, DateTime end);

        // ----------------------------------------------------------------------
        string GetDuration (TimeSpan timeSpan);

        // ----------------------------------------------------------------------
        string GetDuration (TimeSpan timeSpan, DurationFormatType durationFormatType);

        // ----------------------------------------------------------------------
        string GetDuration (int years, int months, int days, int hours, int minutes, int seconds);

        // ----------------------------------------------------------------------
        string GetPeriod (DateTime start, DateTime end, TimeSpan duration);

        // ----------------------------------------------------------------------
        string GetInterval (DateTime start, DateTime end, IntervalEdge startEdge, IntervalEdge endEdge, TimeSpan duration);

        // ----------------------------------------------------------------------
        string GetCalendarPeriod (string start, string end, TimeSpan duration);

        // ----------------------------------------------------------------------
        string GetCalendarPeriod (string context, string start, string end, TimeSpan duration);

        // ----------------------------------------------------------------------
        string GetCalendarPeriod (string startContext, string endContext, string start, string end, TimeSpan duration);

    } // interface ITimeFormatter

    // ------------------------------------------------------------------------
    public interface ITimeInterval : ITimePeriod {

        // ----------------------------------------------------------------------
        bool IsStartOpen { get; }

        // ----------------------------------------------------------------------
        bool IsEndOpen { get; }

        // ----------------------------------------------------------------------
        bool IsOpen { get; }

        // ----------------------------------------------------------------------
        bool IsStartClosed { get; }

        // ----------------------------------------------------------------------
        bool IsEndClosed { get; }

        // ----------------------------------------------------------------------
        bool IsClosed { get; }

        // ----------------------------------------------------------------------
        bool IsEmpty { get; }

        // ----------------------------------------------------------------------
        bool IsDegenerate { get; }

        // ----------------------------------------------------------------------
        bool IsIntervalEnabled { get; }

        // ----------------------------------------------------------------------
        DateTime StartInterval { get; set; }

        // ----------------------------------------------------------------------
        DateTime EndInterval { get; set; }

        // ----------------------------------------------------------------------
        IntervalEdge StartEdge { get; set; }

        // ----------------------------------------------------------------------
        IntervalEdge EndEdge { get; set; }

        // ----------------------------------------------------------------------
        void Move (TimeSpan offset);

        // ----------------------------------------------------------------------
        void ExpandStartTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandEndTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandTo (ITimePeriod period);

        // ----------------------------------------------------------------------
        void ShrinkStartTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ShrinkEndTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ShrinkTo (ITimePeriod period);

        // ----------------------------------------------------------------------
        ITimeInterval Copy (TimeSpan offset);

    } // interface ITimeInterval

    // ------------------------------------------------------------------------
    public interface ITimeLine {

        // ----------------------------------------------------------------------
        ITimePeriodContainer Periods { get; }

        // ----------------------------------------------------------------------
        ITimePeriod Limits { get; }

        // ----------------------------------------------------------------------
        ITimePeriodMapper PeriodMapper { get; }

        // ----------------------------------------------------------------------
        bool HasOverlaps ();

        // ----------------------------------------------------------------------
        bool HasGaps ();

        // ----------------------------------------------------------------------
        ITimePeriodCollection CombinePeriods ();

        // ----------------------------------------------------------------------
        ITimePeriodCollection IntersectPeriods (bool combinePeriods);

        // ----------------------------------------------------------------------
        ITimePeriodCollection CalculateGaps ();

    } // interface ITimeLine

    // ------------------------------------------------------------------------
    public interface ITimeLineMoment {

        // ----------------------------------------------------------------------
        DateTime Moment { get; }

        // ----------------------------------------------------------------------
        int BalanceCount { get; }

        // ----------------------------------------------------------------------
        int StartCount { get; }

        // ----------------------------------------------------------------------
        int EndCount { get; }

        // ----------------------------------------------------------------------
        bool IsEmpty { get; }

        // ----------------------------------------------------------------------
        void AddStart ();

        // ----------------------------------------------------------------------
        void RemoveStart ();

        // ----------------------------------------------------------------------
        void AddEnd ();

        // ----------------------------------------------------------------------
        void RemoveEnd ();

    } // interface ITimeLineMoment

    // ------------------------------------------------------------------------
    public interface ITimeLineMomentCollection : IEnumerable<ITimeLineMoment> {

        // ----------------------------------------------------------------------
        int Count { get; }

        // ----------------------------------------------------------------------
        bool IsEmpty { get; }

        // ----------------------------------------------------------------------
        ITimeLineMoment Min { get; }

        // ----------------------------------------------------------------------
        ITimeLineMoment Max { get; }

        // ----------------------------------------------------------------------
        ITimeLineMoment this [int index] { get; }

        // ----------------------------------------------------------------------
        ITimeLineMoment this [DateTime moment] { get; }

        // ----------------------------------------------------------------------
        void Add (ITimePeriod period);

        // ----------------------------------------------------------------------
        void AddAll (IEnumerable<ITimePeriod> periods);

        // ----------------------------------------------------------------------
        void Remove (ITimePeriod period);

        // ----------------------------------------------------------------------
        ITimeLineMoment Find (DateTime moment);

        // ----------------------------------------------------------------------
        bool Contains (DateTime moment);

        // ----------------------------------------------------------------------
        bool HasOverlaps ();

        // ----------------------------------------------------------------------
        bool HasGaps ();

    } // interface ITimeLineMomentCollection

    // ------------------------------------------------------------------------
    public interface ITimePeriod {

        // ----------------------------------------------------------------------
        bool HasStart { get; }

        // ----------------------------------------------------------------------
        DateTime Start { get; }

        // ----------------------------------------------------------------------
        bool HasEnd { get; }

        // ----------------------------------------------------------------------
        DateTime End { get; }

        // ----------------------------------------------------------------------
        TimeSpan Duration { get; }

        // ----------------------------------------------------------------------
        string DurationDescription { get; }

        // ----------------------------------------------------------------------
        bool IsMoment { get; }

        // ----------------------------------------------------------------------
        bool IsAnytime { get; }

        // ----------------------------------------------------------------------
        bool IsReadOnly { get; }

        // ----------------------------------------------------------------------
        TimeSpan GetDuration (IDurationProvider provider);

        // ----------------------------------------------------------------------
        void Setup (DateTime newStart, DateTime newEnd);

        // ----------------------------------------------------------------------
        bool IsSamePeriod (ITimePeriod test);

        // ----------------------------------------------------------------------
        bool HasInside (DateTime test);

        // ----------------------------------------------------------------------
        bool HasInside (ITimePeriod test);

        // ----------------------------------------------------------------------
        bool IntersectsWith (ITimePeriod test);

        // ----------------------------------------------------------------------
        bool OverlapsWith (ITimePeriod test);

        // ----------------------------------------------------------------------
        PeriodRelation GetRelation (ITimePeriod test);

        // ----------------------------------------------------------------------
        int CompareTo (ITimePeriod other, ITimePeriodComparer comparer);

        // ----------------------------------------------------------------------
        string GetDescription (ITimeFormatter formatter);

    } // interface ITimePeriod

    // ------------------------------------------------------------------------
    public interface ITimePeriodChain : ITimePeriodContainer {

        // ----------------------------------------------------------------------
        new DateTime Start { get; set; }

        // ----------------------------------------------------------------------
        new DateTime End { get; set; }

        // ----------------------------------------------------------------------
        ITimePeriod First { get; }

        // ----------------------------------------------------------------------
        ITimePeriod Last { get; }

    } // interface ITimePeriodChain

    // ------------------------------------------------------------------------
    public interface ITimePeriodCollection : ITimePeriodContainer {

        // ----------------------------------------------------------------------
        new DateTime Start { get; set; }

        // ----------------------------------------------------------------------
        new DateTime End { get; set; }

        // ----------------------------------------------------------------------
        TimeSpan TotalDuration { get; }

        // ----------------------------------------------------------------------
        TimeSpan GetTotalDuration (IDurationProvider provider);

        // ----------------------------------------------------------------------
        void SortBy (ITimePeriodComparer comparer);

        // ----------------------------------------------------------------------
        void SortReverseBy (ITimePeriodComparer comparer);

        // ----------------------------------------------------------------------
        void SortByStart (ListSortDirection sortDirection = ListSortDirection.Ascending);

        // ----------------------------------------------------------------------
        void SortByEnd (ListSortDirection sortDirection = ListSortDirection.Ascending);

        // ----------------------------------------------------------------------
        void SortByDuration (ListSortDirection sortDirection = ListSortDirection.Ascending);

        // ----------------------------------------------------------------------
        bool HasInsidePeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        bool HasOverlaps ();

        // ----------------------------------------------------------------------
        bool HasGaps ();

        // ----------------------------------------------------------------------
        bool HasOverlapPeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        bool HasIntersectionPeriods (DateTime test);

        // ----------------------------------------------------------------------
        bool HasIntersectionPeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        ITimePeriodCollection InsidePeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        ITimePeriodCollection OverlapPeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        ITimePeriodCollection IntersectionPeriods (DateTime test);

        // ----------------------------------------------------------------------
        ITimePeriodCollection IntersectionPeriods (ITimePeriod test);

        // ----------------------------------------------------------------------
        ITimePeriodCollection RelationPeriods (ITimePeriod test, PeriodRelation relation);

    } // interface ITimePeriodCollection

    // ------------------------------------------------------------------------
    public interface ITimePeriodComparer : IComparer<ITimePeriod> { } // interface ITimePeriodComparer

    // ------------------------------------------------------------------------
    public interface ITimePeriodContainer : IList<ITimePeriod>, ITimePeriod {

        // ----------------------------------------------------------------------
        new bool IsReadOnly { get; }

        // ----------------------------------------------------------------------
        bool ContainsPeriod (ITimePeriod test);

        // ----------------------------------------------------------------------
        void AddAll (IEnumerable<ITimePeriod> periods);

        // ----------------------------------------------------------------------
        void Move (TimeSpan delta);

    } // interface ITimePeriodContainer

    // ------------------------------------------------------------------------
    public interface ITimePeriodMapper {

        // ----------------------------------------------------------------------
        DateTime MapStart (DateTime moment);

        // ----------------------------------------------------------------------
        DateTime MapEnd (DateTime moment);

        // ----------------------------------------------------------------------
        DateTime UnmapStart (DateTime moment);

        // ----------------------------------------------------------------------
        DateTime UnmapEnd (DateTime moment);

    } // interface ITimePeriodMapper

    // ------------------------------------------------------------------------
    public interface ITimeRange : ITimePeriod {

        // ----------------------------------------------------------------------
        new DateTime Start { get; set; }

        // ----------------------------------------------------------------------
        new DateTime End { get; set; }

        // ----------------------------------------------------------------------
        new TimeSpan Duration { get; set; }

        // ----------------------------------------------------------------------
        void Move (TimeSpan offset);

        // ----------------------------------------------------------------------
        void ExpandStartTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandEndTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ExpandTo (ITimePeriod period);

        // ----------------------------------------------------------------------
        void ShrinkStartTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ShrinkEndTo (DateTime moment);

        // ----------------------------------------------------------------------
        void ShrinkTo (ITimePeriod period);

        // ----------------------------------------------------------------------
        ITimeRange Copy (TimeSpan offset);

        // ----------------------------------------------------------------------
        ITimeRange GetIntersection (ITimePeriod period);

    } // interface ITimeRange

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
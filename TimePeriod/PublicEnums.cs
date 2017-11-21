// -- FILE ------------------------------------------------------------------
// name       : PublicEnumscs
// project    : Itenso Time Period
// original   : Jani Giannoudis - 2011-2013
// refactor   : Michael Adams - 2017.11.20
// language   : C# 4.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    public enum CalendarNameType {
        Full,
        Abbreviated,
    } // enum CalendarNameType

    // ------------------------------------------------------------------------
    public enum DurationFormatType {
        Compact,
        Detailed,
    } // enum DurationFormatType

    // ------------------------------------------------------------------------
    public enum FiscalQuarterGrouping {
        FourFourFiveWeeks = 0,
            FourFiveFourWeeks = 1,
            FiveFourFourWeeks = 2,
    } // enum FiscalQuarterGrouping

    // ------------------------------------------------------------------------
    public enum FiscalYearAlignment {
        None,
        LastDay,
        NearestDay,
    } // enum FiscalYearAlignment

    // ------------------------------------------------------------------------
    public enum IntervalEdge {
        Closed,
        Open,
    } // enum IntervalEdge

    // ------------------------------------------------------------------------
    // Summary:
    //     Specifies the direction of a sort operation.
    public enum ListSortDirection {
        // Summary:
        //     Sorts in ascending order.
        Ascending = 0,
            //
            // Summary:
            //     Sorts in descending order.
            Descending = 1,
    } // class ListSortDirection

    // ------------------------------------------------------------------------
    public enum PeriodRelation {
        After,
        StartTouching,
        StartInside,
        InsideStartTouching,
        EnclosingStartTouching,
        Enclosing,
        EnclosingEndTouching,
        ExactMatch,
        Inside,
        InsideEndTouching,
        EndInside,
        EndTouching,
        Before,
    } // enum PeriodRelation

    // ------------------------------------------------------------------------
    public enum SeekBoundaryMode {
        Fill,
        Next,
    } // enum SeekBoundaryMode

    // ------------------------------------------------------------------------
    public enum SeekDirection {
        Forward,
        Backward,
    } // enum SeekDirection

    // ------------------------------------------------------------------------
    public enum YearHalfyear {
        First = 1,
            Second = 2,
    } // enum YearHalfyear

    // ------------------------------------------------------------------------
    public enum YearMonth {
        January = 1,
            February = 2,
            March = 3,
            April = 4,
            May = 5,
            June = 6,
            July = 7,
            August = 8,
            September = 9,
            October = 10,
            November = 11,
            December = 12
    } // enum YearMonth

    // ------------------------------------------------------------------------
    public enum YearQuarter {
        First = 1,
            Second = 2,
            Third = 3,
            Fourth = 4,
    } // enum YearQuarter

    // ------------------------------------------------------------------------
    public enum YearType {
        SystemYear,
        CalendarYear,
        FiscalYear,
        SchoolYear,
        CustomYear,
    } // enum YearType

    // ------------------------------------------------------------------------
    public enum YearWeekType {
        Calendar,
        Iso8601,
    } // enum YearWeekType

} // namespace Itenso.TimePeriod

// -- EOF -------------------------------------------------------------------
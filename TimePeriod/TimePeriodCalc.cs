// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCalc.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

// Attempt to fix Issue #17: use ticks, and change the comparison.
// https://stackoverflow.com/questions/5672862/check-if-datetime-instance-falls-in-between-other-two-datetime-objects
// https://github.com/augustoclaro/datetimerange/blob/master/DateTimeRange/DateTimeRange.cs

using System;

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    internal static class TimePeriodCalc {

        // ----------------------------------------------------------------------
       
        public static bool HasInside (ITimePeriod period, DateTime? test) {
            return period.Start <= test && period.End >= test;
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool HasInside (ITimePeriod period, ITimePeriod test) {
            return HasInside (period, test.Start) && HasInside (period, test.End);
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool IntersectsWith (ITimePeriod period, ITimePeriod test) {
            return
            HasInside (period, test.Start) ||
                HasInside (period, test.End) ||
                (test.Start.Ticks < period.Start.Ticks && test.End.Ticks > period.End.Ticks);
        } // IntersectsWith

        // ----------------------------------------------------------------------
        public static bool OverlapsWith (ITimePeriod period, ITimePeriod test) {
            PeriodRelation relation = GetRelation (period, test);
            return
            relation != PeriodRelation.After &&
                relation != PeriodRelation.StartTouching &&
                relation != PeriodRelation.EndTouching &&
                relation != PeriodRelation.Before;
        } // OverlapsWith

        // ----------------------------------------------------------------------
        public static PeriodRelation GetRelation (ITimePeriod period, ITimePeriod test) {           
            if (test.End.Ticks < period.Start.Ticks) return PeriodRelation.After;
            if (test.Start.Ticks > period.End.Ticks) return PeriodRelation.Before;
            if (test.Start.Ticks == period.Start.Ticks && test.End.Ticks == period.End.Ticks) return PeriodRelation.ExactMatch;
            if (test.End.Ticks == period.Start.Ticks) return PeriodRelation.StartTouching;
            if (test.Start.Ticks == period.End.Ticks) return PeriodRelation.EndTouching;
            if (HasInside (period, test)) {
                if (test.Start.Ticks == period.Start.Ticks) return PeriodRelation.EnclosingStartTouching;
                return test.End.Ticks == period.End.Ticks ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing;
            }
            bool periodContainsMyStart = HasInside (test, period.Start);
            bool periodContainsMyEnd = HasInside (test, period.End);
            if (periodContainsMyStart && periodContainsMyEnd) {
                if (test.Start.Ticks == period.Start.Ticks) return PeriodRelation.InsideStartTouching;
                return test.End.Ticks == period.End.Ticks ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside;
            }
            if (periodContainsMyStart) return PeriodRelation.StartInside;
            if (periodContainsMyEnd) return PeriodRelation.EndInside;
            throw new InvalidOperationException ("invalid period relation of '" + period + "' and '" + test + "'");
        } // GetRelation

    } // class TimePeriodCalc

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
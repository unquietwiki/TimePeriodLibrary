// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCalc.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

// Attempt to fix Issue #17: use ticks, and change the comparison. Ended up using DateTime.Compare to resolve this.
// https://stackoverflow.com/questions/5672862/check-if-datetime-instance-falls-in-between-other-two-datetime-objects
// https://github.com/augustoclaro/datetimerange/blob/master/DateTimeRange/DateTimeRange.cs
// https://docs.microsoft.com/en-us/dotnet/api/system.datetime.compare?view=netcore-2.0#System_DateTime_Compare_System_DateTime_System_DateTime_

using System;

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    internal static class TimePeriodCalc {

        // ----------------------------------------------------------------------      
        public static bool HasInside (ITimePeriod period, DateTime test) {
            var testStart = DateTime.Compare(test,period.Start);
            var testEnd = DateTime.Compare(test,period.End);
            if(testStart >= -1 && testEnd <= 1) return true;
            return false;
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool HasInside (ITimePeriod period, ITimePeriod test) {
            var testStart = DateTime.Compare(test.Start,period.Start);
            var testEnd = DateTime.Compare(test.End,period.End);
            if(testStart >= -1 && testEnd <= 1) return true;
            return false;            
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool IntersectsWith (ITimePeriod period, ITimePeriod test) {
            return HasInside (period, test.Start) || HasInside (period, test.End);
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
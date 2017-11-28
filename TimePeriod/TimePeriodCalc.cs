// -- FILE ------------------------------------------------------------------
// name       : TimePeriodCalc.cs
// project    : Itenso Time Period
// created    : Jani Giannoudis - 2011.02.18
// rewritten  : Michael Adams - 2017.11.27
// language   : C# 4.0
// environment: .NET 2.0
// copyright  : (c) 2011-2012 by Itenso GmbH, Switzerland
// --------------------------------------------------------------------------

// Modified heavily to try to fix issue #17
// GetPeriodRelation could in theory return multiple values, so used the diagram from CodeProject to help figure out the difference
// between testing a DateTime in a TimePeriod, and testing a TimePeriod in a TimePeriod.
//
// https://stackoverflow.com/questions/5672862/check-if-datetime-instance-falls-in-between-other-two-datetime-objects
// https://github.com/augustoclaro/datetimerange/blob/master/DateTimeRange/DateTimeRange.cs
// https://www.codeproject.com/Articles/168662/Time-Period-Library-for-NET

using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod {

    // ------------------------------------------------------------------------
    internal static class TimePeriodCalc {

        // ----------------------------------------------------------------------      
        public static bool HasInside (ITimePeriod period, DateTime test) {
            if((test.Ticks >= period.Start.Ticks) && (test.Ticks <= period.End.Ticks)) return true;
            return false;
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool HasInside (ITimePeriod period, ITimePeriod test) {
            var relations = GetRelation (period, test);
            var unwanted = new List<PeriodRelation> () {
                PeriodRelation.Before,
                PeriodRelation.After,
                PeriodRelation.EndTouching,
                PeriodRelation.StartTouching
            };
            foreach (var r in relations)
                if (unwanted.Contains (r)) return false;;
            return true;
        } // HasInside

        // ----------------------------------------------------------------------
        public static bool IntersectsWith (ITimePeriod period, ITimePeriod test) {
            return HasInside (period, test.Start) || HasInside (period, test.End);
        } // IntersectsWith

        // ----------------------------------------------------------------------
        public static bool OverlapsWith (ITimePeriod period, ITimePeriod test) {
            var relations = GetRelation (period, test);
            var unwanted = new List<PeriodRelation> () { PeriodRelation.Before, PeriodRelation.StartTouching, PeriodRelation.EndTouching, PeriodRelation.After };
            foreach (var r in relations)
                if (unwanted.Contains (r)) return false;;
            return true;
        } // OverlapsWith
        
        public static List<PeriodRelation> GetRelation (ITimePeriod period, ITimePeriod test) {
            var prList = new List<PeriodRelation> ();
            try {
                bool periodContainsMyStart = HasInside (test, period.Start);
                bool periodContainsMyEnd = HasInside (test, period.End);
                if (periodContainsMyStart) prList.Add (PeriodRelation.StartInside);
                if (periodContainsMyEnd) prList.Add (PeriodRelation.EndInside);
                if (test.End.Ticks < period.Start.Ticks) prList.Add (PeriodRelation.After);
                if (test.Start.Ticks > period.End.Ticks) prList.Add (PeriodRelation.Before);
                if (test.Start.Ticks == period.Start.Ticks && test.End.Ticks == period.End.Ticks) prList.Add (PeriodRelation.ExactMatch);
                if (test.End.Ticks == period.Start.Ticks) prList.Add (PeriodRelation.StartTouching);
                if (test.Start.Ticks == period.End.Ticks) prList.Add (PeriodRelation.EndTouching);
                if (HasInside (period, test.Start) && (HasInside (period, test.End))) {
                    if (test.Start.Ticks == period.Start.Ticks) prList.Add (PeriodRelation.EnclosingStartTouching);
                    prList.Add (test.End.Ticks == period.End.Ticks ? PeriodRelation.EnclosingEndTouching : PeriodRelation.Enclosing);
                }
                if (periodContainsMyStart && periodContainsMyEnd) {
                    if (test.Start.Ticks == period.Start.Ticks) prList.Add (PeriodRelation.InsideStartTouching);
                    prList.Add (test.End.Ticks == period.End.Ticks ? PeriodRelation.InsideEndTouching : PeriodRelation.Inside);
                }
            } catch {
                throw new InvalidOperationException ("invalid period relation of '" + period + "' and '" + test + "'");
            }
            return prList;
        }

    } // class TimePeriodCalc

} // namespace Itenso.TimePeriod
// -- EOF -------------------------------------------------------------------
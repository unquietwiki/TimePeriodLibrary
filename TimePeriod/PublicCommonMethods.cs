// -- FILE ------------------------------------------------------------------
// name       : PublicCommonMethods.cs
// project    : Itenso Time Period
// original   : Michael Adams - 2017.11.27
// language   : C# 4.0
// copyright  : (c) 2011-2013 by Itenso GmbH, Switzerland
// copyright  : (c) 2017 by Michael Adams
// --------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace Itenso.TimePeriod {

    public static class CommonMethods {
        public static void checkNull (object obj, string name) {
            if (obj == null) throw new ArgumentNullException (name);
        }
    }
}
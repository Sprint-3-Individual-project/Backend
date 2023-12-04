using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct
{
    public class Clock
    {
        private static DateTime? _overrideTime;  // Voor unit testdoeleinden

        public static DateTime CurrentTime
        {
            get { return _overrideTime ?? DateTime.Now; }
        }

        // Methode voor het overschrijven van de tijd tijdens unit tests
        public static void SetStaticTime(DateTime overriddenTime)
        {
            _overrideTime = overriddenTime;
        }

        // Methode om de tijdsovername ongedaan te maken na een unit test
        public static void ResetTimeOverride()
        {
            _overrideTime = DateTime.Now;
        }
    }
}

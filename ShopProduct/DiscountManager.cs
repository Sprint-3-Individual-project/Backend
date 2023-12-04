using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct
{
    public class DiscountManager
    {
        public float MorningDiscountMultiplier { get { return morningDiscountMultiplier; } }
        public float EveningDiscountMultiplier { get { return eveningDiscountMultiplier; } }
        public float DefaultDiscountMultiplier { get { return defaultDiscountMultiplier; } }

        private readonly float morningDiscountMultiplier = 0.70f; //30%
        private readonly float eveningDiscountMultiplier = 0.90f; //10%
        private readonly float defaultDiscountMultiplier = 1;     //0%
        public float DetermineDiscountMultiplier(DateTime time)
        {
            DateTime morningTimeDiscount1 = DateTime.Today.AddHours(10);
            DateTime morningTimeDiscount2 = DateTime.Today.AddHours(13);
            DateTime eveningTimeDiscount1 = DateTime.Today.AddHours(18);
            DateTime eveningTimeDiscount2 = DateTime.Today.AddHours(23);

            if (time >= morningTimeDiscount1 && time <= morningTimeDiscount2)
            {
                return MorningDiscountMultiplier;
            }
            else if (time >= eveningTimeDiscount1 && time <= eveningTimeDiscount2)
            {
                return EveningDiscountMultiplier;
            }
            else
            {
                return DefaultDiscountMultiplier;
            }
        }
    }
}

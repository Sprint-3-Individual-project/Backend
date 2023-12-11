using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopProduct
{
    public class DiscountManager
    {
        public int MorningDiscountPercentage { get { return morningDiscountPercentage; } }
        public int EveningDiscountPercentage { get { return eveningDiscountPercentage; } }
        public int DefaultDiscountPercentage { get { return defaultDiscountPercentage; } }

        private readonly int morningDiscountPercentage = 30; //30%
        private readonly int eveningDiscountPercentage = 10; //10%
        private readonly int defaultDiscountPercentage = 0;     //0%
        public int DetermineDiscountMultiplier(DateTime time)
        {
            DateTime morningTimeDiscount1 = DateTime.Today.AddHours(10);
            DateTime morningTimeDiscount2 = DateTime.Today.AddHours(13);
            DateTime eveningTimeDiscount1 = DateTime.Today.AddHours(18);
            DateTime eveningTimeDiscount2 = DateTime.Today.AddHours(23);

            if (time >= morningTimeDiscount1 && time <= morningTimeDiscount2)
            {
                return MorningDiscountPercentage;
            }
            else if (time >= eveningTimeDiscount1 && time <= eveningTimeDiscount2)
            {
                return EveningDiscountPercentage;
            }
            else
            {
                return DefaultDiscountPercentage;
            }
        }
    }
}

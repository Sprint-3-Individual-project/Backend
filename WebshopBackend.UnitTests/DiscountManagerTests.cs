using ShopProduct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebshopBackend.UnitTests
{
    public class DiscountManagerTests
    {
        [Test]
        public void DetermineDiscountMultiplier_Morning_ReturnsMorningDiscount()
        {
            // Arrange
            var discountManager = new DiscountManager();
            var morningTime = DateTime.Today.AddHours(11);

            // Act
            var result = discountManager.DetermineDiscountMultiplier(morningTime);

            // Assert
            Assert.AreEqual(discountManager.MorningDiscountMultiplier, result);
        }

        [Test]
        public void DetermineDiscountMultiplier_Evening_ReturnsEveningDiscount()
        {
            // Arrange
            var discountManager = new DiscountManager();
            var eveningTime = DateTime.Today.AddHours(20);

            // Act
            var result = discountManager.DetermineDiscountMultiplier(eveningTime);

            // Assert
            Assert.AreEqual(discountManager.EveningDiscountMultiplier, result);
        }

        [Test]
        public void DetermineDiscountMultiplier_Afternoon_ReturnsDefaultDiscount()
        {
            // Arrange
            var discountManager = new DiscountManager();
            var afternoonTime = DateTime.Today.AddHours(14);

            // Act
            var result = discountManager.DetermineDiscountMultiplier(afternoonTime);

            // Assert
            Assert.AreEqual(discountManager.DefaultDiscountMultiplier, result);
        }
    }
}

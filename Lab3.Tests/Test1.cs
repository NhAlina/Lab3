using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lab3;
using System;
using System.Collections.Generic;

namespace Lab3.Tests
{
    [TestClass]
    public class RentalTests
    {
        //Тест успішної ініціалізації квартири
        [TestMethod]
        public void Apartment_Constructor_WithValidData_InitializesCorrectly()
        {
            // Arrange & Act
            var apartment = new ApartmentRental("вул. Шевченка, 10", 15000, 60, 2);

            // Assert
            Assert.AreEqual("вул. Шевченка, 10", apartment.Address);
            Assert.AreEqual(15000, apartment.MonthlyPrice);
            Assert.AreEqual(60, apartment.TotalArea);
            Assert.AreEqual(2, apartment.RoomsCount);
        }

        //Тест валідації від'ємної ціни (очікуємо ArgumentException)
        [TestMethod]
        public void Constructor_WithNegativePrice_ThrowsArgumentException()
        {
            Assert.ThrowsException<ArgumentException>(() =>
            {
                new ApartmentRental("вул. Франка, 5", -5000, 50, 1);
            });
        }

        //Тест валідації порожньої адреси
        [TestMethod]
        public void Address_WithNullOrEmpty_ThrowsArgumentException()
        {
            var house = new HouseRental("вул. Лісова, 1", 20000, 120, true);

            Assert.ThrowsException<ArgumentException>(() =>
            {
                house.Address = "";
            });
        }

        //Тест розрахунку комунальних послуг квартири
        [TestMethod]
        public void Apartment_CalculateUtilities_CalculatesCorrectly()
        {
            // Rooms = 2 (2 * 400 = 800), Area = 50 (50 * 20 = 1000) -> 1800
            var apt = new ApartmentRental("вул. Київська, 1", 10000, 50, 2);
            double expectedUtilities = 1800.0;

            double actualUtilities = apt.CalculateUtilities();

            Assert.AreEqual(expectedUtilities, actualUtilities);
        }

        //Тест формування протоколу оплати (поліморфізм)
        [TestMethod]
        public void Apartment_Pay_ReturnsCorrectProtocol()
        {
            var apt = new ApartmentRental("вул. Миру, 7", 10000, 50, 2);
            string protocol = apt.Pay();

            StringAssert.Contains(protocol, "Ініціалізація платежу");
            StringAssert.Contains(protocol, "вул. Миру, 7");
        }

        //Тест загального аналізу: розрахунок середньої вартості за 1 м²
        [TestMethod]
        public void RentalAnalysis_GetAveragePricePerSquareMeter_CalculatesCorrectly()
        {
            // Arrange
            var list = new List<PropertyRental>
            {
                new ApartmentRental("Кв 1", 10000, 50, 2),     // ціна 10000, площа 50
                new HouseRental("Будинок 1", 20000, 100, true) // ціна 20000, площа 100
            };
            // Всього вартість = 30000, площа = 150 -> середня за 1 м² = 200.0

            // Act
            double average = RentalAnalysis.GetAveragePricePerSquareMeter(list);

            // Assert
            Assert.AreEqual(200.0, average);
        }

        //Тест аналізу з порожнім списком
        [TestMethod]
        public void RentalAnalysis_EmptyList_ReturnsZero()
        {
            var list = new List<PropertyRental>();
            double average = RentalAnalysis.GetAveragePricePerSquareMeter(list);
            Assert.AreEqual(0.0, average);
        }
    }
}

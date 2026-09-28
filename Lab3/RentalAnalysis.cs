using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lab3
{
    public class RentalAnalysis
    {
        /// <summary>
        /// Розраховує середню вартість оренди всіх об'єктів за 1 м² (Загальна вартість / Загальна площа).
        /// </summary>
        public static double GetAveragePricePerSquareMeter(List<PropertyRental> rentals)
        {
            if (rentals == null || rentals.Count == 0)
            {
                return 0.0;
            }

            double totalRentCost = 0;
            double totalArea = 0;

            foreach (var rental in rentals)
            {
                totalRentCost += rental.MonthlyPrice;
                totalArea += rental.TotalArea;
            }

            if (totalArea <= 0)
            {
                return 0.0;
            }

            // Округлюємо до 2 знаків після коми
            return Math.Round(totalRentCost / totalArea, 2);
        }
    }
}
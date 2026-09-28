using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class ApartmentRental : PropertyRental
    {
        private int _roomsCount;

        public int RoomsCount
        {
            get { return _roomsCount; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Кількість кімнат повинна бути більшою за нуль.", nameof(value));
                }
                _roomsCount = value;
            }
        }

        public ApartmentRental(string address, double monthlyPrice, double totalArea, int roomsCount)
            : base(address, monthlyPrice, totalArea)
        {
            if (roomsCount <= 0)
            {
                throw new ArgumentException("Кількість кімнат повинна бути більшою за нуль.");
            }
            this.RoomsCount = roomsCount;
        }

        // Метод: Розрахувати комунальні послуги
        public double CalculateUtilities()
        {
            //фіксована сума за кожну кімнату + коефіцієнт опалення за площу
            return (RoomsCount * 400.0) + (TotalArea * 20.0);
        }

        public override string Pay()
        {
            double utilities = CalculateUtilities();
            double total = MonthlyPrice + utilities;

            var protocol = new StringBuilder();
            protocol.AppendLine($"Ініціалізація платежу: Оренда квартири на суму {total:C}");
            protocol.AppendLine($"Дата транзакції: {AgreementDate.ToShortDateString()}");
            protocol.AppendLine($"Адреса: {Address} (Кількість кімнат: {RoomsCount}, Площа: {TotalArea} м²)");
            protocol.AppendLine($"Комунальні послуги: {utilities:C}");
            return protocol.ToString();
        }

        public override string TerminateContract()
        {
            var protocol = new StringBuilder();
            protocol.AppendLine($"Розірвання договору оренди квартири за адресою: {Address}");
            protocol.AppendLine($"Статус: Ключі здано, договір закрито.");
            return protocol.ToString();
        }
    }
}

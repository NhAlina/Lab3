using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public class HouseRental : PropertyRental
    {
        public bool HasGarden { get; set; }

        public HouseRental(string address, double monthlyPrice, double totalArea, bool hasGarden)
            : base(address, monthlyPrice, totalArea)
        {
            this.HasGarden = hasGarden;
        }

        // Метод: Оцінити інфраструктуру
        public string EvaluateInfrastructure()
        {
            if (HasGarden)
            {
                return "Будинок з присадибною ділянкою та зоною відпочинку (високий комфорт).";
            }
            return "Будинок без саду (базова прибудинкова територія).";
        }

        public override string Pay()
        {
            // Обслуговування території: з садом потрібен додатковий догляд
            double maintenanceFee = HasGarden ? 1200.0 : 400.0;
            double total = MonthlyPrice + maintenanceFee;

            var protocol = new StringBuilder();
            protocol.AppendLine($"Ініціалізація платежу: Оренда будинку на суму {total:C}");
            protocol.AppendLine($"Дата транзакції: {AgreementDate.ToShortDateString()}");
            protocol.AppendLine($"Адреса: {Address} (Площа: {TotalArea} м²)");
            protocol.AppendLine($"Інфраструктура: {EvaluateInfrastructure()}");
            protocol.AppendLine($"Догляд за територією: {maintenanceFee:C}");
            return protocol.ToString();
        }

        public override string TerminateContract()
        {
            var protocol = new StringBuilder();
            protocol.AppendLine($"Розірвання договору оренди будинку за адресою: {Address}");
            protocol.AppendLine($"Статус: Ділянку та приміщення оглянуто, договір закрито.");
            return protocol.ToString();
        }
    }
}
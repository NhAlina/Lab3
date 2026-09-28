using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3
{
    public abstract class PropertyRental
    {
        private string _address = "";
        private double _monthlyPrice;
        private double _totalArea;

        public string Address
        {
            get { return _address; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Адреса не може бути порожньою.", nameof(value));
                }
                _address = value;
            }
        }

        public double MonthlyPrice
        {
            get { return _monthlyPrice; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Вартість за місяць повинна бути більшою за нуль.", nameof(value));
                }
                _monthlyPrice = value;
            }
        }

        public double TotalArea
        {
            get { return _totalArea; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Загальна площа повинна бути більшою за нуль.", nameof(value));
                }
                _totalArea = value;
            }
        }

        public DateTime AgreementDate { get; set; }

        public PropertyRental(string address, double monthlyPrice, double totalArea)
        {
            if (monthlyPrice <= 0)
                throw new ArgumentException("Вартість за місяць повинна бути більшою за нуль.");

            if (totalArea <= 0)
                throw new ArgumentException("Загальна площа повинна бути більшою за нуль.");

            this.Address = address;
            this.MonthlyPrice = monthlyPrice;
            this.TotalArea = totalArea;
            this.AgreementDate = DateTime.Now;
        }

        // Абстрактні методи: Оплатити та Розірвати договір
        public abstract string Pay();
        public abstract string TerminateContract();
    }
}

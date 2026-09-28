using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab3
{
    public partial class MainWindow : Window
    {
        // список зберігає як квартири, так і будинки
        private List<PropertyRental> _rentals = new List<PropertyRental>();

        public MainWindow()
        {
            InitializeComponent();
            PropertyTypeComboBox.SelectedIndex = 0;
            UpdateUiVisibility();
        }

        // Обробник зміни типу нерухомості у випадаючому списку
        private void PropertyTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateUiVisibility();
        }

        // Перемикання видимості полів відповідно до обраного типу
        private void UpdateUiVisibility()
        {
            if (ApartmentFields == null || HouseFields == null)
                return;

            if (PropertyTypeComboBox.SelectedIndex == 0) // Обрано Квартиру
            {
                ApartmentFields.Visibility = Visibility.Visible;
                HouseFields.Visibility = Visibility.Collapsed;
            }
            else // Обрано Будинок
            {
                ApartmentFields.Visibility = Visibility.Collapsed;
                HouseFields.Visibility = Visibility.Visible;
            }
        }

        // Натискання кнопки "Оформити та оплатити"
        private void AddAndPayButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string address = AddressTextBox.Text;

                // Перевірка введення числових значень
                if (!double.TryParse(MonthlyPriceTextBox.Text, out double monthlyPrice))
                {
                    MessageBox.Show("Будь ласка, введіть коректну вартість за місяць.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (!double.TryParse(TotalAreaTextBox.Text, out double totalArea))
                {
                    MessageBox.Show("Будь ласка, введіть коректну площу.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                PropertyRental rental;

                // Створення відповідного об'єкта
                if (PropertyTypeComboBox.SelectedIndex == 0)
                {
                    if (!int.TryParse(RoomsCountTextBox.Text, out int rooms))
                    {
                        MessageBox.Show("Будь ласка, введіть коректну кількість кімнат.", "Помилка введення", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    rental = new ApartmentRental(address, monthlyPrice, totalArea, rooms);
                }
                else
                {
                    bool hasGarden = HasGardenCheckBox.IsChecked ?? false;
                    rental = new HouseRental(address, monthlyPrice, totalArea, hasGarden);
                }

                // Додаємо оренду до нашого списку
                _rentals.Add(rental);

                // Поліморфізм: викликається версія Pay() відповідного нащадка
                string protocol = rental.Pay();
                RentalsListBox.Items.Add(protocol);

                // Оновлюємо середню вартість за 1 м²
                UpdateAnalytics();

                // Очищаємо поля для нового введення
                AddressTextBox.Clear();
                MonthlyPriceTextBox.Clear();
                TotalAreaTextBox.Clear();
                RoomsCountTextBox.Clear();
                HasGardenCheckBox.IsChecked = false;
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Помилка валідації", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Сталася неочікувана помилка: " + ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Натискання кнопки "Розірвати останній договір"
        private void TerminateButton_Click(object sender, RoutedEventArgs e)
        {
            if (_rentals.Count == 0)
            {
                MessageBox.Show("Немає активних договорів для розірвання.", "Повідомлення", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            // 1. Беремо індекс останнього договору
            int lastIndex = _rentals.Count - 1;
            var lastRental = _rentals[lastIndex];

            // 2. Викликаємо метод розірвання договору
            string result = lastRental.TerminateContract();
            RentalsListBox.Items.Add(result);

            // 3. Видаляємо договір зі списку активних оренд
            _rentals.RemoveAt(lastIndex);

            // 4. Оновлюємо аналітику на екрані
            UpdateAnalytics();
        }

        // Оновлення підсумку аналітики
        private void UpdateAnalytics()
        {
            double averagePrice = RentalAnalysis.GetAveragePricePerSquareMeter(_rentals);
            AveragePricePerMeterTextBlock.Text = $"{averagePrice:N2} грн/м²";
        }
    }
}
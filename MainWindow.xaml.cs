using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
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

namespace chizhprimebd
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CarsContext _context = new CarsContext();
        private ObservableCollection<Car> _cars;
        private ObservableCollection<Car> _filteredCars;

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
            LoadManufacturers();
        }
        private void LoadManufacturers()
        {
            // Загружаем список производителей из базы данных
            var manufacturers = _context.Manufacturers.ToList();

            // Привязываем список к ComboBox
            CmbFiltr.ItemsSource = manufacturers;

            // Устанавливаем отображаемое значение и путь к значению
            CmbFiltr.DisplayMemberPath = "Manufacturers";  // Название производителя
            CmbFiltr.SelectedValuePath = "IdManufacturers"; // Идентификатор производителя
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var text = TxtSearch.Text.ToLower();
            var result = _context.Cars
                .Include(c => c.ManufacturersNavigation)
                .Include(c => c.ModelNavigation)
                .Where(c => c.ManufacturersNavigation.Manufacturers.ToLower().Contains(text) ||
                            c.ModelNavigation.Model1.ToLower().Contains(text))
                .ToList();

            CarsDataGrid.ItemsSource = result;
        }

        private void BtnSortUp_Click(object sender, RoutedEventArgs e)
        {
            var sortedCars = _filteredCars.OrderBy(c => c.Mileage ?? 0).ToList();
            _filteredCars.Clear();
            foreach (var car in sortedCars)
            {
                _filteredCars.Add(car);
            }

            // Обновляем привязку
            CarsDataGrid.ItemsSource = _filteredCars;
        }

        // Сортировка по убыванию
        private void BtnSortDown_Click(object sender, RoutedEventArgs e)
        {
            var sortedCars = _filteredCars.OrderByDescending(c => c.Mileage ?? 0).ToList();
            _filteredCars.Clear();
            foreach (var car in sortedCars)
            {
                _filteredCars.Add(car);
            }

            // Обновляем привязку
            CarsDataGrid.ItemsSource = _filteredCars;
        }


        // Фильтрация по производителю
        private void CmbFiltr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранного производителя
            var selectedManufacturer = (Manufacturer)CmbFiltr.SelectedItem;

            if (selectedManufacturer == null)
            {
                // Если производитель не выбран, показываем все автомобили
                _filteredCars = new ObservableCollection<Car>(_cars);
            }
            else
            {
                // Фильтруем автомобили по выбранному производителю
                _filteredCars = new ObservableCollection<Car>(_cars
                    .Where(c => c.ManufacturersNavigation == selectedManufacturer)
                    .ToList());
            }

            // Обновляем привязку к DataGrid
            CarsDataGrid.ItemsSource = _filteredCars;
        }




        private void LoadData()
        {
            // Загружаем все данные в _cars
            _cars = new ObservableCollection<Car>(_context.Cars
                .Include(c => c.ManufacturersNavigation)
                .Include(c => c.ModelNavigation)
                .ToList());

            // Изначально фильтруем все данные
            _filteredCars = new ObservableCollection<Car>(_cars);

            // Привязываем коллекцию к DataGrid
            CarsDataGrid.ItemsSource = _filteredCars;
        }


    }
}

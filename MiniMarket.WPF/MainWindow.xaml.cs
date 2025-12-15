using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace MiniMarket.WPF
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient();

        public MainWindow()
        {
            InitializeComponent();
            _httpClient.BaseAddress = new Uri("http://localhost:5114/");
        }

        // --- POMOCNICZE: Ładowanie danych ---
        private async Task LoadProducts()
        {
            try
            {
                var products = await _httpClient.GetFromJsonAsync<List<ProductDTO>>("api/Product");
                ProductsGrid.ItemsSource = products;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd połączenia z API: {ex.Message}");
            }
        }

        private async void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            await LoadProducts();
        }

        // --- DODAWANIE (Create) ---
        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!decimal.TryParse(PriceInput.Text, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal price))
                {
                    MessageBox.Show("Podaj poprawną cenę (z kropką).");
                    return;
                }

                var newProduct = new
                {
                    Name = NameInput.Text,
                    Description = "Dodane z WPF",
                    Price = price,
                    Category = CategoryInput.Text,
                    // Upewnij się, że to ID jest poprawne w Twojej bazie!
                    SellerId = Guid.Parse("e1b4e100-056f-4260-9410-a53f4db7bae5")
                };

                var response = await _httpClient.PostAsJsonAsync("api/Product", newProduct);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Dodano produkt!");
                    await LoadProducts();
                }
                else
                {
                    MessageBox.Show($"Błąd API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
        }

        // --- USUWANIE ZAZNACZONEGO (Delete) ---
        private async void DeleteSelected_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Sprawdzamy, co jest zaznaczone w tabeli
                if (ProductsGrid.SelectedItem is ProductDTO selectedProduct)
                {
                    var result = MessageBox.Show($"Czy usunąć: {selectedProduct.Name}?", "Potwierdzenie", MessageBoxButton.YesNo);

                    if (result == MessageBoxResult.Yes)
                    {
                        var response = await _httpClient.DeleteAsync($"api/Product/{selectedProduct.Id}");

                        if (response.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Usunięto!");
                            await LoadProducts();
                        }
                        else
                        {
                            MessageBox.Show($"Błąd API: {response.StatusCode}");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Zaznacz najpierw produkt na liście!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
        }

        // --- RABAT (Stored Procedure) ---
        private async void DiscountButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var url = "api/Product/apply-discount?category=Spożywcze&percentage=10";
                var response = await _httpClient.PostAsync(url, null);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Rabat naliczony!");
                    await LoadProducts();
                }
                else
                {
                    MessageBox.Show($"Błąd API: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd: {ex.Message}");
            }
        }
    }
}
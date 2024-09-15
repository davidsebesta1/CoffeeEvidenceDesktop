using CoffeeEvidenceDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace CoffeeEvidenceDesktop.ViewModels
{
    public partial class CoffeeUsageViewModel : BaseViewModel
    {
        public Month[] Months { get; } = [new Month("Všechny", 0), new Month("Leden", 1), new Month("Únor", 2), new Month("Březen", 3), new Month("Duben", 4), new Month("Květen", 5), new Month("Červen", 6), new Month("Červenec", 7), new Month("Spren", 8), new Month("Září", 9), new Month("Říjen", 10), new Month("Listopad", 11), new Month("Prosinec", 12)];
        private HttpClient _client;

        [ObservableProperty]
        private ObservableCollection<MonthlyUsage> _usage;

        [ObservableProperty]
        private Month _selectedMonth;

        public CoffeeUsageViewModel(HttpClient client)
        {
            PageTitle = "Seznam Použití";

            _client = client;
            _usage = new ObservableCollection<MonthlyUsage>();
            SelectedMonth = Months[0];
        }

        async partial void OnSelectedMonthChanged(Month value)
        {
            if (value != null)
            {
                await GetCoffeeUsage(value);
            }
        }

        [RelayCommand]
        private async Task GetCoffeeUsage(Month month)
        {
            _usage.Clear();

            string json = string.Empty;
            try
            {
                json = await _client.GetStringAsync("http://ajax1.lmsoft.cz/procedure.php?cmd=getSummaryOfDrinks" + (month.Id == 0 ? string.Empty : $"&month={month.Id}"));

                List<List<string>> data = JsonConvert.DeserializeObject<List<List<string>>>(json);
                foreach (List<string> item in data)
                {
                    _usage.Add(new MonthlyUsage(item[0], item[1], item[2]));
                }
            }
            catch (JsonSerializationException e)
            {
                await Shell.Current.DisplayAlert("Error", $"Nepodařilo se načíst data o tomto měsíci. Získaná odpověd od serveru je neplatná. ({json})", "Okay");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"General Error:{e.Message}", "Okay");
            }
        }
    }
}

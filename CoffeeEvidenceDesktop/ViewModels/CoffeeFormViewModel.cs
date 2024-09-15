using CoffeeEvidenceDesktop.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;

namespace CoffeeEvidenceDesktop.ViewModels
{
    public partial class CoffeeFormViewModel : BaseViewModel
    {
        private readonly HttpClient _client;

        [ObservableProperty]
        private ObservableCollection<User> _users = new ObservableCollection<User>();

        [ObservableProperty]
        private ObservableCollection<DrinkType> _types = new ObservableCollection<DrinkType>();

        [ObservableProperty]
        private User _selectedUser;

        public CoffeeFormViewModel(HttpClient httpClient)
        {
            PageTitle = "Formulář";

            _client = httpClient;
        }

        [RelayCommand]
        private void IncrementCoffeeAmount(DrinkType type)
        {
            type.Amount++;
        }

        [RelayCommand]
        private void DecrementCoffeeAmount(DrinkType type)
        {
            type.Amount--;
        }

        [RelayCommand]
        private async Task Send()
        {
            try
            {
                List<KeyValuePair<string, string>> formData = new List<KeyValuePair<string, string>>(1 + _types.Count)
                {
                    new KeyValuePair<string, string>("user", SelectedUser.ID.ToString())
                };

                foreach (DrinkType type in _types)
                {
                    formData.Add(new KeyValuePair<string, string>("type[]", type.Amount.ToString()));
                }
                HttpContent content = new FormUrlEncodedContent(formData);

                HttpResponseMessage response = await _client.PostAsync("http://ajax1.lmsoft.cz/procedure.php?cmd=saveDrinks", content);
                string responseString = await response.Content.ReadAsStringAsync();
                JObject jsonObject = JObject.Parse(responseString);

                int code = int.Parse((string)jsonObject["msg"]);
                await Shell.Current.DisplayAlert("Odpověd ze serveru", (code == 1 ? "Data uložena" : $"Chyba, kod: {code}"), "Okay");

                ResetForm();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"General error: {ex.Message}", "Okay");
            }
        }

        private void ResetForm()
        {
            SelectedUser = Users.FirstOrDefault();
            foreach (DrinkType type in Types)
            {
                type.Amount = 0;
            }
        }

        [RelayCommand]
        private async Task GetInputs()
        {
            string json = string.Empty;
            try
            {
                json = await _client.GetStringAsync("http://ajax1.lmsoft.cz/procedure.php?cmd=getPeopleList");

                Dictionary<int, User> data = JsonConvert.DeserializeObject<Dictionary<int, User>>(json);

                foreach (var kvp in data)
                {
                    Users.Add(kvp.Value);
                }

                SelectedUser = Users.FirstOrDefault();
            }
            catch (JsonSerializationException e)
            {
                await Shell.Current.DisplayAlert("Error", $"Nepodařilo se načíst uživatele. Data ze serveru jsou neplatná ({json})", "Okay");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"General Error:{e.Message}", "Okay");
            }

            try
            {
                json = await _client.GetStringAsync("http://ajax1.lmsoft.cz/procedure.php?cmd=getTypesList");

                Dictionary<int, DrinkType> data = JsonConvert.DeserializeObject<Dictionary<int, DrinkType>>(json);

                foreach (var kvp in data)
                {
                    Types.Add(kvp.Value);
                }
            }
            catch (JsonSerializationException e)
            {
                await Shell.Current.DisplayAlert("Error", $"Nepodařilo se načíst typy. Data ze serveru jsou neplatná ({json})", "Okay");
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", $"General Error:{e.Message}", "Okay");
            }
        }
    }
}

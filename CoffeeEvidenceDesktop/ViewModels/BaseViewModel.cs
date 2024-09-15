using CommunityToolkit.Mvvm.ComponentModel;

namespace CoffeeEvidenceDesktop.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _pageTitle;

        public BaseViewModel()
        {

        }
    }
}

using CoffeeEvidenceDesktop.ViewModels;

namespace CoffeeEvidenceDesktop;

public partial class CoffeeUsagePage : ContentPage
{
    public CoffeeUsagePage(CoffeeUsageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
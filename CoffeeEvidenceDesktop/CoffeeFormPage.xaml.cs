using CoffeeEvidenceDesktop.ViewModels;

namespace CoffeeEvidenceDesktop;

public partial class CoffeeFormPage : ContentPage
{
    public CoffeeFormPage(CoffeeFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private void Entry_TextChanged(object sender, TextChangedEventArgs e)
    {
        Entry entry = sender as Entry;

        if (int.TryParse(e.NewTextValue, out _))
        {
            return;
        }

        entry.Text = e.OldTextValue;
    }
}
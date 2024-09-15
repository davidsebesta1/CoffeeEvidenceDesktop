using CommunityToolkit.Mvvm.ComponentModel;

namespace CoffeeEvidenceDesktop.Models
{
    public partial class DrinkType : ObservableObject
    {
        public int ID { get; set; }
        public string Typ { get; set; }

        [ObservableProperty]
        private int _amount;

        public DrinkType(int iD, string typ)
        {
            ID = iD;
            Typ = typ;
        }
    }
}

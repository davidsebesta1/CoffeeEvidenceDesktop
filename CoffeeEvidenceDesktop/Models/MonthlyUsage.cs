
namespace CoffeeEvidenceDesktop.Models
{
    public class MonthlyUsage
    {
        public string Name { get; set; }
        public string Amount { get; set; }
        public string User { get; set; }

        public MonthlyUsage(string name, string amount, string user)
        {
            Name = name;
            Amount = amount;
            User = user;
        }
    }
}

namespace CoffeeEvidenceDesktop.Models
{
    public class Month
    {
        public string Name;
        public int Id;

        public Month(string name, int id)
        {
            Name = name;
            Id = id;
        }

        public override string? ToString()
        {
            return Name;
        }
    }
}

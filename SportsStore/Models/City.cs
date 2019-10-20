namespace SportsStore.Models
{
    public class City
    {
        public string Postalcode { get;  }
        public string Name { get;  }

        public City(string postalcode, string name)
        {
            Postalcode = postalcode;
            Name = name;
        }
    }
}
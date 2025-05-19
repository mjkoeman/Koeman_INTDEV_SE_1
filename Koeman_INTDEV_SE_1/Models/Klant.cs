namespace Koeman_INTDEV_SE_1.Models
{
    public class Klant
    {
        public int KlantId { get; set}
        public string Naam { get; set; }
        public string Adres {get; set; }

        public List<Bestelling> Bestellingen { get; set; }
    }
}

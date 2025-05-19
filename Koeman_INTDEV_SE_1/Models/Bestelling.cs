namespace Koeman_INTDEV_SE_1.Models
{
    public class Bestelling
    {
        public int BestellingId { get; set; }
        public int KlantId { get; set; }   
        public DateTime Besteldatum { get; set; }   
        public decimal Totaalbedrag { get; set; }

        public Klant Klant { get; set; } 
        public List<BestellingProduct> BestellingProducten { get; set; }
    }
}

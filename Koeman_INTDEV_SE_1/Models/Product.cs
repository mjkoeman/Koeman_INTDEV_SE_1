namespace Koeman_INTDEV_SE_1.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Naam { get; set; }    
        public string Beschrijving { get; set; }
        public decimal Prijs { get; set; }

        public List<BestellingProduct> BestellingProducten { get; set; } 
    }
}

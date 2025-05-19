namespace Koeman_INTDEV_SE_1.Models
{
    public class BestellingProduct
    {
        public int BestellingId { get; set; }
        public Bestelling Bestelling { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int Aantal { get; set; }
    }
}

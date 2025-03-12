using CYQ.Data.Orm;

namespace WebApplication1.Entity
{
    public class Commodity 
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colour { get; set; }
        public string Brand { get; set; }
        public double Size { get; set; }
        public double Price { get; set; }
        public double PurchasePrice { get; set; }
        public int PresaleNumber { get; set; }
        public int SalesVolume { get; set; }
        public int Quantity { get; set; }
        public int Stock { get; set; }
    }
}
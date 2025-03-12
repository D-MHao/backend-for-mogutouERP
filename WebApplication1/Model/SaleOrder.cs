using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using CYQ.Data;
using CYQ.Data.Orm;

namespace WebApplication1.Entity
{
    public class SaleOrder
    {
        public string OrderID { get; set; }
        public string CustomorName { get; set; }
        public string Phone { get; set; }
        public string DeliveryAddress { get; set; }
        public string DeliveryCity { get; set; }
        public int Amount { get; set; }
        public int Deposit { get; set; }
        public string Remark { get; set; }
        public int State { get; set; }
        [NotMapped]
        public List<Commodity> Commodities { get; set; } = new();
    }
}
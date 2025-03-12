using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using CYQ.Data;
using CYQ.Data.Table;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        [HttpGet("custormer")]
        public Object getAllCustomerOrder()
        {
            string sql = "select so.*,oc.quantity,commodities.* from sale_order so "+
                         "join order_commodity oc on so.order_id = oc.order_id"+
                         " join commodity commodities on oc.commodity_id = commodities.id";
            using (MAction action = new MAction(sql))
            {
                var mDataTable = action.Select("state < 2");
                var result = mDataTable.ToList<SaleOrder>();

                result = result.GroupBy(p => p.OrderID)
                    .Select(p => p.First())
                    .ToList();

                int listIndex = 0;
                foreach (var row in mDataTable.Rows)
                {
                    var commodity = row.ToEntity<Commodity>();
                    if (row.Get<string>("order_id") != result[listIndex].OrderID) listIndex++;
                    result[listIndex].Commodities.Add(commodity);
                }
                
                return result;
            }
            
        }

        [HttpPost("custormer")]
        public bool postCustormer(SaleOrder saleOrder)
        {
            using (MAction action = new MAction("sale_order"))
            {
                saleOrder.OrderID = "in"+DateTimeOffset.Now.ToUnixTimeSeconds().ToString();
                var type = typeof(SaleOrder);
                var fieldInfos = type.GetProperties();
                foreach (var fieldInfo in fieldInfos)
                {
                    if (fieldInfo.GetCustomAttributes(typeof(NotMappedAttribute), false).Length == 0)
                    {
                        action.Set(fieldInfo.Name, fieldInfo.GetValue(saleOrder));
                    }
                }

                var insert = action.Insert();
                if (!insert)
                {
                    return false;
                }

                using (MAction action2 = new MAction("order_commodity"))
                {
                    var mDataTable = action2.Select("order_id = "+saleOrder.OrderID);
                    foreach (var commodity in saleOrder.Commodities)
                    {
                        MDataRow row = mDataTable.NewRow();
                        row.Set("order_id", saleOrder.OrderID);
                        row.Set("commodity_id", commodity.ID);
                        row.Set("quantity", commodity.Quantity);
                        mDataTable.Rows.Add(row);
                    }
                    mDataTable.AcceptChanges(AcceptOp.Insert);
                    Console.Write(action2.DebugInfo);
                    return true;
                }
            }
        }

        [HttpDelete("custormer/{id}")]
        public bool deleteCustormer(string id)
        {
            using (MAction action = new MAction("sale_order"))
            {
                action.BeginTransation();
                action.Set("order_id", id);
                using (MAction action2 = new MAction("order_commodity"))
                {
                    action2.BeginTransation();
                    action2.Set("order_id", id);
                    var b = action2.Delete();
                    action2.EndTransation();
                }

                var delete = action.Delete();
                Console.Write(action.DebugInfo);
                action.EndTransation();
                return delete;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using CYQ.Data;
using CYQ.Data.Table;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entity;

namespace WebApplication1.Controllers
{
    [ApiController]
    public class CommodityController : ControllerBase
    {
        [HttpGet("commodities")]
        public List<Commodity> GetCommodities()
        {
            using (MAction action = new MAction("Commodity"))
            {
                MDataTable mDataTable = action.Select();
                return mDataTable.ToList<Commodity>();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using PagedList;
using System.Web.Mvc;
using u21487822_HW06.Models;


namespace u21487822_HW06.Controllers
{
    public class ReportController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();
        // GET: Report
        public ActionResult Report()
        {
            Reports reportdata = null;
            // Seclect data in SQL
            var mtBike = from Ord_item in db.order_items
                         join orders in db.orders on Ord_item.order_id equals orders.order_id
                         join pr in db.products on Ord_item.product_id equals pr.product_id
                         join cat in db.categories on pr.category_id equals cat.category_id
                         select new
                         {
                             Ord_item.item_id,
                             Ord_item.order_id,
                             Ord_item.list_price,
                             orders.order_date,
                             cat.category_id
                         };

            reportdata = new Reports
            {
                // Filter the list
                January = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 1 && u.category_id == 6).Count(),
                February = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 2 && u.category_id == 6).Count(),
                March = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 3 && u.category_id == 6).Count(),
                April = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 4 && u.category_id == 6).Count(),
                May = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 5 && u.category_id == 6).Count(),
                June = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 6 && u.category_id == 6).Count(),
                July = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 7 && u.category_id == 6).Count(),
                August = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 8 && u.category_id == 6).Count(),
                September = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 9 && u.category_id == 6).Count(),
                October = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 10 && u.category_id == 6).Count(),
                November = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 11 && u.category_id == 6).Count(),
                December = mtBike.ToList().Where(u => Convert.ToDateTime(u.order_date).Month == 12 && u.category_id == 6).Count()
            };
            // return filter
            return View(reportdata);
        }
    }
}
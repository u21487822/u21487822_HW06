using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using System.Globalization;
using u21487822_HW06.Models;

namespace u21487822_HW06.Controllers
{
    public class OrderController : Controller
    {
        private BikeStoresEntities1 db = new BikeStoresEntities1();
        // Get Orders
        //public async Task<IActionResult> Index(
        //string sortOrder,
        //string searchStringComune,
        //string searchStringProvincia,
        //string searchStringRegione,
        //string searchString,
        //string currentFilter,
        //int? page)
        //{
        //    ViewData["CurrentSort"] = sortOrder;
        //    ViewData["ComuneSortParm"] = String.IsNullOrEmpty(sortOrder) ? "comune" : "";
        //    ViewData["ProvinciaSortParm"] = String.IsNullOrEmpty(sortOrder) ? "provincia" : "";
        //    ViewData["RegioneSortParm"] = String.IsNullOrEmpty(sortOrder) ? "regione" : "";
        //    ViewData["PostalCodeSortParm"] = String.IsNullOrEmpty(sortOrder) ? "CAP" : "";
        //    ViewData["AbitantsSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abitanti" : "";

        //    if (searchStringComune != null ||
        //        searchStringProvincia != null ||
        //        searchStringRegione != null ||
        //        searchString != null)
        //    {
        //        page = 1;
        //        if (searchStringComune != null)
        //        {
        //            searchString = searchStringComune;
        //        }
        //        if (searchStringProvincia != null)
        //        {
        //            searchString = searchStringProvincia;
        //        }
        //        if (searchStringRegione != null)
        //        {
        //            searchString = searchStringRegione;
        //        }
        //    }
        //    else
        //    {
        //        searchString = currentFilter;
        //    }

        //    ViewData["CurrentFilterComune"] = searchStringComune;
        //    ViewData["CurrentFilterProvincia"] = searchStringProvincia;
        //    ViewData["CurrentFilterRegione"] = searchStringRegione;
        //    ViewData["CurrentFilter"] = searchString;

        //    var comuni = from s in _context.Listacomuniitaliani
        //                 select s;

        //    if (!String.IsNullOrEmpty(searchStringComune) ||
        //        !String.IsNullOrEmpty(searchStringProvincia) ||
        //        !String.IsNullOrEmpty(searchStringRegione))
        //    {

        //        comuni = comuni.Where(s => s.Comune.Equals(searchStringComune) ||
        //                                   s.Provincia.Equals(searchStringProvincia) ||
        //                                   s.Regione.Equals(searchStringRegione));
        //    }

        //    switch (sortOrder)
        //    {
        //        case "comune":
        //            comuni = comuni.OrderByDescending(s => s.Comune);
        //            break;
        //        case "provincia":
        //            comuni = comuni.OrderBy(s => s.Provincia);
        //            break;
        //        case "regione":
        //            comuni = comuni.OrderBy(s => s.Regione);
        //            break;
        //        case "CAP":
        //            comuni = comuni.OrderBy(s => s.Cap);
        //            break;
        //        case "abitanti":
        //            comuni = comuni.OrderBy(s => s.Abitanti);
        //            break;
        //        default:
        //            comuni = comuni.OrderBy(s => s.Comune);
        //            break;
        //    }

        //    int pageSize = 7;
        //    return View(await PaginatedList<Listacomuniitaliani>.CreateAsync(comuni.AsNoTracking(), page ?? 1, pageSize));
        //}

        public ViewResult Index(string currentFilter, string searchString, int? page)
        {
            //    if (searchStringComune != null )      
            //    {
            //        page = 1;
            //        if (searchStringComune != null)
            //        {
            //           
            //        }
            //      else
            //    {
            //        searchString = currentFilter;
            //    }
            if (searchString != null)
            {
                //searchString = searchStringComune;
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            IQueryable<order> orders = db.orders;

            if (!String.IsNullOrEmpty(searchString))
            {

                DateTime orderDate = DateTime.ParseExact(searchString, "dd/MM/yyyy", CultureInfo.CurrentUICulture.DateTimeFormat);
                orders = orders.Where(s => s.order_date.Equals(orderDate));
            }

            orders = orders.OrderBy(x => x.order_id);

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            //    return View(await PaginatedList<Listacomuniitaliani>.CreateAsync(page ?? 1, pageSize));
            return View(orders.ToPagedList(pageNumber, pageSize));
        }
    }
}
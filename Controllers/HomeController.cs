using Bookshop_CATeam11.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bookshop_CATeam11.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Promotion()
        {
            BookShopEntities _db = new BookShopEntities();
            var q= _db.Promotions.AsEnumerable().Where(s => s.Start_Time.Date <= DateTime.Now.Date && DateTime.Now.Date <= s.End_Time.Date).ToList();
            
            List<CartItemsViewModel> aa = (from b in _db.Books
                                           join c in _db.Categories on b.CategoryID equals c.CategoryID
                                           select new CartItemsViewModel
                                           {
                                               CategoryName = c.Name,
                                               CategoryID = b.CategoryID,
                                               Cover = b.Cover,
                                               Author = b.Author,
                                               Title = b.Title,
                                               ISBN = b.ISBN,
                                               Price = b.Price,
                                               BookID = b.BookID
                                           }).Take(3).ToList();

            ViewBag.promoBooks = aa;
            return View(q);
        }
        public ActionResult OurStores()
        {
            return View();
        }
        public ActionResult Events()
        {
            return View();
        }
    }
}
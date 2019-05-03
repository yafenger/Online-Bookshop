using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop_CATeam11.Models;

namespace Bookshop_CATeam11.Controllers
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class MaintainPromotionController : Controller
    {
        BookShopEntities db = new BookShopEntities();
        // GET: MaintainPromotion
        
        public ActionResult Index(string sortOrder)
        {
            ViewBag.IDSortParm = String.IsNullOrEmpty(sortOrder) ? "PromotionIDdesc" : "PromotionID";
            ViewBag.DiscountSortParm = sortOrder == "Discount" ? "DiscountDesc" : "Discount";
            ViewBag.StartDateSortParm = sortOrder == "StartDate" ? "StartDateDesc" : "StartDate";
            ViewBag.EndDateSortParm = sortOrder == "EndDate" ? "EndDateAsc" : "EndDate";
            var promotions = from p in db.Promotions
                           select p;
            switch (sortOrder)
            {
                case "PromotionIDdesc":
                    promotions = promotions.OrderByDescending(p => p.PromotionID);
                    break;
                case "Discount":
                    promotions = promotions.OrderBy(p => p.Discount_Price);
                    break;
                case "DiscountDesc":
                    promotions = promotions.OrderByDescending(p => p.Discount_Price);
                    break;
                case "StartDate":
                    promotions = promotions.OrderBy(p => p.Start_Time);
                    break;
                case "StartDateDesc":
                    promotions = promotions.OrderByDescending(p => p.Start_Time);
                    break;
                case "EndDate":
                    promotions = promotions.OrderByDescending(p => p.End_Time);
                    break;
                case "EndDateAsc":
                    promotions = promotions.OrderBy(p => p.End_Time);
                    break;
                default:
                    promotions = promotions.OrderBy(p => p.PromotionID);
                    break;
            }
            return View(promotions.ToList());
        }

        [HttpPost]

        public ActionResult Delete(int id)
        {
            var q = from y in db.Promotions where y.PromotionID == id select y;
            db.Promotions.Remove(q.First());
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            Promotion p = db.Promotions.Single(x => x.PromotionID == id);
            return View(p);
        }

        [HttpPost]
        public ActionResult Edit(Promotion promotion)
        {
            Promotion thisPromo = db.Promotions.Single(x => x.PromotionID == promotion.PromotionID);
            thisPromo.Discount_Price = promotion.Discount_Price;
            thisPromo.Start_Time = promotion.Start_Time;
            thisPromo.End_Time = promotion.End_Time;

            if (ModelState.IsValid)
            {
                db.SaveChanges();
                TempData["Success"] = "Changes Saved!";
            }
            return View(promotion);
        }


        public ActionResult Add()
        {
            Promotion model = new Promotion();
            return View(model);
        }
        
        [HttpPost]

        public ActionResult Add(Promotion p)
        {
            if (ModelState.IsValid)
            {
                db.Promotions.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View(p);
            }
        }
        
    }
}
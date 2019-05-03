using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bookshop_CATeam11.Models;

namespace Bookshop_CATeam11.Controllers
{
    
    public class BookController : Controller
    {
        BookShopEntities _db = new BookShopEntities();
        // private readonly string currentUser = getCurrentUser;//System.Web.HttpContext.Current.User.Identity.Name;
        // GET: Book
        [AllowAnonymous]
        public ActionResult Index()
        {
            ViewBag.CustList = new BusinessLogic().GetBooks;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Show(int? id)  
        {
            if (id == null)    
            {
                ViewBag.BookList = new BusinessLogic().GetBooks;
                return View("Show");
            }
            else
            {
                Book book = new BusinessLogic().GetBook(id);    //book object in details html comes from here
                if (book == null)
                {
                    ViewBag.Id = id;
                    return View("NotBook");
                }
                else
                {
                    ViewBag.Book = book;
                    return View("BookDetails");
                }
            }
        }
        [Authorize]
        public ActionResult CartItems(int? bookid)
        {
            if (bookid > 0)
            {
                if (User.IsInRole("User"))
                {
                    AddToCart(bookid);
                }
                else
                {
                    TempData["info"] = "no access to shopping cart";
                }
                return RedirectToAction("Index");
            }
            else
            {
                List<CartItemsViewModel> carts = (from c in _db.Carts
                                                  join b in _db.Books on c.BookID equals b.BookID
                                                   join m in _db.Memberships on c.MemberID equals m.MemberID
                                                  where m.MemberID == getCurrentUser
                                                  select new CartItemsViewModel
                                                  {
                                                      CartID = c.CartID,
                                                      MemberID = c.MemberID,
                                                      Title = b.Title,
                                                      Cover = b.Cover,
                                                      Price = b.Price,
                                                      ISBN = b.ISBN,
                                                      Author = b.Author,
                                                      Quantity = c.Quantity,
                                                      BookID = b.BookID
                                                  }).ToList();
                if (carts.Count > 0)
                {
                    //   ViewBag.MemberID = currentUser;
                    return View(carts);
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
        }

        [Authorize]
        public ActionResult removeCart(int cartID) {
            Cart cart = _db.Carts.Where(s => s.CartID == cartID && s.MemberID == getCurrentUser).FirstOrDefault();
            if (cart != null)
            {
                _db.Carts.Remove(cart);
                _db.SaveChanges();
            }
            return RedirectToAction("CartItems");
        }

        [Authorize]
        [HttpPost]
        public ActionResult Checkout(List<CartItemsViewModel> viewModel) {
            try
            {
                //List<Sales_Trans> trans = new List<Sales_Trans>();
                //List<Sales_Details> detail = new List<Sales_Details>();
                Promotion pd = _db.Promotions.AsEnumerable().Where(s => s.Start_Time.Date <= DateTime.Now.Date && DateTime.Now.Date <= s.End_Time.Date).FirstOrDefault();

                foreach (CartItemsViewModel item in viewModel)
                {
                    decimal? discountAmt = item.Quantity * item.Price;
                    if (pd != null)
                    {
                        discountAmt = ((100 - pd.Discount_Price) / 100) * discountAmt; 
                    }
                    _db.Sales_Trans.Add(new Sales_Trans {
                        PromotionID = pd != null ? pd.PromotionID : 0,
                        SalesDate = DateTime.UtcNow.Date,
                        Quantity = item.Quantity,
                        Total_Amount = discountAmt,
                        BookID = item.BookID,
                    });
                    Cart cart = _db.Carts.Where(s => s.CartID == item.CartID && s.MemberID == item.MemberID).FirstOrDefault();
                    if (cart != null)
                    {
                        _db.Carts.Remove(cart);
                    }
                }
                _db.SaveChanges();

                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult Search(SearchViewModel vm)//string searchString, string choice
        {
            string searchString = vm.SearchString, choice = vm.Choice;
            List<Book> books = new List<Book>();
            List<Book> bks = new List<Book>();
            var displayedBooks = new BusinessLogic().GetBooks.Take(5);
            ViewBag.randomBooks = displayedBooks;
            if (choice == "Title")
            {
                books = new BusinessLogic().GetBookByTitle(searchString);
                if (books == null)
                {
                    bks = new BusinessLogic().GetBookByTitle(searchString.Substring(0, 1));
                    //ViewBag.booksRelated = bks;
                    return View("ResultsNotFound", bks);
                }
                else
                {
                    //  ViewBag.booklist = books;
                    return View("ResultsFound", books);
                }
            }
            else if (choice == "ISBN")
            {
                books = new BusinessLogic().GetBookByISBN(searchString);
                if (books == null)
                {
                    bks = new BusinessLogic().GetBookByISBN(searchString.Substring(0, 1));
                    //ViewBag.booksRelated = bks;
                    return View("ResultsNotFound", bks);
                }
                else
                {
                    //   ViewBag.booklist = books;
                    return View("ResultsFound", books);
                }
            }
            else
            {
                books = new BusinessLogic().GetBookByAuthor(searchString);
                if (books == null)
                {
                    bks = new BusinessLogic().GetBookByAuthor(searchString.Substring(0, 1));
                    //ViewBag.booksRelated = bks;
                    return View("ResultsNotFound", bks);
                }
                else
                {
                    //  ViewBag.booklist = books;
                    return View("ResultsFound", books);
                }
            }

        }



        public ActionResult ResultsFound(List<Book> books)
        {

            // ViewBag.booklist = books;
            return View(books);
        }
        public ActionResult ResultsNotFound(List<Book> bks)
        {

            // ViewBag.booklist = books;
            return View(bks);
        }

        public ActionResult Children()
        {
            ViewBag.CustList = new BusinessLogic().GetChildrenBooks;
            return View();
        }
        public ActionResult Nonfict()
        {
            ViewBag.CustList = new BusinessLogic().GetNonfictBooks;
            return View();
        }
        public ActionResult Fin()
        {
            ViewBag.CustList = new BusinessLogic().GetFinBooks;
            return View();
        }
        public ActionResult Tech()
        {
            ViewBag.CustList = new BusinessLogic().GetTechBooks;
            return View();
        }
        public ActionResult EditBookInfo()
        {
            return View(_db.Books.ToList());
        }

        [Authorize(Roles ="Admin,SuperAdmin")]
        public ActionResult Edit(int id = 0)
        {


            Book bookModel = new Book();

            BookShopEntities db = new BookShopEntities();
            var getCategoryList = db.Categories.ToList();
            SelectList list = new SelectList(getCategoryList, "CategoryID", "Name");
            ViewBag.CategoryListName = list;

            if (id != 0)
                bookModel = db.Books.Where(x => x.BookID == id).FirstOrDefault();
            bookModel.CategoryCollection = db.Categories.ToList<Category>();




            return View(bookModel);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public ActionResult Edit(Book bk, HttpPostedFileBase image1)
        {
            //_context.Entry<Bookshop_CATeam11.Models.Book>(bk).State = System.Data.Entity.EntityState.Modified;
            BookShopEntities entities = new BookShopEntities();
            Book updatedBook = (from c in entities.Books
                                where c.BookID == bk.BookID
                                select c).FirstOrDefault();
            var getCategoryList = entities.Categories.ToList();
            SelectList list = new SelectList(getCategoryList, "CategoryID", "Name");
            ViewBag.CategoryListName = list;
            updatedBook.CategoryCollection = entities.Categories.ToList<Category>();
            ViewBag.CategoryListName = list;

            if (image1 != null)
            {
                updatedBook.Cover = new byte[image1.ContentLength];
                image1.InputStream.Read(updatedBook.Cover, 0, image1.ContentLength);
            }
            updatedBook.CategoryID = bk.CategoryID;
            updatedBook.Title = bk.Title;
            updatedBook.ISBN = bk.ISBN;
            updatedBook.Author = bk.Author;
            updatedBook.Stock = bk.Stock;
            updatedBook.Price = bk.Price;
            entities.SaveChanges();
            return RedirectToAction("EditBookInfo");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Maintain(int id = 0)
        {
            Book bookModel = new Book();
            BookShopEntities db = new BookShopEntities();
            var getCategoryList = db.Categories.ToList();
            SelectList list = new SelectList(getCategoryList, "CategoryID", "Name");
            ViewBag.CategoryListName = list;

            if (id != 0)
                bookModel = db.Books.Where(x => x.BookID == id).FirstOrDefault();
            bookModel.CategoryCollection = db.Categories.ToList<Category>();


            ViewBag.CategoryListName = list;
            return View(bookModel);
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        [HttpPost]
        public ActionResult Maintain(Book model, HttpPostedFileBase image1)
        {

            var db = new BookShopEntities();

            //model.CategoryID = CategoryList1;
            if (ModelState.IsValid)
            {
                if (image1 != null)
                {
                    model.Cover = new byte[image1.ContentLength];
                    image1.InputStream.Read(model.Cover, 0, image1.ContentLength);
                }

                model.CategoryCollection = db.Categories.ToList<Category>();
                db.Books.Add(model);
                db.SaveChanges();
                ModelState.Clear();

                ViewBag.SuccessMessage = "Successfully Save.";
            }
            // return RedirectToAction("EditBookInfo"); 
            return RedirectToAction("EditBookInfo");
        }

        [Authorize(Roles = "Admin,SuperAdmin")]
        public ActionResult Delete(int id)
        {
            Book bookModel = new Book();
            BookShopEntities entities = new BookShopEntities();
            Book book = (from c in entities.Books where c.BookID == id select c).FirstOrDefault();
            entities.Books.Remove(book);
            entities.SaveChanges();

            // ViewBag.Message = "Record is deleted.";

            return RedirectToAction("EditBookInfo");

        }

        #region helper
        public void AddToCart(int? bookid) {
            Models.Cart cartItem = _db.Carts.SingleOrDefault(c =>
                                 c.MemberID == getCurrentUser && 
                                 c.BookID == bookid);
                if (cartItem == null)
                {
                    cartItem = new Cart
                    {
                        BookID = bookid,
                            MemberID = getCurrentUser,
                        Quantity = 1
                    };

                    _db.Carts.Add(cartItem);
                }
                else
                {
                    cartItem.Quantity++;
                }
                _db.SaveChanges();
        }

        [Authorize]
        public JsonResult CartCount() {
            int res = _db.Carts.Where(s=>s.MemberID==getCurrentUser).Count();
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult checkDiscountPrice() {
            decimal? res = _db.Promotions.AsEnumerable().Where(s => s.Start_Time.Date <= DateTime.Now.Date && DateTime.Now.Date <= s.End_Time.Date).Select(s=>s.Discount_Price).FirstOrDefault();
            return Json(res == null ? 0 : res, JsonRequestBehavior.AllowGet);
        }

        public string getCurrentUser
        {
            get { 
            ApplicationDbContext ctx = new ApplicationDbContext();
        string currentUser = ctx.Users.Where(s => s.UserName == System.Web.HttpContext.Current.User.Identity.Name).Select(s => s.Id).First();
            return currentUser;
            }
        }
        #endregion
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bookshop_CATeam11.Models
{
    public class BusinessLogic
    {
        BookShopEntities m = new BookShopEntities();

        public List<int> GetBookID
        {
            get
            {
                return m.Books.Select
                    (c => c.BookID).ToList();
            }
        }

        public Book GetBook(int? id)
        {
            List<Book> result = m.Books.Where
                              (c => c.BookID==id).ToList<Book>();
            if (result.Count > 0)
                return result[0];
            else
                return null;
        }
        public List<Book> GetBooks
        {
            get
            {
                List<Book> result = m.Books.ToList<Book>();
                if (result.Count > 0)
                    return result;
                else
                    return null;
            }
        }

        public List<Book> GetChildrenBooks
        {
            get
            {
                List<Book> result = m.Books.Where
                                  (c => c.CategoryID.Equals(1)).ToList<Book>();
                if (result.Count > 0)
                    return result;
                else
                    return null;
            }
        }

        public List<Book> GetNonfictBooks
        {
            get
            {
                List<Book> result = m.Books.Where
                                  (c => c.CategoryID.Equals(2)).ToList<Book>();
                if (result.Count > 0)
                    return result;
                else
                    return null;
            }
        }

        public List<Book> GetTechBooks
        {
            get
            {
                List<Book> result = m.Books.Where
                                  (c => c.CategoryID.Equals(3)).ToList<Book>();
                if (result.Count > 0)
                    return result;
                else
                    return null;
            }
        }

        public List<Book> GetFinBooks
        {
            get
            {
                List<Book> result = m.Books.Where
                                  (c => c.CategoryID.Equals(4)).ToList<Book>();
                if (result.Count > 0)
                    return result;
                else
                    return null;
            }
        }

        public List<Book> GetBookByTitle(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                List<Book> books = m.Books.Where(x => x.Title.Contains(searchString)).ToList<Book>();
                if (books.Count > 0)
                    return books;
                else
                    return null;
            }
            else
                return null;
        }
        public List<Book> GetBookByISBN(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                List<Book> books = m.Books.Where(x => x.ISBN.Contains(searchString)).ToList<Book>();
                if (books.Count > 0)
                    return books;
                else
                    return null;
            }
            else
                return null;
        }
        public List<Book> GetBookByAuthor(string searchString)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                List<Book> books = m.Books.Where(x => x.Author.Contains(searchString)).ToList<Book>();
                if (books.Count > 0)
                    return books;
                else
                    return null;
            }
            else
                return null;
        }
    }
}
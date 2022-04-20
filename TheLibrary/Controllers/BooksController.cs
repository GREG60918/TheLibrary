using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using TheLibrary.Data;
using TheLibrary.Models;
using TheLibrary.ViewModels;

namespace TheLibrary.Controllers
{
    public class BooksController : Controller
    {
        private TheLibraryDbContext _theLibraryDbContext;

        public BooksController()
        {
            _theLibraryDbContext = new TheLibraryDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _theLibraryDbContext.Dispose();
        }

        public ViewResult New()
        {
            var genres = _theLibraryDbContext.Genres.ToList();

            var bookFormViewModel = new BookFormViewModel
            {
                Genres = genres
            };

            return View("BookForm", bookFormViewModel);
        }

        public ActionResult Edit(int id)
        {
            var book = _theLibraryDbContext.Books.SingleOrDefault(c => c.Id == id);

            if (book == null)
                return NotFound();

            var bookFormViewModel = new BookFormViewModel(book)
            {
                Genres = _theLibraryDbContext.Genres.ToList()
            };

            return View("BookForm", bookFormViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                var bookFormViewModel = new BookFormViewModel(book)
                {
                    Genres = _theLibraryDbContext.Genres.ToList()
                };

                return View("BookForm", bookFormViewModel);
            }

            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _theLibraryDbContext.Books.Add(book);
            }
            else
            {
                var bookInDb = _theLibraryDbContext.Books.Single(m => m.Id == book.Id);
                bookInDb.Name = book.Name;
                bookInDb.GenreId = book.GenreId;
                bookInDb.NumberInStock = book.NumberInStock;
                bookInDb.ReleaseDate = book.ReleaseDate;
            }

            _theLibraryDbContext.SaveChanges();

            return RedirectToAction("Index", "Books");
        }

        public ViewResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            var customer = _theLibraryDbContext.Books.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();

            return View(customer);
        }

        public IActionResult Random()
        {
            var book = new Book() { Name = "Dictionary" };

            var customers = new List<Customer>
            {
                new Customer { Name="Customer1"},
                new Customer { Name="Customer2"},
            };

            var randomBookViewModel = new RandomBookViewModel
            {
                Book = book,
                Customers = customers,
            };

            return View(randomBookViewModel);
        }
    }
}

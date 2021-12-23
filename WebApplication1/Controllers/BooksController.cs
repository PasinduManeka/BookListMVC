using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BooksController : Controller
    {
        //Create book object with upsert
        [BindProperty]
        public Book Book { get; set; }

        //create a object of the applicationDBContext for databse connection
        private readonly ApplicationDBContext _db;

        //initialize the constructor. Use the dependency injection
        public BooksController(ApplicationDBContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        //UPSERT 
        //parameter can be null(?)
        public IActionResult Upsert(int? id)
        {
            Book = new Book();
            //Check id null or not
            if (id == null)
            {
                //create
                return View(Book);
            }
            //update
            //Populate the book object based on the databse with paseed ID
            Book = _db.Book.FirstOrDefault(u => u.ID == id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book    );
            //Console.WriteLine("ID:",id);

        }

        #region API Calls        
        //Get data
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Book.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //get the book from DB related to the ID
            var bookFromDB = await _db.Book.FirstOrDefaultAsync(u => u.ID == id);

            //null
            if (bookFromDB == null)
            {
                return Json(new { success = false, message = "Error while Deleting" });
            }

            //not null
            _db.Book.Remove(bookFromDB);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Deleted Successfully!!" });

        }
        #endregion
    }
}

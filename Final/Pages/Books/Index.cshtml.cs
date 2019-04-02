using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Final.Pages.Books
{
    public class IndexModel : PageModel
    {
        private readonly Final.Models.FinalContext _context;

        public IndexModel(Final.Models.FinalContext context)
        {
            _context = context;
        }
        
        public IList<Book> Book { get;set; }
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }
        //public int SearchInt { get; set; }
        
        public SelectList Major { get; set; }
        [BindProperty(SupportsGet = true)]
        public string BookMajor { get; set; }

        public async Task OnGetAsync()
        {
            // Use LINQ to get list of major.
            IQueryable<string> majorQuery = from m in _context.Book
                                            orderby m.Major
                                            select m.Major;
            var books = from m in _context.Book
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                books = books.Where(s => s.CourseName.Contains(SearchString));
            }

            if (!string.IsNullOrEmpty(BookMajor))
            {
                books = books.Where(x => x.Major == BookMajor);
            }
            Major = new SelectList(await majorQuery.Distinct().ToListAsync());

            Book = await books.ToListAsync();
        }
    }
}

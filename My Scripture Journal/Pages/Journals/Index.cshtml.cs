using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using My_Scripture_Journal.Data;
using My_Scripture_Journal.Models;

namespace My_Scripture_Journal.Pages.Journals
{
    public class IndexModel : PageModel
    {
        private readonly My_Scripture_Journal.Data.My_Scripture_JournalContext _context;

        public IndexModel(My_Scripture_Journal.Data.My_Scripture_JournalContext context)
        {
            _context = context;
        }

        public string BookSort { get; set; }
        public string DateSort { get; set; }
      

        public IList<Journal> Journal { get;set; } = default!;
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public SelectList? Scriptures { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? JournalScripture { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            var scripture = from m in _context.Journal
                         select m;
            var book = from m in _context.Journal
                            select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                book = book.Where(s => s.Text.Contains(SearchString));
                scripture = scripture.Where(s => s.Book.Contains(SearchString));
            }
            IQueryable<Journal> combinedResults = scripture.Union(book);

            // Sorting logic
            switch (sortOrder)
            {
                case "Date":
                    combinedResults = combinedResults.OrderBy(j => j.DateAdded);
                    break;
                case "Book":
                    combinedResults = combinedResults.OrderBy(j => j.Book);
                    break;
                // Add more cases for additional sorting options if needed
                default:
                    // Default sorting by Date
                    combinedResults = combinedResults.OrderBy(j => j.DateAdded);
                    break;
            }

            Journal = await combinedResults.ToListAsync();


        }
        
    }
}

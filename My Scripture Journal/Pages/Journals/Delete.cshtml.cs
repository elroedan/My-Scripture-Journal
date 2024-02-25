using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using My_Scripture_Journal.Data;
using My_Scripture_Journal.Models;

namespace My_Scripture_Journal.Pages.Journals
{
    public class DeleteModel : PageModel
    {
        private readonly My_Scripture_Journal.Data.My_Scripture_JournalContext _context;

        public DeleteModel(My_Scripture_Journal.Data.My_Scripture_JournalContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Journal Journal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal.FirstOrDefaultAsync(m => m.ID == id);

            if (journal == null)
            {
                return NotFound();
            }
            else
            {
                Journal = journal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var journal = await _context.Journal.FindAsync(id);
            if (journal != null)
            {
                Journal = journal;
                _context.Journal.Remove(Journal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}

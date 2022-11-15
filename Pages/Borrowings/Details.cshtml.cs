using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Kincses_Bianca_Lb2.Data;
using Kincses_Bianca_Lb2.Models;

namespace Kincses_Bianca_Lb2.Pages.Borrowings
{
    public class DetailsModel : PageModel
    {
        private readonly Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context _context;

        public DetailsModel(Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context context)
        {
            _context = context;
        }

      public Borrowing Borrowing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Borrowing == null)
            {
                return NotFound();
            }

            var borrowing = await _context.Borrowing.FirstOrDefaultAsync(m => m.ID == id);
            if (borrowing == null)
            {
                return NotFound();
            }
            else 
            {
                Borrowing = borrowing;
            }
            return Page();
        }
    }
}

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
    public class IndexModel : PageModel
    {
        private readonly Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context _context;

        public IndexModel(Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context context)
        {
            _context = context;
        }

        public IList<Borrowing> Borrowing { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Borrowing != null)
            {
                Borrowing = await _context.Borrowing
                .Include(b => b.Book)
                .ThenInclude(b => b.Author)
                .Include(b => b.Member).ToListAsync();
            }
        }
    }
}

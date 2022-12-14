using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kincses_Bianca_Lb2.Data;
using Kincses_Bianca_Lb2.Models;
//using Kincses_Bianca_Lb2.Models;

namespace Kincses_Bianca_Lb2.Pages.Books
{
    public class EditModel : BookCategoriesPageModel
    {
        private readonly Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context _context;

        public EditModel(Kincses_Bianca_Lb2.Data.Kincses_Bianca_Lb2Context context)
        {
            _context = context;
        }

        [BindProperty]
        public Author Author { get; set; } = default!;
        public Book Book { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }
          Book = await _context.Book
         .Include(b => b.Publisher)
         .Include(b => b.BookCategories).ThenInclude(b => b.Category)
         .AsNoTracking()
         .FirstOrDefaultAsync(m => m.ID == id);


            var book =  await _context.Book.FirstOrDefaultAsync(m => m.ID == id);
            if (book == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Book);

            Book = book;
            var authorList = _context.Author.Select(x => new
            {
                x.ID,
                FullName = x.LastName + " " + x.FirstName
            });
            ViewData["AuthorID"] = new SelectList(authorList, "ID", "FullName");
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID",
            "PublisherName");

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[]
selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var bookToUpdate = await _context.Book
            .Include(i => i.Publisher)
            .Include(i => i.BookCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (bookToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Book>(
            bookToUpdate,
            "Book",
            i => i.Title, i => i.Author,
            i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdateBookCategories(_context, selectedCategories, bookToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdateBookCategories(_context, selectedCategories, bookToUpdate);
            PopulateAssignedCategoryData(_context, bookToUpdate);
            return Page();
        }
    }
   // OnPostAsync()
     //   {
       //     if (!ModelState.IsValid)
         //   {
           //     return Page();
            //}

//            _context.Attach(Book).State = EntityState.Modified;
//
  //          try
    //        {
      //          await _context.SaveChangesAsync();
        //    }
          //  catch (DbUpdateConcurrencyException)
           // {
             //   if (!BookExists(Book.ID))
               // {
                 //   return NotFound();
               // }
               // else
               //{
                //    throw;
               // }
           // }

//            return RedirectToPage("./Index");
  //      }
  //
    //    private bool BookExists(int id)
      //  {
        //  return _context.Book.Any(e => e.ID == id);
       // }
   // }
}

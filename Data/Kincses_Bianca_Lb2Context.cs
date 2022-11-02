using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Kincses_Bianca_Lb2.Models;

namespace Kincses_Bianca_Lb2.Data
{
    public class Kincses_Bianca_Lb2Context : DbContext
    {
        public Kincses_Bianca_Lb2Context (DbContextOptions<Kincses_Bianca_Lb2Context> options)
            : base(options)
        {
        }

        public DbSet<Kincses_Bianca_Lb2.Models.Book> Book { get; set; } = default!;

        public DbSet<Kincses_Bianca_Lb2.Models.Publisher> Publisher { get; set; }

        public DbSet<Kincses_Bianca_Lb2.Models.Author> Author { get; set; }

        public DbSet<Kincses_Bianca_Lb2.Models.Category> Category { get; set; }
    }
}

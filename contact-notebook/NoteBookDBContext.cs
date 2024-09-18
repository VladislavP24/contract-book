using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace contact_notebook;

public class NoteBookDBContext : DbContext
{
    public DbSet<NoteBookModel> notebook => Set<NoteBookModel>();
    public NoteBookDBContext() => Database.EnsureCreated();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "contracts_address_book_db.db");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}

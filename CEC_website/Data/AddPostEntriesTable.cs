using CEC_website.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;


namespace CEC_website.Data
{
    public class AddPostEntriesTable : DbContext
    { 
        public AddPostEntriesTable(DbContextOptions<AddPostEntriesTable> options): base(options)
        {
            
        }


        //this is the data table
        public DbSet<PostEntry> PostDBs { get; set; } 
    }

}

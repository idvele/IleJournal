using IleJournal.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IleJournal.Data
{
    internal class SaveObjectContext :DbContext
    {
        public DbSet<SaveObject> SaveObjects { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-FJGGHA7\MSSQLSERVER01;Initial Catalog=Oppimispaivakirja;Integrated Security=True; Encrypt=False;");
            //guide secure connectioniin https://aka.mas/ef-core-connection-strings
        }
    }
}

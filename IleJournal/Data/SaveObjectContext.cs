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
    }
}

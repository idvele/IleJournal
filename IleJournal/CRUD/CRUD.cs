using IleJournal.Data;
using IleJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IleJournal.CRUD
{
    internal class CRUD
    {
        public static void Insert(string week)
        {
            using SaveObjectContext context = new SaveObjectContext();
            //sisältääkö database kohdeviikon
            
                //ei=luo uusi rivi
                //kyllä= päivitä olemassaoleva rivi
        }

        public static void ReadAll()
        {
            using SaveObjectContext context = new SaveObjectContext();
            var entrys = from SaveObject in context.SaveObjects
                         orderby SaveObject.Week
                         select SaveObject;

        }
        public static string ReadOne(string week)
        {
            using SaveObjectContext context = new SaveObjectContext();
            var entryText = context.SaveObjects
                                .Where(s => s.Week == week)
                                .FirstOrDefault();
            return entryText.Journal_text;
        }
    }
}

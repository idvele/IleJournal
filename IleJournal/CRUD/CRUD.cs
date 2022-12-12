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
        public static void Insert(SaveObject save)
        {
            using SaveObjectContext context = new SaveObjectContext();
            string message = "null";
            //sisältääkö database kohdeviikon
            //ei=luo uusi rivi
            var entryTest = context.SaveObjects
                           .Where(s => s.Week == save.Week)
                           .FirstOrDefault();


            if (entryTest== default)
            {
                context.SaveObjects.Add(save);
                message = "Saved!";
            }
                //kyllä= päivitä olemassaoleva rivi
            else
            {
                var entryText = context.SaveObjects
                                .Where(s => s.Week == save.Week)
                                .FirstOrDefault();
                if(entryText is SaveObject)
                {
                    entryText.Journal_text = save.Journal_text;
                message = "Save updated!";
                }
            }
            context.SaveChanges();
            MessageBox.Show(message);
        }

        public static IList<string> ReadAll()
        {
            using SaveObjectContext context = new SaveObjectContext();
            var entrys = from SaveObject in context.SaveObjects
                         orderby SaveObject.Week
                         select SaveObject;
            
            IList<string> WeekList = new List<string>();
            foreach (SaveObject item in entrys)
            {
                WeekList.Add(item.Week);
            }

            return WeekList;

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

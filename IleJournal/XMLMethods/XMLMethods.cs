using IleJournal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IleJournal.XMLMethods
{
    public class XMLMethods
    {

        public static void XMLSerialize(SaveObject save)
        {
            var xmlSerializer = new XmlSerializer(typeof(SaveObject));
            using (var writer = new StreamWriter(@"C:\Users\ilari\source\repos\IleJournal\Entrys\xmlfilu.xml"))
            {
                xmlSerializer.Serialize(writer, save);
            };
            
        }

        public static SaveObject XMLDeSerialize()
        {
            return new SaveObject();
        }

    }
}

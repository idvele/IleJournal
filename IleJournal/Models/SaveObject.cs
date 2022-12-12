using System.Xml.Serialization;

namespace IleJournal.Models
{

    //Objekti jossa viikko ja tekstit
    [XmlRoot("Oppimispäiväkirja")]
    public class SaveObject
    {
        public Guid Id { get; set; }
        public string? Week { get; set; }
        public string? Journal_text { get; set; }


    }

}//Tsekkaa tää https://www.guru99.com/c-sharp-access-database.html
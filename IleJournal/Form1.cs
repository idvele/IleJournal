using IleJournal.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Windows.Forms;
using IleJournal.CRUD;


namespace IleJournal
{
    public partial class Journal : Form
    {
        //p?iv?m??r? m??ritet?n k?ynnistyksen yhteydess?
            //Get dates
            string week = JournalHelpers.Weekmethod(DateTime.Now.AddDays(7)).ToString();
            string date = DateTime.Today.ToShortDateString();
            string weekday = DateTime.Today.DayOfWeek.ToString();

        
        public Journal()
        {
            InitializeComponent();

        }
        //Mit? tapahtuu aloituksessa
        private void Form1_Load(object sender, EventArgs e)
        {
            //t?h?n XML-deserialisointi
            //get old data if the weekly file exists
            //if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf"))
            //    richTextBox1.LoadFile(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf");
            if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".xml"))
            richTextBox1.Text = XMLMethods.XMLMethods.XMLDeSerialize(week).Journal_text;

            //write timestamp and concatinate if stamp doesn?t exist
            string stamp = TimeStampMethod(week, date, weekday);
            richTextBox1.AppendText(stamp);

            //Move cursor to end
            richTextBox1.Select(richTextBox1.Text.Length - 1, 0);

            //J?rjest? combobox

            OrganizeComboBox();

           

        }

       
        //Tallennusnappula
        private void button1_Click(object sender, EventArgs e)
        {
            //make a saveable object
            SaveObject save = new SaveObject();
            
            save.Id=Guid.NewGuid();
            //Luodaan saveobjectille GUID
            if (WeekBox.Text!= "")
            {
                save.Week = WeekBox.Text;
            }
            else
            {
            save.Week = week;

            }
            //tallennusviikoksi m??ritet??n comboboxin valittu viikko JOS siell? on viikko valittuna
            save.Journal_text = richTextBox1.Text.ToString();
            //Journal Tekstiksi richtextboxin teksti

            //text save-------------------------------

            //T?h?n XML-serialisointi

            XMLMethods.XMLMethods.XMLSerialize(save);
            ////make a path for weeknumber
            //string fileName = save.Week;
            //string fullPath = @"C:\Users\ilari\source\repos\IleJournal\Entrys\" + fileName +".rtf";


            ////save for current weeknumber
            //richTextBox1.SaveFile(fullPath, RichTextBoxStreamType.RichText);

            //Sql save------------------------------


            // Save to Sql

            CRUD.CRUD.Insert(save);
               
        }
       
        //metodi comboboxin osoittaman viikon hakuun
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            richTextBox1.Text=CRUD.CRUD.ReadOne(WeekBox.Text);

        }

     
        //Databaseen yhdist?mismetodi, palauttaa connectionin: obsolete
            static SqlConnection DatabaseConnect()
            {

            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source=DESKTOP-FJGGHA7\MSSQLSERVER01;Initial Catalog=JournalDb;Integrated Security=True; Encrypt=False;";

            cnn= new SqlConnection(connectionString);

            cnn.Open();

                return cnn;
            }
        //Organize combobox method
        private void OrganizeComboBox()
        {
           
            WeekBox.DataSource = CRUD.CRUD.ReadAll();
            //Mik?li viikolle Databasekirjaus, nostetaan viikon jnumero p??limm?iseksi
            try
            {
                WeekBox.SelectedIndex = WeekBox.FindString(week);
            }
            catch (Exception)
            {

                throw;
            }
        }


    
        //Metodi datan hakuun sql:st?: obsolete
        static string DataGet(string week)
            {

            SqlConnection cnn = DatabaseConnect();
            //tehd??n haku databasesta ja printataan se richtextboxiin
            
            string sql = "Select Journal_text from testitable where Week =@SearchWeek;";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@SearchWeek";
            param.Value = week;
            command.Parameters.Add(param);
            
            
            SqlDataReader reader = command.ExecuteReader();
            String Output= "";
            
            while (reader.Read())
            {
                Output += reader.GetValue(0);
            }


            reader.Close();
            command.Dispose();
            cnn.Close();
                return Output;
            }
        //Metodi joka tekee timestampin sen mukaan l?ytyyk? viikolle omaa tiedostoa hakemistosta
        private string TimeStampMethod(string week, string date, string weekday)
        {
                    //Koosta teksti yhdeksi stringiksi
            //Heading m??r?ytyy sen mukaan, onko tekstitedostoa olemassa
            string InitialHeading = "Week number: " + week + "\n \n" + date + " " + weekday + "\n\n" + " ";
            string DailyHeading = "\n------------------------\n" +date + " " + weekday + "\n\n" + " ";

            if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf"))
            {
                if (richTextBox1.Find(date) == -1)
                    return DailyHeading;
                else
                    return " ";
            }
            else
                return InitialHeading;

        }
    }
}//Tsekkaa t?? https://www.guru99.com/c-sharp-access-database.html

// orm entity framework, xml.cs, xml serializer
using IleJournal.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Windows.Forms;

namespace IleJournal
{
    public partial class Journal : Form
    {
        //päivämäärä määritetän käynnistyksen yhteydessä
            //Get dates
            string week = JournalHelpers.Weekmethod(DateTime.Now).ToString();
            string date = DateTime.Today.ToShortDateString();
            string weekday = DateTime.Today.DayOfWeek.ToString();

        
        public Journal()
        {
            InitializeComponent();

        }
        //Mitä tapahtuu aloituksessa
        private void Form1_Load(object sender, EventArgs e)
        {
            //get old data if the weekly file exists
            if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf"))
                richTextBox1.LoadFile(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf");

            //write timestamp and concatinate if stamp doesn´t exist
            string stamp = TimeStampMethod(week, date, weekday);
            richTextBox1.AppendText(stamp);

            //Move cursor to end
            richTextBox1.Select(richTextBox1.Text.Length - 1, 0);

            //Järjestä combobox

            OrganizeComboBox();

            //    WeekBox.Items.Add("yksi");
            //WeekBox.Items.Add("kaksi");

        }

        //null
        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
        //Tallennusnappula
        private void button1_Click(object sender, EventArgs e)
        {
            //make a saveable object
            SaveObject save = new SaveObject();
            //tallennusviikoksi määritetään comboboxin valittu viikko JOS siellä on viikko valittuna
            if (WeekBox.Text!= "")
            {
                save.Week = WeekBox.Text;
            }
            else
            {
            save.Week = week;

            }
            save.Journal_text = richTextBox1.Text.ToString();

            //text save-------------------------------

            //make a path for weeknumber
            string fileName = save.Week;
            string fullPath = @"C:\Users\ilari\source\repos\IleJournal\Entrys\" + fileName +".rtf";


            //save for current weeknumber
            richTextBox1.SaveFile(fullPath, RichTextBoxStreamType.RichText);

            //Sql save------------------------------


            // Save to Sql
            SqlConnection cnn = DatabaseConnect();
            SqlDataAdapter adapter = new SqlDataAdapter();

            //Parametriset SQL-komennot
            //if-logiikka, jos viikkomerkintä on->päivitys jos ei->uusi input
            string CheckSql = "Select * from testitable where Week= '"+save.Week+"'";
            SqlCommand CheckCommand = new SqlCommand(CheckSql, cnn);
            SqlDataReader r = CheckCommand.ExecuteReader();
            string value="a";
            while (r.Read())
            {
                value = r.GetString(0);
            }
            r.Close();
            CheckCommand.Dispose();

            string message = "null";
            if (value=="a")
                //luodaan uusi kirjaus
            {
            
            string InsertSql = ("Insert into dbo.testitable (Week,Journal_text) values('" + save.Week + "','" + save.Journal_text + "')");
            
            //SqlCommand command = new SqlCommand(InsertSql, cnn);

            adapter.InsertCommand = new SqlCommand(InsertSql, cnn);
            adapter.InsertCommand.ExecuteNonQuery();

            //command.Dispose();


             message = "Saved!";
            }
            else
            {
                //päivitetään olemassaoleva kirjaus
                SqlCommand command = new SqlCommand("UPDATE testitable SET Journal_text = '"+save.Journal_text+"' WHERE Week='"+save.Week+"';", cnn);
                adapter.UpdateCommand = command;
                adapter.UpdateCommand.ExecuteNonQuery();

              message = "Save updated";
            }
            
            cnn.Close();
            MessageBox.Show(message);   
        }
       
        //metodi comboboxin osoittaman viikon hakuun
        private void button1_Click_1(object sender, EventArgs e)
        {
            
            richTextBox1.Text=DataGet(WeekBox.Text);

        }

        //comboboxin sisällön vaihto
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string pop = "changed";
            //MessageBox.Show(pop);
        }
        //Databaseen yhdistämismetodi, palauttaa connectionin
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
            using (SqlConnection conn = DatabaseConnect())
            {
                string query = "SELECT week from testitable ORDER BY 'week'";
                SqlDataAdapter da = new SqlDataAdapter(query, conn);

                DataSet ds = new DataSet();
                da.Fill(ds, "week");
                WeekBox.DisplayMember = "week";
                WeekBox.DataSource = ds.Tables["week"];

                try
                {
                    WeekBox.SelectedIndex = WeekBox.FindString(week);
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
        //Metodi datan hakuun sql:stä
        static string DataGet(string week)
            {

            SqlConnection cnn = DatabaseConnect();
            //tehdään haku databasesta ja printataan se richtextboxiin
            
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
        //Metodi joka tekee timestampin sen mukaan löytyykö viikolle omaa tiedostoa hakemistosta
        private string TimeStampMethod(string week, string date, string weekday)
        {
                    //Koosta teksti yhdeksi stringiksi
            //Heading määräytyy sen mukaan, onko tekstitedostoa olemassa
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
}//Tsekkaa tää https://www.guru99.com/c-sharp-access-database.html

// orm entity framework, xml.cs, xml serializer
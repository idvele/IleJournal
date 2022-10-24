using Microsoft.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Windows.Forms;

namespace IleJournal
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();

        }
        //AddDays(1). tester add to date
        private void Form1_Load(object sender, EventArgs e)
        { 
            //Get dates
            string week = JournalHelpers.Weekmethod(DateTime.Now).ToString();
            //string date = DateTime.Today.ToShortDateString();
            string date = DateTime.Today.ToShortDateString();
            string weekday = DateTime.Today.DayOfWeek.ToString();

            //get old data if the weekly file exists
            if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf"))
                richTextBox1.LoadFile(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf");

            //write timestamp and concatinate if stamp doesn¥t exist
            string stamp= TimeStamp( week, date, weekday);
            richTextBox1.AppendText(stamp);

            //Move cursor to end
            richTextBox1.Select(richTextBox1.Text.Length - 1, 0);
        }

        private string TimeStamp(string week, string date, string weekday)
        {
                    //Koosta teksti yhdeksi stringiksi
            //Heading m‰‰r‰ytyy sen mukaan, onko tekstitedostoa olemassa
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

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            //make a path for weeknumber
            string fileName = JournalHelpers.Weekmethod(DateTime.Now).ToString();
            string fullPath = @"C:\Users\ilari\source\repos\IleJournal\Entrys\" + fileName +".rtf";

            //save for current weeknumber
            richTextBox1.SaveFile(fullPath, RichTextBoxStreamType.RichText);

            string message = "Saved!";
            MessageBox.Show(message);
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string connectionString;
            SqlConnection cnn;

            connectionString = @"Data Source=DESKTOP-FJGGHA7\MSSQLSERVER01;Initial Catalog=JournalDb;Integrated Security=True;";

            cnn= new SqlConnection(connectionString);

            cnn.Open();
            //tehd‰‰n haku databasesta ja printataan se richtextboxiin
            
            string sql = "Select Journal_text from testitable";
            SqlCommand command = new SqlCommand(sql, cnn);
            SqlDataReader reader = command.ExecuteReader();
            String Output = "";

            while (reader.Read())
            {
                Output = Output + reader.GetValue(0);
            }


            richTextBox1.Text=Output;
            cnn.Close();
        }
    }
}//Tsekkaa t‰‰ https://www.guru99.com/c-sharp-access-database.html
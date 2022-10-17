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

        private void Form1_Load(object sender, EventArgs e)
        { 
            //Get dates
            string week = JournalHelpers.Weekmethod(DateTime.Now).ToString();
            //string date = DateTime.Today.ToShortDateString();
            string date = DateTime.Today.AddDays(1).ToShortDateString();
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
            string InitialHeading = "Week number: " + week + "\n \n" + date + " " + weekday + "\n" + " ";
            string DailyHeading = "\n------------------------\n" +date + " " + weekday + "\n" + " ";

            if (File.Exists(@"C:\Users\ilari\source\repos\IleJournal\Entrys\" + week + ".rtf"))
            {
                if (richTextBox1.Find(DailyHeading) != -1)
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

            richTextBox1.SaveFile(fullPath, RichTextBoxStreamType.RichText);

            string message = "Saved!";
            MessageBox.Show(message);
        }



    }
}
using System.Globalization;
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
            //Insert timestamp to note
            DateTime time = new DateTime();
            time = DateTime.Now;
            string week = Weekmethod(time).ToString();
            string date = DateTime.Today.ToShortDateString();
            string weekday = DateTime.Today.DayOfWeek.ToString();

            //Koosta teksti yhdeksi stringiksi

            string Heading= "Week number: "+week + "\n \n" + date +" " + weekday+"\n" +" ";

            richTextBox1.Text = Heading;
            richTextBox1.Select(richTextBox1.Text.Length - 1, 0);

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string message = "det wörkkar";
            MessageBox.Show(message);

        }
    public static int Weekmethod (DateTime time)
    {
        // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
        // be the same week# as whatever Thursday, Friday or Saturday are,
        // and we always get those right
        DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
        if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }

        // Return the week of our adjusted day
        return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    // This presumes that weeks start with Monday.
    // Week 1 is the 1st week of the year with a Thursday in it.
    }
    }

}
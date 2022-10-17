using System.Globalization;
using System.Net.Security;

namespace IleJournal
{
    public partial class Journal : Form
    {
        public Journal()
        {
            InitializeComponent();

            //Insert timestamp to note
            DateTime time = new DateTime();
            time = DateTime.Now;
            int week = Weekmethod(time);
            string date = DateTime.Today.ToString();

            //Koosta teksti yhdeksi stringiksi

            string Heading= 

            richTextBox1.Text = "Journal";
            richTextBox1.Text = week.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

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
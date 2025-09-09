/* @author Gurpreet Singh
 * @verizon final
 * @Description This app provides the user with a way to calculate the loan for residential or commercial real estate 
 */




namespace LoanCalculatorApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            chart1.Series["s1"].Points.Clear(); //lines 19-20 are used to clear the piechart making sure its empty before adding data
            chart1.Titles.Clear();

            double loanAmount = 0;
            double loanRate = 0;
            int loanTerm = 0;
            try //I use a try and catch to make sure that the data entered into the textbox are correct and if not - a Messagebox appears
            {
                loanAmount = double.Parse(LoanAmountBox.Text); //lines 27-29 get the data from the textboxes
                loanRate = double.Parse(LoanRateBox.Text);
                loanTerm = int.Parse(LoanTermBox.Text);
            }
            catch
            {
                MessageBox.Show("Error - incorrect input or inputs");
            }


            double monthlyInterest = (loanAmount * ((loanRate / 100) / 12));  //lines 37-39 are the equation to figure out how to calculate the loan
            double exponentBase = 1 + ((loanRate / 100) / 12);
            double monthlyPayment = (monthlyInterest) / (1 - Math.Pow(exponentBase, (-12 * loanTerm)));

            //MessageBox.Show(loanAmount + " " + loanRate.ToString("F2") + " " + loanTerm);     // lines 41-42 is used to test the application by developer
            //MessageBox.Show(monthlyInterest.ToString("F2") + " " + monthlyPayment.ToString("F2"));

            List<Schedule> schedules = new List<Schedule>(); //creates a new List to store the Schedule Objects so we can load data in datagridview1 later
            double startBal = loanAmount;
            double totalInterest = 0;
            double totalPayments = 0;
            DateTime date = dateTimePicker1.Value.Date; 
            //MessageBox.Show(date.ToString("MM/dd/yyyy")); // Test code used by developer

            for (int i = 0; i < (loanTerm * 12); i++)   //for loop to go through all the terms to calculate the loan such as interest, principal and balances
            {
                int month = i + 1;
                double interest = (startBal * ((loanRate / 100) / 12));
                double principal = monthlyPayment - interest;
                double endBal = startBal - principal;

                Schedule s = new Schedule
                {
                    s_month = month,
                    date = date.AddMonths(i),
                    s_startBal = startBal,
                    s_interest = interest,
                    s_principal = principal,
                    s_endBal = endBal
                };

                startBal = s.s_endBal;
                totalInterest += interest;
                totalPayments += interest + principal;
                //MessageBox.Show(s.ToString());
                schedules.Add(s);
            }

            dataGridView1.DefaultCellStyle.Format = "N2";     //Lines 75 through 85 is formatting how the datagridview1 will make the data look like
            dataGridView1.DefaultCellStyle.Font = new Font("Arial", 12); 
            dataGridView1.DataSource = schedules;
            dataGridView1.Columns[1].DefaultCellStyle.Format = "MM/dd/yy";

            dataGridView1.Columns[0].HeaderText = "Month #";
            dataGridView1.Columns[1].HeaderText = "Date";
            dataGridView1.Columns[2].HeaderText = "Balance";
            dataGridView1.Columns[3].HeaderText = "Interest";
            dataGridView1.Columns[4].HeaderText = "Principal";
            dataGridView1.Columns[5].HeaderText = "End Balance";

            label6.Text = monthlyPayment.ToString("N2"); 

            label7.Text = totalInterest.ToString("N2");

            label9.Text = totalPayments.ToString("N2");

            chart1.Titles.Add("Pie Chart");

            double interestPercentage = (totalInterest/ totalPayments);
            double principalPercentage = (1 - interestPercentage);

            chart1.Series["s1"].IsValueShownAsLabel = true;    //lines 98-102 are adding the interest, principal percentage into the pie chart
            chart1.Series["s1"].Points.AddXY("Interest", Math.Round((interestPercentage*100), 0));
            chart1.Series["s1"].Points.AddXY("Principal", Math.Round((principalPercentage*100), 0));
            chart1.Series["s1"].Points[0].Color = Color.Crimson;
            chart1.Series["s1"].Points[1].Color = Color.ForestGreen;
        }
    }
}
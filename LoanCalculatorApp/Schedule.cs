using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoanCalculatorApp
{
    internal class Schedule
    {
        public int s_month { get; set; }
        public DateTime date { get; set; }
        public double s_startBal { get; set; }
        public double s_interest { get; set; }
        public double s_principal { get; set; }
        public double s_endBal { get; set; }

        public override string ToString()
        {
            return s_month.ToString() + " " + s_startBal.ToString("F2") + " " + s_interest.ToString("F2") + " " + s_principal.ToString("F2") + " " + s_endBal.ToString("F2");
        }

    }
}

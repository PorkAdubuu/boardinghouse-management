using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_messaging
{
    public partial class reports_analytics : Form
    {
        public reports_analytics()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(new string[] {
                "January", "February", "March", "April", "May", "June",
                "July", "August", "September", "October", "November", "December"
            });
            for (int i = 2015; i <= 2024; i++)
            {
                comboBox3.Items.Add(i.ToString());
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDaysInComboBox2();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateDaysInComboBox2();
        }
        private void UpdateDaysInComboBox2()
        {
            comboBox2.Items.Clear(); // Clear the days in comboBox2

            // Get the selected month
            string selectedMonth = comboBox1.SelectedItem?.ToString();
            string selectedYear = comboBox3.SelectedItem?.ToString(); // Get the selected year

            if (selectedMonth != null && selectedYear != null)
            {
                int daysInMonth = GetDaysInMonth(selectedMonth, int.Parse(selectedYear));

                // Add the days to comboBox2
                for (int i = 1; i <= daysInMonth; i++)
                {
                    comboBox2.Items.Add(i);
                }

                // Optionally, select the first day
                if (comboBox2.Items.Count > 0)
                {
                    comboBox2.SelectedIndex = 0;
                }
            }
        }

        private int GetDaysInMonth(string month, int year)
        {
            // Days in months (non-leap year)
            switch (month)
            {
                case "January":
                case "March":
                case "May":
                case "July":
                case "August":
                case "October":
                case "December":
                    return 31;
                case "April":
                case "June":
                case "September":
                case "November":
                    return 30;
                case "February":
                    return IsLeapYear(year) ? 29 : 28; // Adjust for leap year
                default:
                    return 0;
            }
        }

        private bool IsLeapYear(int year)
        {
            // A leap year is divisible by 4, except for years divisible by 100 unless divisible by 400
            return (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0));
        }


        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void visualchart_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
    }
}
    


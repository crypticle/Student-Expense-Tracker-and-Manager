using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        public class Account
        {
            public String accountName;
            public decimal balance;


            public Account(string accountName, decimal balance)
            {
                this.accountName = accountName;
                this.balance = balance;
            }

            public void setBalance(decimal value, bool isIncome)
            {
                if (isIncome)
                {
                    balance = balance + value;
                }
                else
                {
                    balance = balance - value;
                }
            }

        }

        private DataTable RemoveDuplicatesRecords(DataTable dt)
        {
            var UniqueRows = dt.AsEnumerable().Distinct(DataRowComparer.Default);
            DataTable dt2 = UniqueRows.CopyToDataTable();
            return dt2;
        }

        List<Panel> listPanel = new List<Panel>();
        List<String> expenseCategory = new List<String>();
        List<String> incomeType = new List<String>();
        List<Account> accounts = new List<Account>();
        DataTable incomeTable = new DataTable();
        DataTable expenseTable = new DataTable();
        DataTable dashboardTable = new DataTable();
        decimal totalBalance;


        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
              int nHeightEllipse
          );


        public Form1()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = BtnDashboard.Height;
            pnlNav.Top = BtnDashboard.Top;
            pnlNav.Left = BtnDashboard.Left;

            //incomeDateTimePicker.Value = DateTime.Now;
            //expenseDateTimePicker.Value = DateTime.Now;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            listPanel.Add(dashboardPanel);
            listPanel.Add(incomePan);
            listPanel.Add(expensePanel);
            listPanel.Add(settingPanel);
            listPanel[0].BringToFront();
            dashboardTable.DefaultView.ToTable( /*distinct*/ true);

            incomeTable.Columns.Add("Account", typeof(String));
            incomeTable.Columns.Add("Category", typeof(String));
            incomeTable.Columns.Add("Date", typeof(String));
            incomeTable.Columns.Add("Value", typeof(decimal));

            expenseTable.Columns.Add("Account", typeof(String));
            expenseTable.Columns.Add("Category", typeof(String));
            expenseTable.Columns.Add("Date", typeof(String));
            expenseTable.Columns.Add("Value", typeof(decimal));

            incomeDataGridView.DataSource = incomeTable;
            expenseDataGridView.DataSource = expenseTable;



            //dashboardDataGridView.DataSource = dashboardTable;



        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnExpenses.Height;
            pnlNav.Top = btnExpenses.Top;
            btnExpenses.BackColor = Color.FromArgb(46, 51, 73);
            listPanel[2].BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnDashboard_Click(object sender, EventArgs e)
        {
            pnlNav.Height = BtnDashboard.Height;
            pnlNav.Top = BtnDashboard.Top;
            pnlNav.Left = BtnDashboard.Left;
            BtnDashboard.BackColor = Color.FromArgb(46, 51, 73);
            listPanel[0].BringToFront();

        }

        private void btnIncome_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnIncome.Height;
            pnlNav.Top = btnIncome.Top;
            btnIncome.BackColor = Color.FromArgb(46, 51, 73);
            listPanel[1].BringToFront();
            dashboardDataGridView.BringToFront();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            pnlNav.Height = btnSettings.Height;
            pnlNav.Top = btnSettings.Top;
            btnSettings.BackColor = Color.FromArgb(46, 51, 73);
            listPanel[3].BringToFront();
        }

        private void BtnDashboard_Leave(object sender, EventArgs e)
        {
            BtnDashboard.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnIncome_Leave(object sender, EventArgs e)
        {
            btnIncome.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnExpenses_Leave(object sender, EventArgs e)
        {
            btnExpenses.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnSettings_Leave(object sender, EventArgs e)
        {
            btnSettings.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label6_Click_1(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void accountNameField_TextChanged(object sender, EventArgs e)
        {

        }

        private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
        {

        }

        private void label23_Click(object sender, EventArgs e)
        {

        }

        private void expensePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void accountSubmitBtn_Click(object sender, EventArgs e)
        {
            Decimal value = -1;
            if (decimal.TryParse(startingBalanceField.Text, out value))
            {
                accounts.Add(new Account(accountNameField.Text, value));
                if (!incomeAccountDropDown.Items.Contains(accountNameField.Text))
                {
                    incomeAccountDropDown.Items.Add(accountNameField.Text);
                    expenseAccountComboBox.Items.Add(accountNameField.Text);
                }

            }
            else
            {
                MessageBox.Show("Input Error", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void incomeAccountDropDown_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void expenseCategorySubmitBtn_Click(object sender, EventArgs e)
        {
            if (!expenseCategoryComboBox.Items.Contains(addExpenseTextBox.Text))
                expenseCategoryComboBox.Items.Add(addExpenseTextBox.Text);
        }

        private void incomeTypeSubmitBtn_Click(object sender, EventArgs e)
        {
            if (!incomeCategoryDropDown.Items.Contains(incomeTypeTextBox.Text))
                incomeCategoryDropDown.Items.Add(incomeTypeTextBox.Text);
        }

        private void incomePan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void incomeDataGridView_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dashboardPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void incomeInsertRowBtn_Click(object sender, EventArgs e)
        {
            incomeTable.Rows.Add(incomeAccountDropDown.SelectedItem, incomeCategoryDropDown.SelectedItem, incomeDateTimePicker.Value, decimal.Parse(incomeValueTextBox.Text));
            dashboardTable.Merge(incomeTable, false, MissingSchemaAction.Add);
            dashboardDataGridView.DataSource = RemoveDuplicatesRecords(dashboardTable);

        }

        private void dataGridView2_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void expenseInsertRowBtn_Click(object sender, EventArgs e)
        {
            expenseTable.Rows.Add(expenseAccountComboBox.SelectedItem, expenseCategoryComboBox.SelectedItem, expenseDateTimePicker.Value, decimal.Parse(expenseValueTextBox.Text));
            dashboardTable.Merge(expenseTable, false, MissingSchemaAction.Add);
            dashboardDataGridView.DataSource = RemoveDuplicatesRecords(dashboardTable);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            incomeDateTimePicker.Value = DateTime.Now;
            expenseDateTimePicker.Value = DateTime.Now;
        }
    }
}

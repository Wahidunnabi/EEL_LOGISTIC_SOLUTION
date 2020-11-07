using LOGISTIC.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LOGISTIC.UI.Report.Accounts
{
    public partial class AccountsVoucherReport : Form
    {
        private List<Employee> m_employees;
        public AccountsVoucherReport()
        {
            InitializeComponent();
        }

        private void AccountsVoucherReport_Load(object sender, EventArgs e)
        {

          



        }

        public List<Employee> GetEmployees()

        {
            return m_employees;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_employees = new List<Employee>();

            m_employees.Add(new Employee("Mahesh Chand", "112 New Road, Chadds Ford, PA", "123-21-1212", 30));

            m_employees.Add(new Employee("Jack Mohita", "Pear Lane, New York 23231", "878-12-2334", 23));

            m_employees.Add(new Employee("Renee Singer", "Near medow, Philadelphia, PA", "980-00-2320", 20));

            this.reportViewer1.RefreshReport();

            EmployeeBindingSource.DataSource = GetEmployees();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //AccountsVoucherBLL objBll = new AccountsVoucherBLL();
            //List<AccountsVoucher> list = new List<AccountsVoucher>();
            //list = objBll.GetAccountsVoucher();
        }
    }


    public class Employee

    {

        private string name;



        public string Name

        {

            get { return name; }

            set { name = value; }

        }

        private string address;



        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string ssn;
        public string Ssn
        {

            get { return ssn; }

            set { ssn = value; }

        }
        private Int16 age;
        public Int16 Age

        {

            get { return age; }

            set { age = value; }

        }



        public Employee(string EmpName, string EmpAddress, string EmpSsn, Int16 EmpAge)

        {
            this.name = EmpName;
            this.address = EmpAddress;
            this.ssn = EmpSsn;
            this.age = EmpAge;

        }

    }
}


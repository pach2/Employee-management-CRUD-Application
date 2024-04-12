using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUD
{
    public partial class Form1 : Form
    {
        Employee emp = new Employee();
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource= emp.GetEmployeeTable();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            emp.EmpId = textBox1.Text;
            emp.EmpName = textBox2.Text;
            emp.Age = textBox3.Text;
            emp.Contact=textBox4.Text;
            emp.Gender= comboBox1.SelectedItem.ToString();

            var success = emp.InsertEmployee(emp);
            dataGridView1.DataSource = emp.GetEmployeeTable();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been added successfully");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            emp.EmpId = textBox1.Text;
            emp.EmpName = textBox2.Text;
            emp.Age = textBox3.Text;
            emp.Contact = textBox4.Text;
            emp.Gender = comboBox1.SelectedItem.ToString();

            var success = emp.UpdateEmployee(emp);
            dataGridView1.DataSource = emp.GetEmployeeTable();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been Updated successfully");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            emp.EmpId = textBox1.Text;

            var success = emp.DeleteEmployee(emp);
            dataGridView1.DataSource = emp.GetEmployeeTable();

            if (success)
            {
                ClearControls();
                MessageBox.Show("Employee has been Deleted successfully");
            }
            else
            {
                MessageBox.Show("Error");
            }
        }


        private void ClearControls()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text="";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ClearControls();
        }
    }

    public class Employee
    {
        private static string conn = "Data Source=DESKTOP-7JQPFQU;Initial Catalog=Connection;Integrated Security=True";

        public string EmpId { get; set; }
        public string EmpName { get; set; }
        public string Age { get; set; }
        public string Contact { get; set; }
        public string Gender { get; set; }

        private const string InsertQuery = "Insert into EmployeeInfo(EmpId,EmpName,EmpAge,EmpContact,EmpGender) values (@EmpId,@EmpName,@EmpAge,@EmpContact,@EmpGender)";
        private const string UpdateQuery = "Update EmployeeInfo set EmpName=@EmpName, EmpAge=@EmpAge,EmpContact=@EmpContact, EmpGender=@EmpGender where EmpId=@EmpId";
        private const string DeleteQuery = "Delete from EmployeeInfo where EmpId=@EmpId";
        private const string SelectQuery = "Select * from EmployeeInfo";

        public bool InsertEmployee(Employee emp)
        {
            int rows;
            using(SqlConnection con=new SqlConnection(conn))
            {
                con.Open();
                using(SqlCommand cmd=new SqlCommand(InsertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@EmpId",emp.EmpId);
                    cmd.Parameters.AddWithValue("@EmpName", emp.EmpName);
                    cmd.Parameters.AddWithValue("@EmpAge", emp.Age);
                    cmd.Parameters.AddWithValue("@EmpContact", emp.Contact);
                    cmd.Parameters.AddWithValue("@EmpGender", emp.Gender);
                    rows= cmd.ExecuteNonQuery();
                }
            }
            return (rows > 0) ? true : false;

        }

        public bool UpdateEmployee(Employee emp)
        {
            int rows;
            using(SqlConnection con=new SqlConnection(conn)) 
            { 
                con.Open();
                using(SqlCommand cmd=new SqlCommand(UpdateQuery, con))
                {
                    cmd.Parameters.AddWithValue("@EmpName",emp.EmpName);
                    cmd.Parameters.AddWithValue("@EmpAge", emp.Age);
                    cmd.Parameters.AddWithValue("@EmpContact", emp.Contact);
                    cmd.Parameters.AddWithValue("@EmpGender", emp.Gender);
                    cmd.Parameters.AddWithValue("@EmpId", emp.EmpId);
                    rows=cmd.ExecuteNonQuery();
                }
            }
            return (rows>0)? true : false;
        }

        public bool DeleteEmployee(Employee emp)
        {
            int rows;
            using(SqlConnection con=new SqlConnection(conn))
            {
                con.Open();
                using(SqlCommand cmd=new SqlCommand(DeleteQuery, con))
                {
                    cmd.Parameters.AddWithValue("EmpId", emp.EmpId);
                    rows = cmd.ExecuteNonQuery();
                }
            }
            return(rows>0)?true:false;
        }

        public DataTable GetEmployeeTable()
        {
            var dt=new DataTable();
            using(SqlConnection con=new SqlConnection(conn)) 
            {
                using(SqlCommand cmd=new SqlCommand(SelectQuery,con))
                {
                    using(SqlDataAdapter ad=new SqlDataAdapter(cmd)) 
                    {
                        ad.Fill(dt);
                    }
                }
            }
            return dt;
        }

    }
}

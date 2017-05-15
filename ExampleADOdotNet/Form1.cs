using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExampleADOdotNet
{
    public partial class Form1 : Form
    {
        SqlConnection con; SqlCommand cmd; SqlDataReader dr; string SQLstr;

        public Form1()
        {
            InitializeComponent();
            con = new SqlConnection("User ID=sa;Password=123;Database=DimX;Data Source=.");
            cmd = new SqlCommand();
            cmd.Connection = con;
            con.Open();
            LoadData();
        }

        private void LoadData()
        {
            cmd.CommandText = "select Eno ,Ename,Job,Salary from Employee Order by Eno";
            dr = cmd.ExecuteReader();
            ShowData();
        }

        private void ShowData()
        {
            if (dr.Read())
            {
                txtbxEmpno.Text = dr[0].ToString();             // first column
                txtbxEname.Text = dr[1].ToString();             // second column
                txtbxJob.Text = dr[2].ToString();               // Third Column
                txtbxSalary.Text = dr[3].ToString();            // Fourth column
            }
            else
            {
                MessageBox.Show("No data Exists");
            }
        }

        private void button1_Click(object sender, EventArgs e)   // Next button
        {
            ShowData();
        }

        private void button2_Click(object sender, EventArgs e)   // New Button
        {
            txtbxEmpno.Text = txtbxEname.Text = txtbxJob.Text = txtbxSalary.Text = "";
            dr.Close();
            cmd.CommandText = "Select IsNull(Max(Eno),1000)+1 from Employee";
            txtbxEmpno.Text = cmd.ExecuteScalar().ToString();
            button3.Enabled = true;
            txtbxEname.Focus();
        }

        private void button6_Click(object sender, EventArgs e)     // Close Button
        {
            if (con.State != ConnectionState.Closed)
            {
                con.Close();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)   //insert button
        {
            SQLstr = "Insert Into Employee(Eno,Ename,Job,Salary) Values(" + txtbxEmpno.Text + "," + txtbxEname.Text + "," + txtbxJob.Text + "," + txtbxSalary.Text + ")";
            ExecuteDML();

            button3.Enabled = false;
        }

        private void ExecuteDML()
        {
            DialogResult d = MessageBox.Show("Are you want to sure of Executing the below SQL statement?\n\n"
                + SQLstr, "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (d == DialogResult.Yes)
            {
                cmd.CommandText = SQLstr;
                dr.Close();
                int count = cmd.ExecuteNonQuery();
                if (count > 0)
                {
                    MessageBox.Show("StateMent Executed Succesfully.");
                }
                else
                {
                    MessageBox.Show("Staement Executed failed.");
                }
                LoadData();
            }

        }

        private void button5_Click(object sender, EventArgs e)   //update Button
        {
            //SQLstr = "UPdate Employee set Ename=" + txtbxEname.Text + ",Job=" + txtbxJob.Text
            //    + ",Salary" + txtbxSalary.Text + "Where Eno=" + txtbxEmpno.Text;

            SQLstr = String.Format("Update Employee set Ename = {0}, Job = {1} , Salary = {2} where Eno = {3}", txtbxEmpno);

            dr.Close();
            ExecuteDML();

        }

        private void button4_Click(object sender, EventArgs e)    //Delete Button
        {
            SQLstr = String.Format("Delete from Employee where Eno={0}", txtbxEmpno.Text);
            dr.Close();
            ExecuteDML();
        }
    }
}

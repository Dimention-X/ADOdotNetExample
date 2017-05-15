using System;
using System.Data.OleDb;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADOdotNetExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OleDbDataReader dr;
        OleDbConnection con;
        OleDbDataAdapter ad;
        private void button1_Click(object sender, EventArgs e)
        {
            con = new OleDbConnection("Provider=SQLOledb;Data Source=.;User Id=sa;Password=123;db=DimX");
            OleDbCommand cmd = new OleDbCommand("Select * from Department_Tbl where DeptNo=2", con);
            //{
            //    con.Open();
            //    MessageBox.Show(con.State == ConnectionState.Open ? "Copen" : "Cclose");
            //    con.Close();
            //    MessageBox.Show(con.State == ConnectionState.Closed ? "Closed" : "COpen");
            //}
            
            Department obj = new Department();
            con.Open();
            ad = new OleDbDataAdapter();

            //DataSet ds = new DataSet();
            //ad.Fill(ds, "Department_Tbl");
            //con.Close();

            dr = cmd.ExecuteReader();
        
            textBox1.Text = dr.GetName(0);
            textBox2.Text = dr.GetName(1);
            textBox3.Text = dr.GetName(2);
            ShowData();
        }

        private void ShowData()
        {
            if(dr.Read())
            {
                textBox1.Text = dr.GetValue(0).ToString();
                textBox2.Text = dr.GetValue(1).ToString();
                textBox3.Text = dr.GetValue(2).ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (con.State != ConnectionState.Closed) { con.Close(); }
            this.Close();
        }
    }
}

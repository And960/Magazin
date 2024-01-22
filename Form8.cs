using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazin
{
    public partial class Form8 : Form
    {
        public string conString = Form1.conString;
        public int max;
        public Form8()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 Form = new Form4(null);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form9 Form = new Form9(null);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form10 Form = new Form10(null);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            max = 0;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string sql = "SELECT MAX([NDok]) FROM Sale";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            max = int.Parse(reader.GetValue(0).ToString());
                        }

                        con.Close();
                    }
                }
            }
            Form6 Form = new Form6(max+1);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            max = 0;
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string sql = "SELECT MAX([NDok]) FROM Purchases";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            max= int.Parse(reader.GetValue(0).ToString());
                        }

                        con.Close();
                    }
                }
            }
            Form12 Form = new Form12(max+1);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form13 Form = new Form13();
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Form14 Form = new Form14();
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Form15 Form = new Form15();
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Form7 Form = new Form7("Firma", null);
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }
    }
}

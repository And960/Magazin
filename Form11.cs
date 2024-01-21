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
    public partial class Form11 : Form
    {
        static string zapros;
        public string Indf;
        public string Str1;
        public string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";

        public Form11(string str1)
        {
            InitializeComponent();

            if (str1 != null)
            {
                Str1 = str1;
                button1.Visible = false;
                OpenFirm();
            }
            else
            {
                button2.Visible = false;
            }
        }

        private void OpenFirm()
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                if (Str1 != null)
                {
                    zapros = " Where BuyerId = '" + int.Parse(Str1) + "'";
                }
                string sql = "Select * From Buyer" + zapros;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox1.Text = reader.GetValue(1).ToString();
                            textBox5.Text = reader.GetValue(2).ToString();
                            textBox6.Text = reader.GetValue(3).ToString();
                            textBox2.Text = reader.GetValue(4).ToString();
                            textBox3.Text = reader.GetValue(5).ToString();
                            textBox4.Text = reader.GetValue(0).ToString();
                        }

                        con.Close();
                    }
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update Buyer set LastName=@LastName,FirstName=@FirstName,Name=@Name,Phone=@Phone,Adress=@Adress where BuyerId=@BuyerId", con);
            con.Open();
            cmd.Parameters.AddWithValue("@BuyerId", int.Parse(textBox4.Text));
            cmd.Parameters.AddWithValue("@LastName", textBox1.Text.Replace(" ", ""));
            cmd.Parameters.AddWithValue("@FirstName", textBox5.Text.Replace(" ", ""));
            cmd.Parameters.AddWithValue("@Name", textBox6.Text.Replace(" ", ""));
            cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@Adress", textBox3.Text.Replace(" ", ""));
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Запись обновлена");
            con.Close();
            DialogResult = DialogResult.OK;
            Close();
        }

        public void SaveFirm()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("insert into Buyer (LastName,FirstName,Name,Phone,Adress) values(@LastName,@FirstName,@Name,@Phone,@Adress)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@LastName", textBox1.Text);
            cmd.Parameters.AddWithValue("@FirstName", textBox5.Text);
            cmd.Parameters.AddWithValue("@Name", textBox6.Text);
            cmd.Parameters.AddWithValue("@Phone", textBox2.Text);
            cmd.Parameters.AddWithValue("@Adress", textBox3.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Запись добавлена");
            con.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SaveFirm();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox2.Text, out double parsedNumber))
            { }
            else
            {
                textBox2.Text = "";
                MessageBox.Show("Используйте только цифры !");
            }
        }
    }
}

using MySql.Data.MySqlClient;
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
    public partial class Form7 : Form
    {
        static string zapros;
        public string Indf;
        public string Str1;
        public string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public Form7(string indf,string str1)
        {
            InitializeComponent();
            if (indf == "Provider" && str1==null)
            {
                Indf = indf;
                button1.Visible = false;
            }
            else if (indf == "Provider" && str1 !=null)
            {
                Indf = indf;
                Str1 = str1;
                button3.Visible = false;
                OpenFirm();
            }
            else if (indf == "Firma")
            {
                Indf = indf;
                button3.Visible = false;
                OpenFirm();
            }
        }
    

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateFirm();
            DialogResult = DialogResult.OK;
            Close();
        }

        public void SaveFirm()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("insert into "+Indf+" (Name,INN,KPP,Phone,address) values(@Name,@INN,@KPP,@Phone,@address)", con);
            con.Open();
            cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@INN", textBox2.Text.Trim());
            cmd.Parameters.AddWithValue("@KPP", textBox3.Text.Trim());
            cmd.Parameters.AddWithValue("@Phone", textBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@address", textBox5.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Запись добавлена");
            con.Close();
        }

        public void UpdateFirm()
        {
            SqlConnection con = new SqlConnection(conString);
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand("update "+Indf+ " set Name=@Name,INN=@INN,KPP=@KPP,Phone=@Phone,address=@address where " + Indf + "Id =@" + Indf + "Id", con);
            con.Open();
            cmd.Parameters.AddWithValue("@" + Indf + "Id", int.Parse(textBox6.Text.Trim()));
            cmd.Parameters.AddWithValue("@Name", textBox1.Text.Trim());
            cmd.Parameters.AddWithValue("@INN", Convert.ToDecimal(textBox2.Text.Trim()));
            cmd.Parameters.AddWithValue("@KPP", Convert.ToDecimal(textBox3.Text.Trim()));
            cmd.Parameters.AddWithValue("@Phone", textBox4.Text.Trim());
            cmd.Parameters.AddWithValue("@address", textBox5.Text.Trim());
            cmd.ExecuteNonQuery();
            con.Close();
            MessageBox.Show("Запись обновлена");
            con.Close();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void OpenFirm()
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                if (Indf == "Provider"){
                    zapros = " Where ProviderId = '" + int.Parse(Str1) + "'";
                }
                string sql = "Select * From "+ Indf + zapros;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        while (reader.Read())
                        {
                            // Индекс столбца Emp_ID в команде SQL.
                            textBox1.Text = reader.GetValue(1).ToString().Trim();
                            textBox2.Text = reader.GetValue(2).ToString().Trim();
                            textBox3.Text = reader.GetValue(3).ToString().Trim();
                            textBox4.Text = reader.GetValue(4).ToString().Trim();
                            textBox5.Text = reader.GetValue(5).ToString().Trim();
                            textBox6.Text = reader.GetValue(0).ToString().Trim();

                        }

                        con.Close();
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFirm();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void textBox_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox2.Text, out double parsedNumber))
            {            }
            else
            {
                textBox2.Text = "";
                MessageBox.Show("Используйте только цифры !");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox3.Text, out double parsedNumber))
            { }
            else
            {
                textBox3.Text = "";
                MessageBox.Show("Используйте только цифры !");
            }
        }
        private void textBox4_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox4.Text, out double parsedNumber))
            { }
            else
            {
                textBox4.Text = "";
                MessageBox.Show("Используйте только цифры !");
            }
        }
    }
}

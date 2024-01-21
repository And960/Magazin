using Magazin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazin
{
    public partial class Form1 : Form
    {
        public static string conString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public static flowerList list = new flowerList();
        public static korzinaList list1 = new korzinaList();
        public Form1()
        {
            InitializeComponent();
            UpdateForm();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Файлы csv (*.csv)|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LoadTxtFile(dialog.FileName);
            }
        }

        private void LoadTxtFile(string fileName)
        {
            
            list.LoadFromFile(fileName);

        }

        private void button2_Click(object sender, EventArgs e)
        {
                Form8 Form = new Form8();
                this.Hide();
                Form.ShowDialog();
                this.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void UpdateForm()
        {
            SqlConnection con = new SqlConnection(conString);
            con.Open();
            if (con.State == System.Data.ConnectionState.Open)
            {
                string sql = "select * from Firma";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            label4.Text = reader.GetValue(1).ToString();
                            label2.Text = reader.GetValue(5).ToString();
                        }

                        con.Close();
                    }
                }
            }
        }
    }
}

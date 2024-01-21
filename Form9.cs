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
    public partial class Form9 : Form
    {
        public string str2;
        public string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public Form9(string str)
        {
            if (str == "Viever")
            {
                InitializeComponent();
                ShowDataInDataGridView();
                dataGridView1.RowHeadersVisible = false;
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
            }
            else
            {
                InitializeComponent();
                ShowDataInDataGridView();
                dataGridView1.RowHeadersVisible = false;
                button5.Visible = false;
            }
        }

        private void ShowDataInDataGridView()
        {
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {

                string sql = "select * from Provider";
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {

                        List<string[]> data = new List<string[]>();
                        while (reader.Read())
                        {
                            data.Add(new string[6]);

                            data[data.Count - 1][0] = reader.GetValue(0).ToString(); ;
                            data[data.Count - 1][1] = reader.GetValue(1).ToString(); ;
                            data[data.Count - 1][2] = reader.GetValue(2).ToString(); ;
                            data[data.Count - 1][3] = reader.GetValue(3).ToString(); ;
                            data[data.Count - 1][4] = reader.GetValue(4).ToString(); ;
                            data[data.Count - 1][5] = reader.GetValue(5).ToString(); ;
                        }
                        con.Close();

                        foreach (string[] s in data)
                        dataGridView1.Rows.Add(s);
                    }
                }
            }
            dataGridView1.Refresh();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;


        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 Form7 = new Form7("Provider",null);
            if (Form7.ShowDialog() == DialogResult.OK)
            {
            ShowDataInDataGridView();
            }
            else
            {
                Form7.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int Y = dataGridView1.CurrentCellAddress.Y;
            if (Y < 0)
            {
                MessageBox.Show("Таблица не заполнена");
                return;
            }
            string str1 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            Form7 Form7 = new Form7("Provider", str1);
            if (Form7.ShowDialog() == DialogResult.OK)
            {
                ShowDataInDataGridView();
            }
            else
            {
                Form7.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int Y = dataGridView1.CurrentCellAddress.Y;
            if (Y < 0)
            {
                MessageBox.Show("Таблица не заполнена");
                return;
            }
            string str1 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            if (str1 != null)
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("delete Provider where ProviderId=@ProviderId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@ProviderId", str1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Запись удалена!");
                ShowDataInDataGridView();
            }
            else
            {
                MessageBox.Show("Ошибка при удалении");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int Y = dataGridView1.CurrentCellAddress.Y;
            str2 = str2 + dataGridView1.Rows[Y].Cells[1].Value.ToString().Trim();// + " " + dataGridView1.Rows[Y].Cells[2].Value.ToString().Trim() + " " + dataGridView1.Rows[Y].Cells[3].Value.ToString().Trim();
            DialogResult = DialogResult.OK;
        }
    }
}

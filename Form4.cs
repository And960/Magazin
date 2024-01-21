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
using System.Xml;
using Magazin;

namespace Magazin
{

    public partial class Form4 : Form
    {
        public string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public byte[] data = null;
        public string filename;
        public string str2;
        public string str1;
        public Form4(string str)
        {
            InitializeComponent();
            if (str == "Viever" | str == "Viever1" | str == "Viever2")
            {
                str1 = str;
                button1.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                button6.Visible = false;
            }
            else
            {
                button8.Visible = false;
            }

            ShowDataInDataGridView();
            dataGridView1.RowHeadersVisible = false;
            button7.Visible = false;
        }
        private void ShowDataInDataGridView()
        {
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {

                string sql = "select * from Tovar";
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
                            data.Add(new string[8]);
                            data[data.Count - 1][0] = reader.GetValue(0).ToString().Trim();
                            data[data.Count - 1][1] = reader.GetValue(1).ToString().Trim();
                            data[data.Count - 1][2] = reader.GetValue(2).ToString().Trim();
                            data[data.Count - 1][3] = reader.GetValue(3).ToString().Trim();
                            data[data.Count - 1][4] = reader.GetValue(4).ToString().Trim();
                            data[data.Count - 1][5] = reader.GetValue(5).ToString().Trim();
                            data[data.Count - 1][6] = reader.GetValue(6).ToString().Trim();
                            data[data.Count - 1][7] = reader.GetValue(7).ToString().Trim();
                        }
                        con.Close();

                        foreach (string[] s in data)
                            dataGridView1.Rows.Add(s);
                    }
                }
            }
            dataGridView1.Refresh();
            dataGridView1.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form5 Form5 = new Form5(null);
            if (Form5.ShowDialog() == DialogResult.OK)
            {
                ShowDataInDataGridView();
            }
            else
            {
                Form5.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.Filter = "Файлы csv (*.csv)|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SaveTxtFile(dialog.FileName);
            }
        }
        private void SaveTxtFile(string fileName)
        {

            StreamWriter sw = new StreamWriter(fileName, false, Encoding.GetEncoding("windows-1251"));

            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                {
                    sw.Write(dataGridView1.Rows[i].Cells[j].Value);
                    if (j < dataGridView1.ColumnCount - 1)
                        sw.Write(";");
                }
                sw.WriteLine();
            }
            sw.Close();

        }

        private void button4_Click(object sender, EventArgs e)
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
                cmd = new SqlCommand("delete Tovar where TovarId=@TovarId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@TovarId", str1);
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

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.RowCount; i++)
            {
                dataGridView1.Rows[i].Selected = false;
                for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    if (dataGridView1.Rows[i].Cells[j].Value != null)
                        if (dataGridView1.Rows[i].Cells[j].Value.ToString().ToLower().Contains(textBox1.Text.ToLower()))
                        {
                            dataGridView1.Rows[i].Selected = true;
                            break;
                        }
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int Y = dataGridView1.CurrentCellAddress.Y;
            if (Y < 0)
            {
                MessageBox.Show("Таблица не заполнена");
                return;
            }
            string str1 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();

            Form5 Form5 = new Form5(str1);
            if (Form5.ShowDialog() == DialogResult.OK)
            {
                ShowDataInDataGridView();
            }
            else
            {
                Form5.Close();
            }
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Foto();
        }

        private void Foto()
        {
            SqlConnection con = new SqlConnection(conString);

            con.Open();
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            if (con.State == System.Data.ConnectionState.Open)
            {
                int Y = dataGridView1.CurrentCellAddress.Y;
                if (Y < 0)
                {
                    MessageBox.Show("Таблица не заполнена");
                    return;
                }
                else
                {
                    string str1 = dataGridView1[0, dataGridView1.CurrentRow.Index].Value.ToString();
                    string sql = "Select * From Tovar Where TovarId =" + int.Parse(str1);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandText = sql;
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                textBox2.Text = reader.GetValue(9).ToString();
                                if (textBox2.Text=="")
                                { return; }
                                data = (byte[])reader.GetValue(8);
                            }
                            filename = @"C:\Pictures\" + textBox2.Text;
                            con.Close();
                            using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.OpenOrCreate))
                            {
                                fs.Write(data, 0, data.Length);
                                pictureBox1.Image = Image.FromStream(fs);
                                fs.Close();
                            }


                        }
                    }
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Foto();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (str1 == "Viever")
            {
                int Y = dataGridView1.CurrentCellAddress.Y;
                str2 = dataGridView1.Rows[Y].Cells[2].Value.ToString().Trim();
                DialogResult = DialogResult.OK;
            }
            else if (str1 == "Viever1")
            {
                int Y = dataGridView1.CurrentCellAddress.Y;
                if (Convert.ToDouble(dataGridView1.Rows[Y].Cells[6].Value.ToString()) > 0)
                {
                    str2 = "";
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        if (j == 6)
                        {
                            str2 = str2 + "1" + ";";
                            dataGridView1.Rows[Y].Cells[j].Value = Convert.ToDouble(dataGridView1.Rows[Y].Cells[j].Value.ToString()) - 1;
                        }
                        else if (j == 7)
                        {
                            dataGridView1.Rows[Y].Cells[j].Value = Convert.ToDouble(dataGridView1.Rows[Y].Cells[j].Value.ToString()) - Convert.ToDouble(dataGridView1.Rows[Y].Cells[5].Value.ToString());
                            str2 = str2 + dataGridView1.Rows[Y].Cells[5].Value.ToString() + ";";
                        }
                        else
                            str2 = str2 + dataGridView1.Rows[Y].Cells[j].Value.ToString() + ";";
                    }
                    Form1.list1.AdStr(str2);
                }
                else
                {
                    MessageBox.Show("Товара нет на складе");
                }
            }
            else if (str1 == "Viever2")
            {
                int Y = dataGridView1.CurrentCellAddress.Y;
                    str2 = "";
                    for (int j = 0; j < dataGridView1.ColumnCount; j++)
                    {
                        if (j == 6)
                        {
                            str2 = str2 + "1" + ";";
                        }
                        else if (j == 7)
                            str2 = str2 + dataGridView1.Rows[Y].Cells[5].Value.ToString() + ";";
                        else
                            str2 = str2 + dataGridView1.Rows[Y].Cells[j].Value.ToString() + ";";
                    }
                    Form1.list1.AdStr(str2);

            }
            else
            {
            
            }
        }
    }
} 


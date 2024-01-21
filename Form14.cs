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
    public partial class Form14 : Form
    {
        public string conString = Form1.conString;
        public int Nmaxim = 0;
        public string ZaprosData;
        public Form14()
        {
            InitializeComponent();
            ShowDataInDataGridView(ZaprosData);
            dataGridView1.RowHeadersVisible = false;
            dateTimePicker1.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        private void ShowDataInDataGridView(string zaprosData)
        {
            dataGridView1.Rows.Clear();
            SqlConnection con = new SqlConnection(conString);

            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {

                string sql = "SELECT max([SaleId]) ,[NDok] ,max([DataDok]) ,max([dbo].[Firma].[Name]) ,max([dbo].[Buyer].[LastName]) ,max([SummaDok]) ,max([TypeDok]) FROM [dbo].[Sale]" +
                    " inner join [dbo].[Firma] on [dbo].[Sale].FirmaId = [dbo].[Firma].FirmaId " +
                    "inner join [dbo].[Buyer] on [dbo].[Sale].BuyerId = [dbo].[Buyer].BuyerId" + ZaprosData + "  GROUP BY [NDok]";
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
                            data.Add(new string[7]);

                            data[data.Count - 1][0] = reader.GetValue(0).ToString();
                            data[data.Count - 1][1] = reader.GetValue(1).ToString();
                            data[data.Count - 1][2] = reader.GetValue(6).ToString();
                            data[data.Count - 1][3] = reader.GetValue(2).ToString();
                            data[data.Count - 1][4] = reader.GetValue(3).ToString();
                            data[data.Count - 1][5] = reader.GetValue(4).ToString();
                            data[data.Count - 1][6] = reader.GetValue(5).ToString();

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
            Close();
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
                cmd = new SqlCommand("delete Sale where SaleId=@SaleId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@SaleId", str1);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Документ удален!");
                ShowDataInDataGridView(ZaprosData);
            }
            else
            {
                MessageBox.Show("Ошибка при удалении документа");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form10 Form10 = new Form10("Viever");
            if (Form10.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = Form10.str2;
                Form10.Close();
            }
            else
            {
                Form10.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string lastName;
            string data1 = dateTimePicker1.Value.ToString("yyyy.MM.dd");
            string data2 = dateTimePicker2.Value.AddDays(1).ToString("yyyy.MM.dd");
            ZaprosData = " WHERE[DataDok] BETWEEN CONVERT(DATETIME , '" + data1 + "' , 102 ) AND CONVERT(DATETIME , '" + data2 + "'  , 102 )";
            if (textBox1.Text == "")
            {

            }
            else
            {
                int count = textBox1.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
                if (count < 1)
                {
                    lastName = textBox1.Text.Substring(0, textBox1.Text.IndexOf(' ') + 1);
                }
                else
                {
                    lastName = textBox1.Text.Trim();
                }
                ZaprosData = ZaprosData + " AND [dbo].[Buyer].[LastName] =N'" + lastName.Trim() + "'";
            }
            if (textBox2.Text == "")
            {

            }
            else
            {
                ZaprosData = ZaprosData + " AND [NDok] ='" + textBox2.Text.Trim() + "'";
            }

            ShowDataInDataGridView(ZaprosData);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.Count - 1;
            if (selectedRowCount > 0)
            {
                for (int j = 0; j < selectedRowCount; j++)
                {
                    string Max1 = dataGridView1.Rows[j].Cells[1].Value.ToString();
                    if (int.Parse(Max1) >= Nmaxim)
                    {
                        Nmaxim = int.Parse(Max1);
                        dataGridView1[j, 1].Selected = true;
                    }
                }

                Form6 Form = new Form6(Nmaxim + 1);
                this.Hide();
                Form.ShowDialog();
                this.Show();
                ShowDataInDataGridView(ZaprosData);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        bool StringIsDigits(string s)
        {
            foreach (var item in s)
            {
                if (!char.IsDigit(item))
                    return false; //если хоть один символ не число, то выкидываешь "ложь"
            }
            return true; //если ни разу не выбило в цикле, значит, все символы - это цифры
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (StringIsDigits(textBox2.Text) == false)
            {
                MessageBox.Show("Номер должен содержать только цифры");
                textBox2.Text = "";
            }
        }
    }
}

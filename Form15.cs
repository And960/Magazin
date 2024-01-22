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
    public partial class Form15 : Form
    {
        public string conString = Form1.conString;
        public string ZaprosData;
        public string ZaprosKontr1;
        public string ZaprosKontr2;
        public Form15()
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
            string sql;
            con.Open();

            if (con.State == System.Data.ConnectionState.Open)
            {
                string sql1 = "SELECT [SaleId] ,[NDok] ,[DataDok] ,[dbo].[Firma].[Name] ,[dbo].[Buyer].[LastName] ,[dbo].[Tovar].[Name],[PrinceDok] ,[QuantityDok] ,[SummaD] ,[SummaDok] ,[TypeDok] " +
                    "FROM[dbo].[Sale] inner join [dbo].[Firma] on [dbo].[Sale].FirmaId = [dbo].[Firma].FirmaId " +
                    "inner join [dbo].[Buyer] on [dbo].[Sale].BuyerId = [dbo].[Buyer].BuyerId " +
                    "inner join [dbo].[Tovar] on [dbo].[Sale].TovarId = [dbo].[Tovar].TovarId" + ZaprosData + ZaprosKontr1;
                string sql2 = "SELECT [PurchasesId] ,[NDok] ,[DataDok] ,[dbo].[Firma].[Name] ,[dbo].[Provider].[Name] ,[dbo].[Tovar].[Name],[PrinceDok] ,[QuantityDok] ,[SummaD] ,[SummaDok] ,[TypeDok] " +
                    "FROM[dbo].[Purchases] inner join [dbo].[Firma] on [dbo].[Purchases].FirmaId = [dbo].[Firma].FirmaId " +
                    "inner join [dbo].[Provider] on [dbo].[Purchases].ProviderId = [dbo].[Provider].ProviderId " +
                    "inner join [dbo].[Tovar] on [dbo].[Purchases].TovarId = [dbo].[Tovar].TovarId" + ZaprosData + ZaprosKontr2;
                if (comboBox2.Text == "Покупатель") {
                    if (string.IsNullOrEmpty(textBox1.Text)) { sql = sql1 + " UNION " + sql2; }
                    else { sql = sql1; }
                }
                else if (comboBox2.Text == "Поставщик") {
                    if (string.IsNullOrEmpty(textBox1.Text)) { sql = sql1 + " UNION " + sql2; }
                    else { sql = sql2; }
                }
                else
                {
                    sql = sql1 + " UNION " + sql2;
                }
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
                            data.Add(new string[10]);

                            data[data.Count - 1][0] = reader.GetValue(0).ToString();
                            data[data.Count - 1][1] = reader.GetValue(1).ToString();
                            data[data.Count - 1][2] = reader.GetValue(10).ToString();
                            data[data.Count - 1][3] = reader.GetValue(2).ToString();
                            data[data.Count - 1][4] = reader.GetValue(3).ToString();
                            data[data.Count - 1][5] = reader.GetValue(4).ToString();
                            data[data.Count - 1][6] = reader.GetValue(5).ToString();
                            data[data.Count - 1][7] = reader.GetValue(6).ToString();
                            data[data.Count - 1][8] = reader.GetValue(7).ToString();
                            data[data.Count - 1][9] = reader.GetValue(8).ToString();
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

        private void button4_Click(object sender, EventArgs e)
        {
            string lastName;
            string data1 = dateTimePicker1.Value.ToString("yyyy.MM.dd");
            string data2 = dateTimePicker2.Value.AddDays(1).ToString("yyyy.MM.dd");
            ZaprosData = " WHERE[DataDok] BETWEEN CONVERT(DATETIME , '" + data1 + "' , 102 ) AND CONVERT(DATETIME , '" + data2 + "'  , 102 )";
            if (textBox1.Text == "")
            {
                ZaprosKontr1 = "";
                ZaprosKontr2 = "";
            }
            else
            {
                if (comboBox2.Text == "Покупатель")
                {
                    int count = textBox1.Text.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries).Length;
                    if (count > 1)
                    {
                        lastName = textBox1.Text.Substring(0, textBox1.Text.IndexOf(' ') + 1);
                    }
                    else
                    {
                        lastName = textBox1.Text.Trim();
                    }
                    ZaprosKontr1= " AND [dbo].[Buyer].[LastName] =N'" + lastName.Trim() + "'";
                }
                else if (comboBox2.Text == "Поставщик")
                {
                    if (textBox1.Text == "")
                    {

                    }
                    else
                    {
                        ZaprosKontr2 = " AND [dbo].[Provider].[Name] =N'" + textBox1.Text.Trim() + "'";
                    }
                }    
            }
            if (textBox2.Text == "")
            {

            }
            else
            {
                ZaprosData = ZaprosData + " AND [NDok] ='" + textBox2.Text.Trim() + "'";
            }

            if (textBox3.Text == "")
            {

            }
            else
            {
                ZaprosData = ZaprosData + " AND [dbo].[Tovar].[Name] =N'" + textBox3.Text.Trim() + "'"; ;
            }

            if (comboBox1.Text == "")
            {

            }
            else
            {
                ZaprosData = ZaprosData + " AND [TypeDok] =N'" + comboBox1.Text.Trim() + "'";
            }

            ShowDataInDataGridView(ZaprosData);
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

        private void button6_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "Покупатель")
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
            else
            {
                Form9 Form9 = new Form9("Viever");
                if (Form9.ShowDialog() == DialogResult.OK)
                {
                    textBox1.Text = Form9.str2;
                    Form9.Close();
                }
                else
                {
                    Form9.Close();
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4("Viever");
            if (Form4.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = Form4.str2;
                Form4.Close();
            }
            else
            {
                Form4.Close();
            }
           
        }
    }
}

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
    public partial class Form13 : Form
    {
        public string str2;
        public string conString = Form1.conString;
        public int Nmaxim = 0;
        public string ZaprosData;
        public Form13()
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

                string sql = "SELECT max([PurchasesId]) ,[NDok] ,max([DataDok]) ,max([dbo].[Firma].[Name]) ,max([dbo].[Provider].[Name]) ,max([SummaDok]) ,max([TypeDok]) FROM [dbo].[Purchases]" +
                    " inner join [dbo].[Firma] on [dbo].[Purchases].FirmaId = [dbo].[Firma].FirmaId " +
                    "inner join [dbo].[Provider] on [dbo].[Purchases].ProviderId = [dbo].[Provider].ProviderId" + ZaprosData+"  GROUP BY [NDok]";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView1.Rows.Count-1;
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
                    Form12 Form = new Form12(Nmaxim+1);
                    this.Hide();
                    Form.ShowDialog();
                    this.Show();
                ShowDataInDataGridView(ZaprosData);
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
                cmd = new SqlCommand("delete Purchases where PurchasesId=@PurchasesId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@PurchasesId", str1);
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

        private void button4_Click(object sender, EventArgs e)
        {
            string data1 = dateTimePicker1.Value.ToString("yyyy.MM.dd");
            string data2 = dateTimePicker2.Value.AddDays(1).ToString("yyyy.MM.dd");
            ZaprosData = " WHERE[DataDok] BETWEEN CONVERT(DATETIME , '"  + data1 + "' , 102 ) AND CONVERT(DATETIME , '" + data2 + "'  , 102 )";
            if (textBox1.Text == ""){

            }
            else
            {
                ZaprosData = ZaprosData+ " AND [dbo].[Provider].[Name] =N'" + textBox1.Text.Trim() + "'";
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

        private void button5_Click(object sender, EventArgs e)
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
            textBox2.Clear();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
        }
    }
}

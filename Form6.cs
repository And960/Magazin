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
    public partial class Form6 : Form
    {
        public double qual = 0;
        public string str22;
        public double ostatok = 0;
        public object ID;
        public static string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public Form6(int Nmax)
        {
            
            InitializeComponent();
            dataGridView2.Rows.Clear();
            UpdateForm(Nmax);
            dataGridView2.RowHeadersVisible = false;
        }
        private void ShowDataInDataGridView1()
        {

            dataGridView2.RowHeadersVisible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text == "")
            {
                MessageBox.Show("Вы не выбрали покупателя !");
                return;
            }
            Int32 selectedRowCount = dataGridView2.Rows.Count;
            if (selectedRowCount > 0)
            {
                for (int j = 0; j < selectedRowCount; j++)
                {
                    string phrase = textBox3.Text;
                    string[] nameParts = phrase.Split(' ');

                    string firstName = nameParts[0];
                    string lastName = nameParts[1];
                    string Name = nameParts[2];
                    SqlConnection con = new SqlConnection(conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd = new SqlCommand("insert into Sale" + " (NDok,DataDok,FirmaId,BuyerId,TovarId,PrinceDok,QuantityDok,SummaD,SummaDok,TypeDok) values(@NDok,@DataDok,(SELECT [FirmaId] FROM [dbo].[Firma] WHERE [Name] = N'" + textFirma.Text.Trim() + "'),(SELECT TOP 1000 [BuyerId] FROM [dbo].[Buyer] WHERE [LastName] = N'"+firstName.Trim()+ "' AND [FirstName] = N'"+lastName.Trim()+ "' AND [Name] = N'"+Name.Trim()+"'),@TovarId,@Prince,@Quantity,@Summa,@SummaDok,@TypeDok)", con);
                    con.Open();
                    cmd.Parameters.AddWithValue("@NDok", textBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@DataDok", dateTimePicker1.Value.ToString("MM.dd.yyyy HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@TovarId", dataGridView2.Rows[j].Cells[0].Value);
                    cmd.Parameters.AddWithValue("@Prince", Convert.ToDecimal(dataGridView2.Rows[j].Cells[5].Value));
                    cmd.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(dataGridView2.Rows[j].Cells[6].Value));
                    cmd.Parameters.AddWithValue("@Summa", Convert.ToDecimal(dataGridView2.Rows[j].Cells[7].Value));
                    cmd.Parameters.AddWithValue("@SummaDok", Convert.ToDecimal(textBox1.Text.Trim()));
                    cmd.Parameters.AddWithValue("@TypeDok", "Продажа");
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                MessageBox.Show("Документ записан");
            }
            else
            {
                MessageBox.Show("Табличная часть пустая");
                return;
            }
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int Y = dataGridView2.CurrentCellAddress.Y;

            if (Y < 0)
            {
                MessageBox.Show("Табличная часть пустая");
                return;
            }
            str22 = "";
            if (Convert.ToDouble(dataGridView2.Rows[Y].Cells[6].Value.ToString()) > 0)
            {
                for (int j = 0; j < dataGridView2.ColumnCount; j++)
                {
                    ID = dataGridView2.Rows[Y].Cells[0].Value;
                    if (j == 6)
                    {
                        str22 = str22 + "1" + ";";
                        dataGridView2.Rows[Y].Cells[j].Value = Convert.ToDouble(dataGridView2.Rows[Y].Cells[j].Value.ToString()) - 1;
                    }
                    else if (j == 7)
                    {
                        str22 = str22 + dataGridView2.Rows[Y].Cells[5].Value.ToString() + ";";
                        dataGridView2.Rows[Y].Cells[j].Value = Convert.ToDouble(dataGridView2.Rows[Y].Cells[j].Value.ToString()) - Convert.ToDouble(dataGridView2.Rows[Y].Cells[5].Value.ToString());
                        qual = qual + -Convert.ToDouble(dataGridView2.Rows[Y].Cells[5].Value.ToString());
                    }
                    else
                    {
                        str22 = str22 + dataGridView2.Rows[Y].Cells[j].Value.ToString() + ";";
                    }
                }
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd = new SqlCommand("UPDATE Tovar SET Quantity = isnull(Quantity, 0) +1,Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                cmd.Parameters.AddWithValue("@TovarId", ID);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE Tovar SET Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                cmd.Parameters.AddWithValue("@TovarId", ID);
                cmd.ExecuteNonQuery();
                con.Close();
                Form1.list.LoadStr(str22);
                Form1.list1.DelStr(str22);
                
                if (Convert.ToDouble(dataGridView2.Rows[Y].Cells[6].Value.ToString()) == 0)

                {
                    dataGridView2.Rows.RemoveAt(Y);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Form4 Form4 = new Form4("Viever1");
            if (Form4.ShowDialog() == DialogResult.OK)
            {
                Form4.Close();
            }
            else
            {
                Form4.Close();
            }
            dataGridView2.Rows.Clear();
            SqlConnection con = new SqlConnection(conString);
            
            con.Open();
            foreach (Sale korzina in Form1.list1.list1)

            {
                SqlCommand cmd = new SqlCommand();
                //добавим строку, указав нужны данные в порядке следования столбцов
                dataGridView2.Rows.Add(korzina.Id, korzina.Art, korzina.Name, korzina.Description, korzina.EdIzn,
                korzina.Prince, korzina.Quality, korzina.Summa, korzina.Img, korzina.ImgName);
                qual = qual + korzina.Summa;
                cmd = new SqlCommand("UPDATE Tovar SET Quantity = isnull(Quantity, 0) -'" + korzina.Quality + "',Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                cmd.Parameters.AddWithValue("@TovarId", korzina.Id);
                cmd.ExecuteNonQuery();
                cmd = new SqlCommand("UPDATE Tovar SET Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                cmd.Parameters.AddWithValue("@TovarId", korzina.Id);
                cmd.ExecuteNonQuery();
            }
            con.Close();
            textBox1.Text = qual.ToString();
            dataGridView2.Refresh();
            dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            Form1.list1.ClearList();
        }
        private void UpdateForm(int Nmax)
        {
            textBox2.Text = Nmax.ToString();
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
                            textFirma.Text = reader.GetValue(1).ToString();

                        }

                        con.Close();
                    }
                }
            }
            //textTime.Text = DateTime.Now.ToString("dd.MM.yy HH:mm:ss");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form10 Form10 = new Form10("Viever");
            if (Form10.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = Form10.str2;
                Form10.Close();
            }
            else
            {
                Form10.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Int32 selectedRowCount = dataGridView2.Rows.Count;
            if (selectedRowCount > 0)
            {
                SqlConnection con = new SqlConnection(conString);
                SqlCommand cmd = new SqlCommand();
                con.Open();
                for (int j = 0; j < selectedRowCount; j++)
                {
                    ID = dataGridView2.Rows[j].Cells[0].Value;
                    cmd = new SqlCommand("UPDATE Tovar SET Quantity = isnull(Quantity, 0) +'" + Convert.ToDecimal(dataGridView2.Rows[j].Cells[6].Value) + "',Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                    cmd.Parameters.AddWithValue("@TovarId", dataGridView2.Rows[j].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                    cmd = new SqlCommand("UPDATE Tovar SET Summa=Prince*Quantity WHERE TovarId=@TovarId", con);
                    cmd.Parameters.AddWithValue("@TovarId", dataGridView2.Rows[j].Cells[0].Value);
                    cmd.ExecuteNonQuery();
                }

                con.Close();
            }
                    Form1.list1.ClearList();
            Close();
        }
    }
}

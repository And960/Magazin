using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazin
{
    public partial class Form5 : Form
    {
        public string conString = Form1.conString;//"Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=F:\\Mag\\SQL\\Shops.mdf;Integrated Security=True;Connect Timeout=30";
        public byte[] data = null;
        public string filename;
        public string Str1;
        static string zapros;
        public byte[] imageData=null;
        public string shortFileName;
        public Form5(string str1)
        {
            InitializeComponent();
            if (str1 != null)
            {
                Str1 = str1;
                OpenFirm();
                button1.Visible = false;
            }
            else
            {
                button4.Visible = false;
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
                    zapros = " Where TovarId = '" + int.Parse(Str1) + "'";
                }
                string sql = "Select * From Tovar" + zapros;
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            textBox10.Text = reader.GetValue(0).ToString().Trim();
                            textBox1.Text = reader.GetValue(1).ToString().Trim();
                            textBox2.Text = reader.GetValue(2).ToString().Trim();
                            textBox3.Text = reader.GetValue(3).ToString().Trim();
                            textBox4.Text = reader.GetValue(4).ToString().Trim();
                            textBox5.Text = reader.GetValue(5).ToString().Trim();
                            textBox6.Text = reader.GetValue(6).ToString().Trim();
                            textBox7.Text = reader.GetValue(7).ToString().Trim();
                            textBox8.Text = reader.GetValue(9).ToString().Trim();
                            if (textBox8.Text == "") { }
                            else { data = (byte[])reader.GetValue(8); }

                        }
                        if (textBox8.Text == "") { }
                        else
                        {
                            filename = @"C:\Pictures\" + textBox8.Text;
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
        private void button1_Click(object sender, EventArgs e)
        {
            proverka();
            SaveFileToDatabase();
            MessageBox.Show("Запись добавлена");
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void SaveFileToDatabase()
        {
            using (SqlConnection con = new SqlConnection(conString))
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                string shortFileName;
                byte[] imageData;
                if (filename != null)
                {
                    shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1); // cats.jpg
                    using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                        fs.Close();
                    }
                }
                else
                {
                    shortFileName = "";
                    imageData = new byte[1];
                }

                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("insert into Tovar(Art,Name,Description,EdIzm,Prince,Quantity,Summa,Img,ImgName) values(@Art,@Name,@Description,@EdIzm,@Prince,@Quantity,@Summa,@Img,@ImgName)", con);
                con.Open();
                cmd.Parameters.AddWithValue("@Art", textBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@Name", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@EdIzm", textBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Prince", textBox5.Text.Trim());
                cmd.Parameters.AddWithValue("@Quantity", textBox6.Text.Trim());
                cmd.Parameters.AddWithValue("@Summa", textBox7.Text.Trim());
                cmd.Parameters.AddWithValue("@Img", imageData);
                cmd.Parameters.AddWithValue("@ImgName", shortFileName);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox6.Text, out double parsedNumber))
            {
                if (textBox5.Text == "")
                {
                    textBox7.Text = "";
                }
                else
                {
                    textBox7.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox6.Text));
                }
            }
            else
            {
                textBox6.Text = "";
                MessageBox.Show("Вы ввели не правильно КОЛИЧЕСТВО , используйте только цифры и ,");
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)

        {
            if (double.TryParse(textBox5.Text, out double parsedNumber))
            {

                if (textBox6.Text == "")
                {
                    textBox7.Text = "";
                }
                else
                {
                    textBox7.Text = Convert.ToString(Convert.ToDouble(textBox5.Text) * Convert.ToDouble(textBox6.Text));
                }
            }
            else
            {
                textBox5.Text = "";
                MessageBox.Show("Вы ввели не правильно ЦЕНУ , используйте только цифры и ,");
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

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (StringIsDigits(textBox1.Text) == false)
            {
                MessageBox.Show("Артикул должен содержать только цифры");
                textBox1.Text = textBox1.Text.Remove(textBox1.TextLength-1);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Contains(";"))
            {
                MessageBox.Show("Знак" +"';'" +"нельзя использовать");
                textBox2.Text = textBox2.Text.Replace(";", "");
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains(";"))
            {
                MessageBox.Show("Знак" + "';'" + "нельзя использовать");
                textBox3.Text = textBox3.Text.Replace(";", "");
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Contains(";"))
            {
                MessageBox.Show("Знак" + "';'" + "нельзя использовать");
                textBox4.Text = textBox4.Text.Replace(";", "");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Bitmap Image;
            OpenFileDialog s = new OpenFileDialog();
            s.Filter = "jpg files (*.jpg)|*.jpg|All files (*.*)|*.*";
            if (s.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    filename = s.FileName;
                    Image = new Bitmap(s.FileName);
                    pictureBox1.Image = Image;
                    pictureBox1.Invalidate();
                    s.Dispose();
                }
                catch
                {
                    DialogResult rezult = MessageBox.Show("Не возможно открыть файл",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            proverka();
            using (SqlConnection con = new SqlConnection(conString))
            {
                if (pictureBox1.Image != null)
                {
                    pictureBox1.Image.Dispose();
                    pictureBox1.Image = null;
                }
                if (filename == null) {
                    filename = @"C:\Pictures\defimage.gif";

                }
                //else
                //{
                    shortFileName = filename.Substring(filename.LastIndexOf('\\') + 1); // cats.jpg
                                                                                               // массив для хранения бинарных данных файла
                    byte[] imageData;
                    using (System.IO.FileStream fs = new System.IO.FileStream(filename, FileMode.Open))
                    {
                        imageData = new byte[fs.Length];
                        fs.Read(imageData, 0, imageData.Length);
                        fs.Close();
                    }
                //}
                SqlCommand cmd = new SqlCommand();
                cmd = new SqlCommand("update Tovar set Art=@Art,Name=@Name,Description=@Description,EdIzm=@EdIzm,Prince=@Prince,Quantity=@Quantity,Summa=@Summa,Img=@Img,ImgName=@ImgName where TovarId=@TovarId", con);
                con.Open();
                cmd.Parameters.AddWithValue("@TovarId", int.Parse(textBox10.Text));
                cmd.Parameters.AddWithValue("@Art", Convert.ToDecimal(textBox1.Text.Trim()));
                cmd.Parameters.AddWithValue("@Name", textBox2.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", textBox3.Text.Trim());
                cmd.Parameters.AddWithValue("@EdIzm", textBox4.Text.Trim());
                cmd.Parameters.AddWithValue("@Prince", Convert.ToDecimal(textBox5.Text.Trim()));
                cmd.Parameters.AddWithValue("@Quantity", Convert.ToDecimal(textBox6.Text.Trim()));
                cmd.Parameters.AddWithValue("@Summa", Convert.ToDecimal(textBox7.Text.Trim()));
                cmd.Parameters.AddWithValue("@Img", imageData);
                cmd.Parameters.AddWithValue("@ImgName", shortFileName);
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Запись обновлена");
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
                pictureBox1.Image = null;
            }
            DialogResult = DialogResult.OK;
        }

        private void proverka()
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Вы не ввели АРТИКУЛ товара");
                return;
            }
            else if (textBox2.Text == "")
            {
                MessageBox.Show("Вы не ввели НАИМЕНОВАНИЕ товара");
            }
            else if (textBox3.Text == "")
            {
                MessageBox.Show("Вы не ввели ВИД товара");
            }
            else if (textBox4.Text == "")
            {
                MessageBox.Show("Вы не ввели ЦВЕТ товара");
            }
            else if (textBox5.Text == "")
            {
                MessageBox.Show("Вы не ввели ЦЕНУ товара");
            }
            else if (textBox6.Text == "")
            {
                MessageBox.Show("Вы не ввели КОЛИЧЕСТВО товара");
            }
        }
    }
}

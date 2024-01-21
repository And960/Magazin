using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Magazin;

namespace Magazin
{
    public partial class Form3 : Form
    {
        public string str2;
        public Form3()
        {
            InitializeComponent();
            ShowDataInDataGridView();
        }

        private void ShowDataInDataGridView()
        {
            dataGridView1.Rows.Clear();
            foreach (Flower flower in Form1.list.list)
            {
                //добавим строку, указав нужны данные в порядке следования столбцов
                //dataGridView1.Rows.Add(flower.Art, flower.Name, flower.Color, flower.Vid,
                //flower.prince, flower.quality, flower.amount);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            int Y = dataGridView1.CurrentCellAddress.Y;
            for (int j = 0; j < dataGridView1.ColumnCount; j++)
            {
                 str2 = str2 + dataGridView1.Rows[Y].Cells[j].Value.ToString() + ";";
            }
            Form1.list1.LoadStr1(str2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
            //Form6 Form = new Form6();
            //this.Hide();
            //Form.ShowDialog();
            //this.Show();
        }
    }
}

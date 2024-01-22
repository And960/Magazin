using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazin
{
    public partial class Form2 : Form
    {
        public static string nal;
           public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Show();
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 Form = new Form3();
            this.Hide();
            Form.ShowDialog();
            this.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            nal = textBox1.Text;
        }
    }
}


namespace Magazin
{
    partial class Form3
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Art = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Name1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Vid1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Color1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.prince1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.quality1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.amount1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Art,
            this.Name1,
            this.Vid1,
            this.Color1,
            this.prince1,
            this.quality1,
            this.amount1});
            this.dataGridView1.Location = new System.Drawing.Point(12, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(749, 405);
            this.dataGridView1.TabIndex = 0;
            // 
            // Art
            // 
            this.Art.HeaderText = "Артикул";
            this.Art.Name = "Art";
            this.Art.ReadOnly = true;
            // 
            // Name1
            // 
            this.Name1.HeaderText = "Наименование";
            this.Name1.Name = "Name1";
            this.Name1.ReadOnly = true;
            // 
            // Vid1
            // 
            this.Vid1.HeaderText = "Вид";
            this.Vid1.Name = "Vid1";
            this.Vid1.ReadOnly = true;
            // 
            // Color1
            // 
            this.Color1.HeaderText = "Цвет";
            this.Color1.Name = "Color1";
            this.Color1.ReadOnly = true;
            // 
            // prince1
            // 
            this.prince1.HeaderText = "Цена";
            this.prince1.Name = "prince1";
            this.prince1.ReadOnly = true;
            // 
            // quality1
            // 
            this.quality1.HeaderText = "Количество";
            this.quality1.Name = "quality1";
            this.quality1.ReadOnly = true;
            // 
            // amount1
            // 
            this.amount1.HeaderText = "Сумма";
            this.amount1.Name = "amount1";
            this.amount1.ReadOnly = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Список товара";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(767, 33);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(124, 31);
            this.button1.TabIndex = 2;
            this.button1.Text = "Добавить в корзину";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(767, 83);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(124, 31);
            this.button2.TabIndex = 3;
            this.button2.Text = "Корзина";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Name = "Form3";
            this.Text = "Список товаров";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Art;
        private System.Windows.Forms.DataGridViewTextBoxColumn Name1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Vid1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Color1;
        private System.Windows.Forms.DataGridViewTextBoxColumn prince1;
        private System.Windows.Forms.DataGridViewTextBoxColumn quality1;
        private System.Windows.Forms.DataGridViewTextBoxColumn amount1;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button button2;
    }
}
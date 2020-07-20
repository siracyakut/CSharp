using System;
using System.Windows.Forms;

namespace Password_Saver
{
    public partial class Form5 : Form
    {
        private Form3 form3;

        public Form5(Form3 frm)
        {
            InitializeComponent();
            form3 = frm;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            form3.dataGridView1.Rows[form3.secilenRow].SetValues(textBox1.Text, textBox2.Text);
            form3.dataGridView1.Update();
            this.Close();

            form3.secilenRow = -2;
        }

        private void Form5_Load(object sender, EventArgs e)
        {
            textBox1.Text = form3.dataGridView1.Rows[form3.secilenRow].Cells[0].Value.ToString();
            textBox2.Text = form3.dataGridView1.Rows[form3.secilenRow].Cells[1].Value.ToString();
        }
    }
}
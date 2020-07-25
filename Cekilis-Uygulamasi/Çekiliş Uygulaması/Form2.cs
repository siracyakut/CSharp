using System;
using System.Windows.Forms;

namespace Çekiliş_Uygulaması
{
    public partial class Form2 : Form
    {
        private Form1 form1;

        public Form2(Form1 frm)
        {
            InitializeComponent();
            this.form1 = frm;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._792753_cup_512x512;
            pictureBox2.Image = Properties.Resources._792753_cup_512x512;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;

            for (int i = 0; i < form1.kazanacakSayi; i++)
            {
                textBox1.Text += form1.kazananlar[i];
                if (i != form1.kazanacakSayi - 1) textBox1.Text += ", ";
            }

            for (int i = 0; i < form1.yedekSayi; i++)
            {
                textBox2.Text += form1.yedekler[i];
                if (i != form1.yedekSayi - 1) textBox2.Text += ", ";
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show();
            this.Hide();
        }
    }
}
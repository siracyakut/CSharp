using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (Control X in this.Controls)
            {
                if (X is TextBox)
                {
                    (X as TextBox).ReadOnly = true;
                }
            }

            Baslangic(Color.FromArgb(255, 128, 128, 255));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RenkSecimi();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            RenkSecimi();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Kopyala();
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Kopyala();
        }

        private void Kopyala()
        {
            Clipboard.SetText($"#{(pictureBox1.BackColor.ToArgb() & 0x00FFFFFF).ToString("X6")}");
            MessageBox.Show("Renk kodu kopyalandı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void RenkSecimi()
        {
            if ((colorDialog1.ShowDialog()) == DialogResult.OK)
            {
                pictureBox1.BackColor = colorDialog1.Color;
                pictureBox2.BackColor = colorDialog1.Color;

                Color renk = colorDialog1.Color;
                textBox1.Text = renk.R.ToString();
                textBox2.Text = renk.G.ToString();
                textBox3.Text = renk.B.ToString();

                label4.BackColor = renk;
                String code = (renk.ToArgb() & 0x00FFFFFF).ToString("X6");
                label4.Text = $"#{code}";
            }
        }

        private void Baslangic(Color renk)
        {
            pictureBox1.BackColor = renk;
            pictureBox2.BackColor = renk;

            textBox1.Text = renk.R.ToString();
            textBox2.Text = renk.G.ToString();
            textBox3.Text = renk.B.ToString();

            label4.BackColor = renk;
            String code = (renk.ToArgb() & 0x00FFFFFF).ToString("X6");
            label4.Text = $"#{code}";
        }
    }
}
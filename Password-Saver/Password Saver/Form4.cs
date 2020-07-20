using System;
using System.Windows.Forms;
using Microsoft.VisualBasic;

namespace Password_Saver
{
    public partial class Form4 : Form
    {
        Random rastgele = new Random();
        private Form3 form3;

        public Form4(Form3 form3)
        {
            InitializeComponent();
            this.form3 = form3;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string veri = Interaction.InputBox("Üretilecek şifre kaç haneli olsun?", "Şifre", "10");
            if (string.IsNullOrEmpty(veri) || string.IsNullOrEmpty(veri) || !IsNumeric(veri))
            {
                MessageBox.Show("Hatalı giriş yaptınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string harfler = "AaBbCcDdEeFfGgHhJjKkLMmNnOoPpRrSsTtUuVvYyZz1234567890";
            string sifre = "";

            for (int i = 0; i < Convert.ToInt32(veri); i++)
            {
                sifre += harfler[rastgele.Next(0, harfler.Length - 1)];
            }

            textBox2.Text = sifre;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string id = textBox1.Text;
            string pw = textBox2.Text;

            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(pw) || string.IsNullOrEmpty(pw))
            {
                MessageBox.Show("Hatalı giriş yaptınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            form3.dataGridView1.Rows.Add(id, pw);
            form3.dataGridView1.Update();
        }

        private static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }
    }
}
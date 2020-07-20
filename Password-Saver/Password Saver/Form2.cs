using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Password_Saver
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Kullanıcı adını boş bırakmayın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Şifreyi boş bırakmayın!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PasswordSaver");

            key.SetValue("id", textBox1.Text);
            key.SetValue("pw", textBox2.Text);
            key.Close();

            MessageBox.Show("Hesabınız başarıyla oluşturuldu!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            Form1 frm = new Form1();
            frm.Show();
        }
    }
}
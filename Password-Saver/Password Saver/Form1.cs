using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Password_Saver
{
    public partial class Form1 : Form
    {
        public string username = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PasswordSaver");

            if (key != null)
            {
                if (key.GetValue("hatirla").ToString() == "True")
                {
                    textBox1.Text = key.GetValue("hatirlananid").ToString();
                    textBox2.Text = key.GetValue("hatirlananpw").ToString();
                    checkBox1.Checked = true;
                }
                else
                {
                    textBox1.Text = string.Empty;
                    textBox2.Text = string.Empty;
                    checkBox1.Checked = false;
                }
                key.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 frm = new Form2();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
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

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PasswordSaver");

            if (key != null)
            {
                if (key.GetValue("id") == null || key.GetValue("pw") == null)
                {
                    MessageBox.Show("Yanlış giriş yaptınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (textBox1.Text == key.GetValue("id").ToString() && textBox2.Text == key.GetValue("pw").ToString())
                {
                    username = key.GetValue("id").ToString();
                    MessageBox.Show($"Giriş başarılı, hoş geldiniz {username}!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    RegistryKey key2 = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PasswordSaver");
                    key2.SetValue("hatirla", checkBox1.Checked);
                    key2.SetValue("hatirlananid", textBox1.Text);
                    key2.SetValue("hatirlananpw", textBox2.Text);
                    key2.Close();

                    Form3 frm = new Form3();
                    this.Hide();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Yanlış giriş yaptınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                key.Close();
            }
            else
            {
                MessageBox.Show("Yanlış giriş yaptınız!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PasswordSaver");

            key.SetValue("hatirla", checkBox1.Checked);
            key.SetValue("hatirlananid", textBox1.Text);
            key.SetValue("hatirlananpw", textBox2.Text);
            key.Close();

            Environment.Exit(1);
        }
    }
}
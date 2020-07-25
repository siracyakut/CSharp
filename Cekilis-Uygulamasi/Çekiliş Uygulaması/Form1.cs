using System;
using System.Windows.Forms;

namespace Çekiliş_Uygulaması
{
    public partial class Form1 : Form
    {
        public Random rand = new Random();
        public string[] katilanlar = new string[100];
        public string[] kazananlar = new string[100];
        public string[] yedekler = new string[100];
        public int[] secildi = new int[100];
        public int toplamKatilim = 0;
        public int kazanacakSayi = 0;
        public int yedekSayi = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnBelirle_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtKazanacak.Text) || string.IsNullOrWhiteSpace(txtKazanacak.Text) || !IsNumeric(txtKazanacak.Text) || Convert.ToInt32(txtKazanacak.Text) < 1)
            {
                MessageBox.Show("Kazanacak kişi sayısı girmediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else if (string.IsNullOrEmpty(txtYedek.Text) || string.IsNullOrWhiteSpace(txtYedek.Text) || !IsNumeric(txtYedek.Text))
            {
                MessageBox.Show("Yedek kişi sayısı girmediniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            toplamKatilim = 0;
            var katilimcilar = txtKatilimcilar.Text.Split('\n');
            for (int i = 0; i < katilimcilar.Length; i++)
            {
                if (!string.IsNullOrEmpty(katilimcilar[i]) && !string.IsNullOrWhiteSpace(katilimcilar[i]) && katilimcilar[i].Length > 0)
                {
                    katilanlar[toplamKatilim] = katilimcilar[i].Trim();
                    toplamKatilim++;
                    if (toplamKatilim >= katilanlar.Length)
                    {
                        MessageBox.Show($"{katilanlar.Length} sayısından fazla katılımcı giremezsiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Environment.Exit(1);
                        break;
                    }
                }
            }

            if (toplamKatilim < 2)
            {
                MessageBox.Show("Yetersiz katılımcı girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < secildi.Length; i++) secildi[i] = 0;
            kazanacakSayi = Convert.ToInt32(txtKazanacak.Text);
            yedekSayi = Convert.ToInt32(txtYedek.Text);

            if ((kazanacakSayi + yedekSayi) > toplamKatilim || kazanacakSayi == toplamKatilim)
            {
                MessageBox.Show("Kazanacak kişi sayısını veya yedek kişi sayısını fazla/hatalı yazdınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int i = 0; i < kazanacakSayi; i++)
            {
            bas:
                int buldum = rand.Next(0, toplamKatilim);
                if (secildi[buldum] == 1) goto bas;
                secildi[buldum] = 1;
                kazananlar[i] = katilanlar[buldum];
            }

            for (int i = 0; i < yedekSayi; i++)
            {
            bas:
                int buldum = rand.Next(0, toplamKatilim);
                if (secildi[buldum] == 1) goto bas;
                secildi[buldum] = 1;
                yedekler[i] = katilanlar[buldum];
            }

            Form2 frm = new Form2(this);
            frm.Show();
            this.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.unnamed;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(1);
        }

        private static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }
    }
}
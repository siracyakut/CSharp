using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _10FastFingers
{
    public partial class Form1 : Form
    {
        Random rand = new Random();
        static string[] kelimeler = { "konu", "birlikte", "verilmek", "duymak", "hem", "böyle", "devlet", "az", "yok", "söz", "düşünmek", "arkadaş", "ilk", "gitmek", "ancak", "üzerine", "ise", "tek", "yaş", "güzel", "su" };
        string[] anyK = new string[20];
        int[] used = new int[kelimeler.Length];
        int slot = 0, toplam = 0, dogru = 0, yanlis = 0, saniye = 60;
        string satir = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            KelimeYerlestir();
            label3.Text = $"Saniye: {saniye}";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = $"Doğru: {dogru} - Yanlış: {yanlis} - Toplam: {toplam}";
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            label3.Text = $"Saniye: {saniye}";
            if (saniye == 0)
            {
                timer2.Stop();
                if ((MessageBox.Show($"Doğru yazılan kelime: {dogru}\nYanlış yazılan kelime: {yanlis}\nToplam yazılan kelime: {toplam}\n\nDevam etmek istiyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    toplam = 0;
                    dogru = 0;
                    yanlis = 0;
                    saniye = 60;
                    slot = 0;
                    label3.Text = $"Saniye: {saniye}";
                    KelimeYerlestir();
                }
                else
                {
                    Environment.Exit(1);
                }
                return;
            }
            saniye--;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            timer2.Enabled = true;
            if (textBox1.Text.Length > 0)
            {
                if (textBox1.Text[textBox1.Text.Length - 1].ToString() == " ")
                {
                    textBox1.Text = textBox1.Text.Replace(" ", string.Empty);
                    if (textBox1.Text == anyK[slot])
                    {
                        satir = satir.Replace($"{anyK[slot]}", $"<span style=\"color: Green; \">{anyK[slot]}</span>");
                        EkranGuncelle(satir);

                        dogru++;
                    }
                    else
                    {
                        satir = satir.Replace($"{anyK[slot]}", $"<span style=\"color: Red; \">{anyK[slot]}</span>");
                        EkranGuncelle(satir);

                        yanlis++;
                    }

                    toplam++;
                    slot++;
                    if (slot == 20)
                    {
                        KelimeYerlestir();
                        slot = 0;
                    }
                    textBox1.Text = string.Empty;
                    textBox1.Focus();
                }
            }
        }

        private void KelimeYerlestir()
        {
            for (int i = 0; i < anyK.Length; i++) anyK[i] = string.Empty;
            for (int i = 0; i < used.Length; i++) used[i] = 0;
            satir = "<body style=\"background - color:appworkspace; \"><span style=\"font-size:20px\">";
            for (int i = 0; i < 20; i++)
            {
            tekrar:
                if (i == 10) satir += "\n";
                int x = rand.Next(0, kelimeler.Length);
                if (used[x] == 1) goto tekrar;
                used[x] = 1;
                satir += $"{kelimeler[x]} ";
                anyK[i] = kelimeler[x];
            }
            satir += "</span></body>";

            EkranGuncelle(satir);
        }

        private void EkranGuncelle(string yaz)
        {
            webBrowser1.Navigate("about:blank");
            webBrowser1.Document.OpenNew(false);
            webBrowser1.Document.Write(yaz);
            webBrowser1.Refresh();
        }

        private void SecimEngelle(object sender, HtmlElementEventArgs e)
        {
            webBrowser1.Document.ExecCommand("Unselect", true, null);
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            webBrowser1.Document.Click += new HtmlElementEventHandler(SecimEngelle);
            webBrowser1.Document.MouseMove += new HtmlElementEventHandler(SecimEngelle);
        }
    }
}
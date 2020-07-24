using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using Newtonsoft.Json;
using VideoLibrary;

namespace YouTube_Search_and_Downloader
{
    public partial class Form1 : Form
    {
        string[] linkler = new string[10];
        string[] idler = new string[10];
        int secilen = -1;

        public Form1()
        {
            InitializeComponent();
        }

        public class veri
        {
            [JsonProperty(PropertyName = "id")]
            public string ID { get; set; }

            [JsonProperty(PropertyName = "title")]
            public string Isim { get; set; }

            [JsonProperty(PropertyName = "full_link")]
            public string Link { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text.Length < 3)
            {
                MessageBox.Show("Arama kelimeniz 2 karakterden uzun olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string adres = $"http://51.195.39.75/youtube/search.php?q={textBox1.Text}";
            System.Net.WebClient wc = new System.Net.WebClient();
            string webSatir = wc.DownloadString(adres);

            if (webSatir.Contains("\"error\":true,"))
            {
                MessageBox.Show("Arama yapılırken bir hata ile karşılaşıldı. Lütfen daha sonra tekrar deneyin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            webSatir = webSatir.Replace("{\"error\":false,\"message\":null,\"results\":", string.Empty);
            webSatir = webSatir.Remove(webSatir.Length - 1, 1);
            var degisken = JsonConvert.DeserializeObject<List<veri>>(webSatir);

            listBox1.Items.Clear();
            int slot = 0;
            foreach (var veri in degisken)
            {
                veri.Isim = veri.Isim.Replace("&#39;", "'");
                veri.Isim = veri.Isim.Replace("&amp;", "&");
                listBox1.Items.Add(veri.Isim);
                linkler[slot] = veri.Link;
                idler[slot] = veri.ID;
                slot++;
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || listBox1.Items.Count < 1)
            {
                MessageBox.Show("Önce arama yaparak listeden bir şarkı seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var youTube = YouTube.Default;
            var video = youTube.GetVideo(linkler[listBox1.SelectedIndex]);
            secilen = listBox1.SelectedIndex;
            if ((MessageBox.Show($"{video.Title}\nDosyanın indirilmesini istiyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
            {
                backgroundWorker1.RunWorkerAsync();
                MessageBox.Show("Video indiriliyor, lütfen bekleyin. İndirme tamamlandığında bildirim verilecektir.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            var youTube = YouTube.Default;
            var video = youTube.GetVideo(linkler[secilen]);
            File.WriteAllBytes($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\{video.FullName}", video.GetBytes());
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Video başarıyla indirildi, masaüstüne kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.ImageLocation = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Youtube%28amin%29.png";
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1 || listBox1.Items.Count < 1) return;
            pictureBox1.ImageLocation = $"http://i3.ytimg.com/vi/{idler[listBox1.SelectedIndex]}/maxresdefault.jpg";
        }
    }
}
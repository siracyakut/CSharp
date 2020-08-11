using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using YouTubeSearch;

namespace YouTube_Search_and_Downloader
{
    public partial class Form1 : Form
    {
        List<string> linkler = new List<string>();
        public string dosya = "";
        public long boyut = 0;

        public Form1()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Internet bağlantınız kontrol ediliyor, lütfen bekleyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (!InternetVarMi())
            {
                MessageBox.Show("Internet bağlantınız olmadan bu uygulamayı kullanamazsınız.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
            pictureBox1.ImageLocation = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Youtube%28amin%29.png";
            label2.Text = "Durum: 0%";
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrWhiteSpace(textBox1.Text) || textBox1.Text.Length < 3)
            {
                MessageBox.Show("Arama kelimeniz 2 karakterden uzun olmalıdır!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AramaYap(this);
            MessageBox.Show("Lütfen bekleyin, sonuçlar birazdan listelenecektir...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count < 1 || listBox1.SelectedIndex == -1) return;

            string url = linkler[listBox1.SelectedIndex];
            url = url.Replace("http://www.youtube.com/watch?v=", string.Empty);
            pictureBox1.ImageLocation = $"http://i3.ytimg.com/vi/{url}/maxresdefault.jpg";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count < 1 || listBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Önce arama yaparak listeden bir şarkı seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show("Indirme birazdan başlayacak, lütfen bekleyin...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            label2.Text = "Durum: 0%";
            progressBar1.Value = 0;
            progressBar1.Maximum = 100;

            IndirmeYap(linkler[listBox1.SelectedIndex]);
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1.PerformClick();
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private static async void AramaYap(Form1 frm)
        {
            frm.listBox1.Items.Clear();
            frm.linkler.Clear();
            Log.setMode(false);

            VideoSearch videolar = new VideoSearch();
            var veriler = await videolar.GetVideos(frm.textBox1.Text, 1);

            foreach (var veri in veriler)
            {
                string ekle = frm.IsimDuzenle(veri.getTitle());
                frm.listBox1.Items.Add(ekle);
                frm.linkler.Add(veri.getUrl());
            }
        }

        private void IndirmeYap(string link)
        {
            Log.setMode(false);

            IEnumerable<VideoInfo> videoBilgileri = DownloadUrlResolver.GetDownloadUrls(link, false);

            VideoIndir(videoBilgileri);
        }

        private void VideoIndir(IEnumerable<VideoInfo> videoBilgileri)
        {
            VideoInfo video = videoBilgileri
                .First(bilgi => bilgi.VideoType == VideoType.Mp4 && bilgi.Resolution == 360);

            if (video.RequiresDecryption)
            {
                DownloadUrlResolver.DecryptDownloadUrl(video);
            }

            string ism = video.Title;
            ism = Helper.makeFilenameValid(ism).Replace("/", "")
                                                .Replace(".", "")
                                                .Replace("|", "")
                                                .Replace("?", "")
                                                .Replace("<", "")
                                                .Replace(">", "")
                                                .Replace("\\", "")
                                                .Replace("*", "")
                                                .Replace("!", "");

            VideoDownloader dl = new VideoDownloader();
            dl.DownloadFile(video.DownloadUrl, ism, true, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), video.VideoExtension);

            dosya = ism + video.VideoExtension;
            boyut = video.FileSize;
            timer1.Start();
        }

        private string IsimDuzenle(string isim)
        {
            string satir = isim;

            satir = satir.Replace("Ä±", "ı")
                        .Replace("Ä°", "İ")
                        .Replace("Ã‡", "Ç")
                        .Replace("Ã§", "ç")
                        .Replace("Ã¼", "ü")
                        .Replace("Ãœ", "Ü")
                        .Replace("Ã¶", "ö")
                        .Replace("Ã–", "Ö")
                        .Replace("ÅŸ", "ş")
                        .Replace("Å", "Ş")
                        .Replace("ÄŸ", "ğ")
                        .Replace("Ä", "Ğ");

            return satir;
        }

        private bool InternetVarMi()
        {
            try
            {
                using (var client = new System.Net.WebClient())
                using (client.OpenRead("http://google.com/generate_204"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            long length = new FileInfo($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\{dosya}").Length;
            long hesapla = ((length * 100) / boyut);

            progressBar1.Value = Convert.ToInt32(hesapla);
            label2.Text = $"Durum: {hesapla}%";

            if (hesapla >= 100)
            {
                timer1.Stop();
                MessageBox.Show("Video indirildi ve masaüstüne kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
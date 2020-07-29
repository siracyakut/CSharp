using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using YouTubeSearch;
using WrapYoutubeDl;

namespace YouTube_Search_and_Downloader
{
    public partial class Form1 : Form
    {
        public string[] linkler = new string[60];
        public string dosya = "";
        public string dLink = "";

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

            using (FileStream MsiFile = new FileStream(@"C:\youtube-dl.exe", FileMode.Create))
            {
                MsiFile.Write(Properties.Resources.youtube_dl, 0, Properties.Resources.youtube_dl.Length);
            }
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

        private static async void AramaYap(Form1 frm)
        {
            int slot = 0;
            frm.listBox1.Items.Clear();
            Log.setMode(false);

            VideoSearch videolar = new VideoSearch();
            var veriler = await videolar.GetVideos(frm.textBox1.Text, 1);

            foreach (var veri in veriler)
            {
                string ekle = frm.IsimDuzenle(veri.getTitle());
                frm.listBox1.Items.Add(ekle);
                frm.linkler[slot] = veri.getUrl();
                slot++;
            }
        }

        private void IndirmeYap(string link)
        {
            Log.setMode(false);

            IEnumerable<VideoInfo> videoBilgileri = DownloadUrlResolver.GetDownloadUrls(link, false);

            VideoIndir(videoBilgileri);
            Form1 form = (Form1)Application.OpenForms["Form1"];
            form.dLink = link;
        }

        private void VideoIndir(IEnumerable<VideoInfo> videoBilgileri)
        {
            VideoInfo video = videoBilgileri
                .First(bilgi => bilgi.VideoType == VideoType.Mp4 && bilgi.Resolution == 360);

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

            Form1 form = (Form1)Application.OpenForms["Form1"];
            form.dosya = ism;
            form.backgroundWorker1.RunWorkerAsync();
        }

        private string IsimDuzenle(string isim)
        {
            string satir = isim;

            satir = satir.Replace("Ä±", "ı");
            satir = satir.Replace("Ä°", "İ");
            satir = satir.Replace("Ã‡", "Ç");
            satir = satir.Replace("Ã§", "ç");
            satir = satir.Replace("Ã¼", "ü");
            satir = satir.Replace("Ãœ", "Ü");
            satir = satir.Replace("Ã¶", "ö");
            satir = satir.Replace("Ã–", "Ö");
            satir = satir.Replace("ÅŸ", "ş");
            satir = satir.Replace("Å", "Ş");
            satir = satir.Replace("ÄŸ", "ğ");
            satir = satir.Replace("Ä", "Ğ");

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

        private void Form1_Shown(object sender, EventArgs e)
        {
            textBox1.Focus();
        }

        private void downloader_FinishedDownload(object sender, DownloadEventArgs e)
        {
            MessageBox.Show("Video indirildi ve masaüstüne kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void downloader_ProgressDownload(object sender, ProgressEventArgs e)
        {
            Form1 form = (Form1)Application.OpenForms["Form1"];
            form.progressBar1.Value = Convert.ToInt32(e.Percentage);
            form.label2.Text = $"Durum: {e.Percentage}%";
        }

        private void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)
        {
            string yol = $@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\{dosya}.mp3";
            if (File.Exists(yol))
            {
                File.Delete(yol);
            }

            var downloader = new AudioDownloader(dLink, dosya, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), @"C:\");
            downloader.ProgressDownload += downloader_ProgressDownload;
            downloader.FinishedDownload += downloader_FinishedDownload;
            downloader.Download();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (File.Exists(@"C:\youtube-dl.exe"))
            {
                File.Delete(@"C:\youtube-dl.exe");
            }
        }
    }
}
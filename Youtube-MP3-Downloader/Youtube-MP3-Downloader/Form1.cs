using System;
using System.ComponentModel;
using System.Net;
using System.Windows.Forms;

namespace Youtube_MP3_Downloader
{
	public partial class Form1 : Form
	{
		Random rand = new Random();
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			int pos;
			string url = textBox1.Text;

			if (string.IsNullOrEmpty(url) || string.IsNullOrWhiteSpace(url))
			{
				MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (url.Contains("youtube.com"))
			{
				if (!url.Contains("="))
				{
					MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				pos = url.IndexOf("=");
			}
			else if (url.Contains("youtu.be"))
			{
				if (!url.Contains("/"))
				{
					MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
				pos = url.LastIndexOf("/");
			}
			else
			{
				MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			if (url.Substring(pos + 1, (url.Length - pos - 1)).Length < 5)
			{
				MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			label2.Text = "Durum: 0%";
			string downURL = $"https://www.convertmp3.io/fetch/?video={url}";
			DosyaIndir(downURL, Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), randomIsim());
		}

		public string randomIsim()
		{
			string isim = "Muzik_";
			string harfler = "abcdefghijklmnoprstzvyz";
			for (int i = 0; i < 4; i++)
			{
				isim += harfler[rand.Next(0, harfler.Length)];
			}
			isim += ".mp3";
			return isim;
		}

		public void DosyaIndir(string URL, string IndirilecekDizin, string DosyaAdi)
		{
			WebClient webClient = new WebClient();
			webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(Completed);
			webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(ProgressChanged);
			webClient.DownloadFileAsync(new Uri(URL), IndirilecekDizin + "/" + DosyaAdi);
		}

		public static void ProgressChanged(object sender, DownloadProgressChangedEventArgs e)
		{
			label2.Text = $"Durum: {e.ProgressPercentage}%";
		}

		public static void Completed(object sender, AsyncCompletedEventArgs e)
		{
			label2.Text = "Indirme Tamamlandi.";
			MessageBox.Show("Indirme tamamlandı, dosya masaüstünüze kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			label2.Text = "Durum: 0%";
		}
	}
}
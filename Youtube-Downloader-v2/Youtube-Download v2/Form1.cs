using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VideoLibrary;

namespace Youtube_Download_v2
{
	public partial class Form1 : Form
	{
		public string downURL = "";

		public Form1()
		{
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;
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

			string vID = url.Substring(pos + 1, (url.Length - pos - 1));

			if (vID.Length < 5)
			{
				MessageBox.Show("Hatalı link girdiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			downURL = url;
			pictureBox1.ImageLocation = $"http://i3.ytimg.com/vi/{vID}/maxresdefault.jpg";

			var youTube = YouTube.Default;
			var video = youTube.GetVideo(url);
			if ((MessageBox.Show($"Video bulundu, {video.Title}\nDosyanın indirilmesini istiyor musunuz?", "Bilgi", MessageBoxButtons.YesNo, MessageBoxIcon.Information)) == DialogResult.Yes)
			{
				backgroundWorker1.RunWorkerAsync();
				MessageBox.Show("Video indiriliyor, lütfen bekleyin.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			pictureBox1.ImageLocation = "https://upload.wikimedia.org/wikipedia/commons/9/9f/Youtube%28amin%29.png";
		}

		private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
		{
			var youTube = YouTube.Default;
			var video = youTube.GetVideo(downURL);
			File.WriteAllBytes($@"{Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)}\{video.FullName}", video.GetBytes());
		}

		private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
		{
			MessageBox.Show("Video başarıyla indirildi, masaüstüne kaydedildi.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}
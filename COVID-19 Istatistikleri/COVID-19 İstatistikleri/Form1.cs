using System;
using System.Collections.Generic;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json;

namespace COVID_19_İstatistikleri
{
    public partial class Form1 : Form
    {
        public class veri
        {
            [JsonProperty(PropertyName = "country")]
            public string Ulke { get; set; }

            [JsonProperty(PropertyName = "totalcases")]
            public string ToplamVaka { get; set; }

            [JsonProperty(PropertyName = "newCases")]
            public string YeniVaka { get; set; }

            [JsonProperty(PropertyName = "totaldeaths")]
            public string ToplamOlum { get; set; }

            [JsonProperty(PropertyName = "newDeaths")]
            public string YeniOlum { get; set; }

            [JsonProperty(PropertyName = "totalRecovered")]
            public string ToplamIyilesen { get; set; }

            [JsonProperty(PropertyName = "activeCases")]
            public string AktifVaka { get; set; }
        }

        string cek = "";
        List<veri> degisken = new List<veri>();

        public Form1()
        {
            InitializeComponent();

            MessageBox.Show("Lütfen bekleyin, istatistikler yüklenecek...", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            var client = new RestClient("https://api.collectapi.com/corona/countriesData");
            var request = new RestRequest(Method.GET);
            request.AddHeader("authorization", "apikey 5nfzqcvrBYQLsprPklm6VW:0BXlS1ExwVoC8WC3mcmlSt");
            request.AddHeader("content-type", "application/json");
            IRestResponse response = client.Execute(request);

            if (!response.Content.Contains("\"success\":true,"))
            {
                MessageBox.Show("Verileri çekerken bir hata oluştu, program kapatılacak.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
                return;
            }

            cek = response.Content;
            cek = cek.Replace("{\"success\":true,\"result\":", string.Empty);
            cek = cek.Remove(cek.Length - 1, 1);

            degisken = JsonConvert.DeserializeObject<List<veri>>(cek);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources.fddb72a34fb15d467431b4f352e35786;
            foreach (var veri in degisken)
            {
                comboBox1.Items.Add(veri.Ulke);
                if (string.IsNullOrEmpty(veri.ToplamVaka) || string.IsNullOrWhiteSpace(veri.ToplamVaka))
                {
                    veri.ToplamVaka = "x";
                }
                if (string.IsNullOrEmpty(veri.YeniVaka) || string.IsNullOrWhiteSpace(veri.YeniVaka))
                {
                    veri.YeniVaka = "x";
                }
                if (string.IsNullOrEmpty(veri.ToplamOlum) || string.IsNullOrWhiteSpace(veri.ToplamOlum))
                {
                    veri.ToplamOlum = "x";
                }
                if (string.IsNullOrEmpty(veri.YeniOlum) || string.IsNullOrWhiteSpace(veri.YeniOlum))
                {
                    veri.YeniOlum = "x";
                }
                if (string.IsNullOrEmpty(veri.ToplamIyilesen) || string.IsNullOrWhiteSpace(veri.ToplamIyilesen))
                {
                    veri.ToplamIyilesen = "x";
                }
                if (string.IsNullOrEmpty(veri.AktifVaka) || string.IsNullOrWhiteSpace(veri.AktifVaka))
                {
                    veri.AktifVaka = "x";
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (var veri in degisken)
            {
                if (veri.Ulke == comboBox1.Text)
                {
                    label1.Text = veri.ToplamVaka.ToString();
                    label6.Text = veri.YeniVaka.ToString();
                    label10.Text = veri.ToplamOlum.ToString();
                    label8.Text = veri.YeniOlum.ToString();
                    label14.Text = veri.ToplamIyilesen.ToString();
                    label12.Text = veri.AktifVaka.ToString();
                    break;
                }
            }
        }
    }
}
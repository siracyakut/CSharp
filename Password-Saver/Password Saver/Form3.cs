using Microsoft.Win32;
using System;
using System.Windows.Forms;

namespace Password_Saver
{
    public partial class Form3 : Form
    {
        public int secilenRow = -2;

        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            HesapKaydet();
            Environment.Exit(1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 frm = new Form4(this);
            frm.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            secilenRow = e.RowIndex;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (secilenRow < 0)
            {
                MessageBox.Show("Önce bir sütun seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Form5 frm = new Form5(this);
            frm.ShowDialog();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }
            HesapYukle();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (secilenRow < 0)
            {
                MessageBox.Show("Önce bir sütun seçmelisiniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if ((MessageBox.Show($"{dataGridView1.Rows[secilenRow].Cells[0].Value.ToString()} ID'li hesabınızı silmek istiyor musunuz?", "Soru", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
            {
                dataGridView1.Rows.Remove(dataGridView1.Rows[secilenRow]);
                dataGridView1.Update();
                HesapKaydet();
            }

            secilenRow = -2;
        }

        private string HesapCek()
        {
            string hesaplar = "";

            if (dataGridView1.Rows.Count > 0)
            {
                for (int i = 0; i < dataGridView1.Rows.Count; i++)
                {
                    hesaplar += dataGridView1.Rows[i].Cells[0].Value.ToString() + ",";
                    hesaplar += dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (i != dataGridView1.Rows.Count - 1) hesaplar += "|";
                }
            }

            return hesaplar;
        }

        private void HesapYukle()
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\PasswordSaver");

            if (key != null && key.GetValue("hesaplar") != null)
            {
                string str = key.GetValue("hesaplar").ToString();
                if (string.IsNullOrEmpty(str) || string.IsNullOrWhiteSpace(str)) return;

                var hesaplar_ayri = str.Split('|');
                for (int i = 0; i < hesaplar_ayri.Length; i++)
                {
                    var hesap = hesaplar_ayri[i].Split(',');
                    dataGridView1.Rows.Add(hesap[0], hesap[1]);
                    dataGridView1.Update();
                }

                key.Close();
            }
        }

        private void HesapKaydet()
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\PasswordSaver");
            key.SetValue("hesaplar", HesapCek());
            key.Close();
        }
    }
}
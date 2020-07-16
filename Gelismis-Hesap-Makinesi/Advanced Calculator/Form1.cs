using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Advanced_Calculator
{
    public partial class Form1 : Form
    {
        bool first = true;
        string[] operators = { "+", "-", "/", "*", "=", "x", ",", "." };

        public Form1()
        {
            InitializeComponent();
        }

        private void Rakamlar(object sender, EventArgs e)
        {
            Button tiklanan = (Button)sender;

            if (tiklanan.Text == "=")
            {
                if (IsLastOperator()) return;
                if (first) return;

                DataTable dt = new DataTable();
                var sonuc = dt.Compute(label1.Text.Replace("x", "*"), "");
                label1.Text += $"={sonuc}";
                label1.Text = SatirDuzenle(label1.Text);
                first = true;
                return;
            }
            else if (tiklanan.Text == "<<")
            {
                if (first == true)
                {
                    label1.Text = "0";
                    return;
                }
                if (label1.Text.Length >= 2) label1.Text = label1.Text.Remove(label1.Text.Length - 1, 1);
                return;
            }
            else if (tiklanan.Text == "C" || tiklanan.Text == "CE")
            {
                label1.Text = "0";
                first = true;
                return;
            }
            else if (tiklanan.Text == "√")
            {
                if (!IsNumeric(label1.Text)) return;
                label1.Text += $"√={Math.Sqrt(Convert.ToDouble(label1.Text))}";
                first = true;
                return;
            }
            else if (tiklanan.Text == "x²")
            {
                if (!IsNumeric(label1.Text)) return;
                label1.Text += $"²={Math.Pow(Convert.ToDouble(label1.Text), 2)}";
                first = true;
                return;
            }
            else if (tiklanan.Text == "1/x")
            {
                if (!IsNumeric(label1.Text)) return;
                label1.Text = $"1 / {label1.Text} = {1 / Convert.ToDouble(label1.Text)}";
                first = true;
                return;
            }
            else if (tiklanan.Text == ".")
            {
                if (label1.Text.Contains(".")) return;
            }

            if (IsOperator(tiklanan.Text) && first) return;
            if (IsOperator(tiklanan.Text)) if (IsLastOperator()) return;

            if (first)
            {
                first = false;
                label1.Text = string.Empty;
            }

            if (tiklanan.Text == "0")
            {
                if (label1.Text == "0")
                {
                    label1.Text = "0";
                }
                else
                {
                    label1.Text += tiklanan.Text;
                }
            }
            else
            {
                label1.Text += tiklanan.Text;
            }
        }

        private bool IsLastOperator()
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (label1.Text[label1.Text.Length - 1].ToString() == operators[i])
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsOperator(string satir)
        {
            for (int i = 0; i < operators.Length; i++)
            {
                if (satir == operators[i])
                {
                    return true;
                }
            }
            return false;
        }

        private static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }

        private string SatirDuzenle(string str)
        {
            string satir = str;

            var numbers = Regex.Matches(satir, @"(\d+)");
            var symbols = Regex.Matches(satir, @"\D");

            long[] sayilar = new long[50];
            string[] operatorler = new string[50];
            int slot = 0;
            int slot2 = 0;

            foreach (Match number in numbers)
            {
                sayilar[slot] = Convert.ToInt64(number.Value);
                slot++;
            }

            foreach (Match symbol in symbols)
            {
                if (string.IsNullOrWhiteSpace(symbol.Value))
                    continue;

                operatorler[slot2] = symbol.Value.Trim();
                slot2++;
            }

            for (int i = 0; i < operatorler.Length; i++)
            {
                if (operatorler[i] == "x")
                {
                    satir = satir.Replace($"{sayilar[i]}x{sayilar[i + 1]}", $"({sayilar[i]}x{sayilar[i + 1]})");
                }
                else if (operatorler[i] == "/")
                {
                    satir = satir.Replace($"{sayilar[i]}/{sayilar[i + 1]}", $"({sayilar[i]}/{sayilar[i + 1]})");
                }
            }

            return satir;
        }
    }
}
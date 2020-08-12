using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Timers;
using WindowsInput;
using WindowsInput.Native;
using Timer = System.Timers.Timer;

namespace DG_Chat_Hack
{
    class Program
    {
        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        static InputSimulator sim = new InputSimulator();
        static int total_tests;
        static Timer t;

        static void Main(string[] args)
        {
            if (Process.GetProcessesByName("DG Chat Hack").Length > 1)
            {
                Environment.Exit(1);
                return;
            }

            if (Process.GetProcessesByName("gta_sa").Length <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HATA: Önce SA:MP açmalısınız!");
                Console.ReadKey();
                return;
            }

            if (!File.Exists($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\GTA San Andreas User Files\SAMP\chatlog.txt"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("HATA: chatlog.txt dosyası yok!");
                Console.ReadKey();
                return;
            }

            t = new Timer(100);
            t.Elapsed += new ElapsedEventHandler(Cheat);
            t.Start();

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("   __ _                      _   _ _   _ _   _ ");
            Console.WriteLine("  / _| |                    | \\ | | \\ | | \\ | |");
            Console.WriteLine(" | |_| | __ _ _ __ ___  ___ |  \\| |  \\| |  \\| |");
            Console.WriteLine(" |  _| |/ _` | '__/ _ \\/ _ \\| . ` | . ` | . ` |");
            Console.WriteLine(" | | | | (_| | | |  __/ (_) | |\\  | |\\  | |\\  |");
            Console.WriteLine(" |_| |_|\\__,_|_|  \\___|\\___/|_| \\_|_| \\_|_| \\_|" + Environment.NewLine + Environment.NewLine);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Hack is activated, go back to game." + Environment.NewLine);
            Console.ReadKey();
        }

        static void Cheat(object o, ElapsedEventArgs a)
        {
            if (GetActiveWindowTitle() != "GTA:SA:MP") return;

            StreamReader sr = new StreamReader($@"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\GTA San Andreas User Files\SAMP\chatlog.txt", Encoding.Default, false);
            List<string> satirlar = new List<string>();
            string ekle;

            while ((ekle = sr.ReadLine()) != null)
            {
                satirlar.Add(ekle);
            }

            sr.Close();

            satirlar.Reverse();

            string cozumle = satirlar[1];

            if (cozumle.Contains("] {A2FF00}Hız Testi » {FFFFFF}Verdiğim"))
            {
                cozumle = cozumle.Remove(0, 11);
                cozumle = cozumle.Replace("{A2FF00}Hız Testi » {FFFFFF}Verdiğim {A2FF00}", "")
                        .Replace(" {FFFFFF}harflerini ilk yazan {A2FF00}", "")
                        .Replace(" {FFFFFF}kazanacak!", "");
                cozumle = cozumle.Remove(13, cozumle.Length - 13);

                sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                sim.Keyboard.TextEntry(cozumle);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                total_tests++;
                Console.WriteLine($"Test {total_tests} successfully winned, reaction answer: {cozumle}");
            }
            else if (cozumle.Contains("] {E5FF00}Matematik » {FFFFFF}Verdiğim"))
            {
                cozumle = cozumle.Remove(0, 11);
                cozumle = cozumle.Replace("{E5FF00}Matematik » {FFFFFF}Verdiğim {E5FF00}", "");
                int bul = cozumle.IndexOf("{");
                cozumle = cozumle.Remove(bul, cozumle.Length - bul);

                DataTable dt = new DataTable();
                var sonuc = dt.Compute(cozumle, "");

                sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                sim.Keyboard.TextEntry(sonuc.ToString());
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                total_tests++;
                Console.WriteLine($"Test {total_tests} successfully winned, math answer: {sonuc}");
            }
            else if (cozumle.Contains("] Dark Gaming » {FFFFFF}Matematik cevabı"))
            {
                cozumle = cozumle.Remove(0, 11);
                cozumle = cozumle.Replace("Dark Gaming » {FFFFFF}Matematik cevabı >> ", "");

                sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                sim.Keyboard.TextEntry(cozumle);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                total_tests++;
                Console.WriteLine($"Test {total_tests} successfully winned, math answer: {cozumle}");
            }
            else if (cozumle.Contains("] Dark Gaming » {FFFFFF}Lotto cevabı >>"))
            {
                t.Stop();
                cozumle = cozumle.Remove(0, 11);
                cozumle = cozumle.Replace("Dark Gaming » {FFFFFF}Lotto cevabı >> ", "");

                int cevap = Convert.ToInt32(cozumle) + 2;

                sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                sim.Keyboard.TextEntry("/lottokatil");
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);

                int ab = 0;

                while (ab < cevap)
                {
                    sim.Keyboard.KeyDown(VirtualKeyCode.DOWN);
                    Thread.Sleep(50);
                    ab++;
                }

                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
                sim.Keyboard.KeyPress(VirtualKeyCode.DOWN);

                total_tests++;
                Console.WriteLine($"Test {total_tests} successfully winned, lotto answer: {cevap - 1}");

                t.Start();
            }
        }

        static private string GetActiveWindowTitle()
        {
            const int charDegerleri = 256;
            StringBuilder satir = new StringBuilder(charDegerleri);
            IntPtr h = GetForegroundWindow();

            if (GetWindowText(h, satir, charDegerleri) > 0)
            {
                return satir.ToString();
            }

            return null;
        }
    }
}
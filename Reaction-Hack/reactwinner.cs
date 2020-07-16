using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;
using System.IO;
using System.Runtime.InteropServices;
using WindowsInput.Native;
using WindowsInput;

namespace ConsoleApp2
{
    class Program
    {

        static InputSimulator sim = new InputSimulator();

        static void Main(string[] args)
        {
            Timer t = new Timer(100);
            t.Elapsed += new ElapsedEventHandler(rwinner);
            t.Start();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Reaction Winner v1.0 Loaded by flareoNNN.");
            Console.WriteLine("You can turn on the game now.");
            Console.ReadKey();
        }

        static void rwinner(object o, ElapsedEventArgs a)
        {
            string belgelerim = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<string> satirlar = new List<string>();
            StreamReader sr = new StreamReader($@"{belgelerim}\GTA San Andreas User Files\SAMP\chatlog.txt", Encoding.GetEncoding("iso-8859-9"), false);
            string oku;

            while ((oku = sr.ReadLine()) != null)
            {
                satirlar.Add(oku);
            }

            satirlar.Reverse();

            string cek = satirlar[1].ToString();

            if (cek.Contains("GL FastFingers"))
            {
                string fix1 = cek.Replace(" {A2FF00}GL FastFingers » {FFFFFF}Verdiğim {A2FF00}", "");
                string fix2 = fix1.Replace(" {FFFFFF}harflerini ilk yazan {A2FF00}", "");
                string fix3 = fix2.Replace(" {FFFFFF}kazanacak!", "");
                string fix4 = fix3.Remove(0, 10);
                int xd = fix4.Length - 12;
                string fix5 = fix4.Remove(13, xd - 1);

                sim.Keyboard.KeyPress(VirtualKeyCode.VK_T);
                sim.Keyboard.TextEntry(fix5);
                sim.Keyboard.KeyPress(VirtualKeyCode.RETURN);
            }
            sr.Close();
        }
    }
}

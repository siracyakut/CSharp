using System;
using System.Diagnostics;

namespace Processes
{
    class Program
    {
        static void Main(string[] args)
        {
            Process[] hafiza = Process.GetProcesses();
            foreach (Process prc in hafiza)
            {
                Echo("Uygulama: ", ConsoleColor.Green);
                Echo($"{prc.ProcessName}\n", ConsoleColor.White);
            }
            Console.ReadKey();
        }
        static void Echo(string text, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ResetColor();
        }
    }
}
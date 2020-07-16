using System;

namespace Faktoriyel
{
    class Program
    {
        static void Main(string[] args)
        {
        start:
            Console.Write("Girdi: ");
            string number = Console.ReadLine();
            if (IsNumeric(number))
            {
                int sayi = Convert.ToInt32(number);
                int toplam = 1;
                for (int i = 1; i <= sayi; i++)
                {
                    toplam *= i;
                }
                Console.WriteLine($"{number} faktöriyeli: {toplam}");
                goto start;
            }
            else
            {
                Console.WriteLine("Hatalı giriş.");
                goto start;
            }

            Console.ReadKey();
        }

        public static bool IsNumeric(string input)
        {
            int test;
            return int.TryParse(input, out test);
        }
    }
}
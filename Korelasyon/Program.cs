using System;

namespace Korelasyon_
{
    class Program
    {
        /*
         * 
        Düzey             >>>  İlişki
        "0.00 -  0.25"          >>> Zayıf  
        "0.26 -  0.49"          >>> Düşük   
        "0.50 -  0.69"          >>> Orta 
        "0.70 -  0.89"          >>> Yüksek 
        "0.90 -  1.00"          >>> Çok Yüksek

        Yön
        Korelasyon katsayısı negatif ise negatif yönde ilişki, pozitif ise pozitif yönde ilişki var demektir.
        
             */
        static void Main(string[] args)
        {
            double r = 0;

            int[] x = { 7, 8, 6, 7, 8, 9, 8, 9, 10, 8 };
            int[] y = { 10, 8, 8, 9, 7, 6, 6, 5, 6, 5 };

            float ortalamaX = 0;
            for (int i = 0; i < x.Length; i++)
            {
                ortalamaX += x[i];
            }

            ortalamaX = (ortalamaX / x.Length);

            float ortalamaY = 0;
            for (int i = 0; i < y.Length; i++)
            {
                ortalamaY += y[i];
            }
            ortalamaY = (ortalamaY / y.Length);

            Console.WriteLine("X verisinin ortalaması : " + ortalamaX);
            Console.WriteLine("Y verisinin ortalaması : " + ortalamaY);

            double hesapla = 0, findx = 0, findy = 0;
            for (int i = 0; i < x.Length; i++)
            {
                hesapla += (x[i] - ortalamaX) * (y[i] - ortalamaY);

                findx += Math.Pow((x[i] - ortalamaX), 2);
                findy += Math.Pow((y[i] - ortalamaY), 2);
            }

            r = (hesapla / Math.Sqrt(findx * findy));

            Console.WriteLine("Korelasyon Katsayısı r = " + r);

            string iliski = r > 0 ? "Pozitif İlişki " : "Negatif İlişki";
            r = Math.Abs(r);
            if (r > 0 && r <= 0.25)
            {
                Console.WriteLine("Çok Zayıf Düzey " + iliski);

            }
            else if (r > 0.25 && r <= 0.49)
            {
                Console.WriteLine("Zayıf Düzey " + iliski);
            }
            else if (r > 0.50 && r <= 0.69)
            {
                Console.WriteLine("Orta Düzey " + iliski);
            }
            else if (r > 0.70 && r <= 0.89)
            {
                Console.WriteLine("Çok Yüksek Düzey " + iliski);
            }
            else if (r > 0.90 && r <= 1)
            {
                Console.WriteLine("Çok Yüksek Düzey " + iliski);
            }
            else
            {
                Console.WriteLine("Doğrusal bir ilişki yok.");
            }

            Console.ReadKey();
        }
    }
}
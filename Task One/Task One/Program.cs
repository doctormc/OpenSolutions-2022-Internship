using System;
using System.Linq;
using System.Diagnostics;

namespace Task_One
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            Random r = new Random();
            Tree tr = new Tree();
            for (int i = 0; i < 10; i++)
            {
                tr.Add(r.Next(1, 50));
            }

            tr.Save(tr.Root);

            Console.WriteLine("\n Обход в глубину:");
            sw.Start();
            tr.VGlubinu(tr.Root);
            sw.Stop();
            Console.WriteLine("\n Время обхода в глубину: " + sw.ElapsedTicks + "ticks");
            string[] f = tr.SimpleFoundInDepth.Select(x => x.ToString()).ToArray();
            Console.WriteLine("Простые числа при обходе в глубину: " + string.Join(" ", f));

            Console.WriteLine("\n Обход в ширину:");
            sw.Restart();
            tr.VShirinu(tr.Root);
            sw.Stop();

            Console.WriteLine("\n Время: " + sw.ElapsedTicks + "ticks");
            string[] w = tr.SimpleFoundInDepth.Select(x => x.ToString()).ToArray();
            Console.WriteLine("Простые числа при обходе в ширину: " + string.Join(" ", w));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrEnd
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Первая строка:");
            string first = Console.ReadLine();
            Console.WriteLine("Вторая строка:");
            string second = Console.ReadLine();
            Console.WriteLine(StrEndClass.StrEnd(first, second));
        }
    }
}

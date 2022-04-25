using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_One
{
    public class Node
    {
        //20.03.22: не стал закрывать поля, не знаю нужно ли. Проверку в Value>0 тоже не ввожу, поскольку генератор подает числа 1-50.
       
        public int Value
        {
            get;set;
        }
        
        public Node Left
        {
            get;set;
        }
        
        public Node Right
        {
            get;set;
        }
        

        public Node()
        {

        }
        public Node(int value)
        {
            Value=value;
        }
    }
}

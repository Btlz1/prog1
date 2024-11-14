using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace похуй
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int i = 0;
            Em[] sosalki = { new Sosu(), new Est(), new Srat(), new Spat() };
            Console.CursorVisible = false;
            while (true)
            {
                Console.WriteLine(new string('=', 26));
                Console.WriteLine($"[=========ДЕНЬ {i + 1}=========]");
                Console.WriteLine(new string('=', 26));
                System.Threading.Thread.Sleep(700);
                i++;
                foreach (Em em in sosalki)
                {
                    em.Update();
                    System.Threading.Thread.Sleep(700);
                } 
            }
        }
    }
    class Em
    {
        public virtual void Update() { }
    }
    class Sosu : Em
    {
        public override void Update()
        {
            Console.WriteLine("          Сосать");
        }
    }
    class Spat : Em
    {
        public override void Update()
        {
            Console.WriteLine("          Спать");
        }
    }
    class Est : Em
    {
        public override void Update()
        {
            Console.WriteLine("          Жрать");
        }
    }
    class Srat : Em
    {
        public override void Update()
        {
            Console.WriteLine("          Срать");
        }
    }
}

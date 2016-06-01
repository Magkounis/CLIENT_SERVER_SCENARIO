using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        public Program(C nc) { c = nc; }
       private C c;
        static void Main(string[] args)
       {
           Program pr = new Program(C);
           pr.dosth();
        }
        void dosth()
        {
            c.Value = 3;
            A a = new A(c);
            a.DoSomething();
            Console.Write( c.Value.ToString());
        }
    }
}

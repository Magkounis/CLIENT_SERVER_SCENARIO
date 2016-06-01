using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Example
    {
    }
    class C
    {
        public C() { }

        public int Value
        {
            get;
            set;
        }
        private int val;
    }
    class A
    {
        public A(C nc) { c = nc; }
        public void DoSomething() { c.Value = 2; }
        

        private C c;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


public class TestAssembly
    {
        private int factor;
        public TestAssembly(int f)
        {
            factor = f;
        }

        public int SampleMethod(int x)
        {
            Console.WriteLine("\nExample.SampleMethod({0}) executes.", x);
            return x * factor;
        }

    }


using System;
using System.Collections.Generic;
using System.Text;

namespace AssemblyTest1
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute : Attribute
    {
        public TestAttribute(string name)
        {
            ClassName = name;
        }
        public string ClassName = "";

    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace 测试类库
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TestAttribute:Attribute
    {
        public string ClassName = "";

    }
}

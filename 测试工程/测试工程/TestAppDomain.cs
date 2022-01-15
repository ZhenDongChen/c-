using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{
    public class TestAppDomain
    {
        public static void TestAppDomain1()
        {
            AppDomainSetup info = new AppDomainSetup();
            info.LoaderOptimization = LoaderOptimization.SingleDomain;
            info.ShadowCopyFiles = "true";
            info.ApplicationBase = @"E:\c#Project\c-\测试工程\test3\bin\Debug";
            AppDomain domain = AppDomain.CreateDomain("myDomain");
            domain.ExecuteAssembly(@"E:\c#Project\c-\测试工程\test3\bin\Debug\test3.exe");
            AppDomain.Unload(domain);
        }

        public static void TestAppdomain2()
        {

            AppDomainSetup info2 = new AppDomainSetup();
             info2.LoaderOptimization = LoaderOptimization.SingleDomain;
             info2.ApplicationBase = @"E:\c#Project\c-\测试工程\test3\bin\Debug";
             AppDomain domain2 = AppDomain.CreateDomain("test3", null, info2);
            ObjectHandle objHandle = domain2.CreateInstance("test3", "test3.TestStatic");
            ICollection obj = objHandle.Unwrap() as ICollection;
            int i = obj.Count;
            domain2.ExecuteAssembly(@"E:\c#Project\c-\测试工程\test3\bin\Debug\test3.exe");
            Console.WriteLine($"result:{i}");
             AppDomain.Unload(domain2);
        }

    }
}

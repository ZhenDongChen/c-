using AssemblyTest1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
   
    class Program
    {


        public static void TestAssembly()
        {
            Assembly assem = typeof(TestAssembly).Assembly;
            Console.WriteLine("Assembly Full Name:");
            Console.WriteLine(assem.FullName);

            AssemblyName assemblyName = assem.GetName();
            Console.WriteLine("Name:{0}",assemblyName.Name);
            Console.WriteLine("\nAssembly CodeBase:");
            Console.WriteLine(assem.CodeBase);

            // Create an object from the assembly, passing in the correct number
            // and type of arguments for the constructor.
            Object o = assem.CreateInstance("TestAssembly", false,
                BindingFlags.ExactBinding,
                null, new Object[] { 2 }, null, null);

            // Make a late-bound call to an instance method of the object.
            MethodInfo m = assem.GetType("TestAssembly").GetMethod("SampleMethod");
            Object ret = m.Invoke(o, new Object[] { 42 });
            Console.WriteLine("SampleMethod returned {0}.", ret);

            Console.WriteLine("\nAssembly entry point:");
            foreach (var item in assem.ExportedTypes)
            {
                Console.WriteLine(item.Name);
            }
            Assembly assembly = Assembly.Load("AssemblyTest1");
            if (assembly != null)
            {
                Console.WriteLine("加载程序成功");

                var allTypes = assembly.GetTypes();
                foreach (var item in allTypes)
                {
                    foreach (var item1 in item.GetTypeInfo().CustomAttributes)
                    {
                        Console.WriteLine("class attribute" + item1.AttributeType.Name);
                    }
                    Console.WriteLine(item.Name);
                }
                Console.WriteLine("-------------");

                Attribute[] definedAttributes = Attribute.GetCustomAttributes(assembly);
                foreach (var item in definedAttributes)
                {
                    Console.WriteLine(item.ToString());
                }
            }


        }
        static void Main(string[] args)
        {
            ///测试异步加载接口
            //TestIasyncResult.Test();
            //TestTaskFactory.Test();

            //TestInterlocked.NoSyncTest();
            // TestInterlocked.SyncTest();
            //testYieldReturn1.Test1();
            //LinkNodeList linkNodeList = new LinkNodeList();
            //LinkNode linkNode = new LinkNode("A");
            //LinkNode linkNode1 = new LinkNode("B");
            //LinkNode linkNode2 = new LinkNode("C");
            //LinkNode linkNode3 = new LinkNode("D");
            //linkNodeList.Add(linkNode);
            //linkNodeList.Add(linkNode1);
            //linkNodeList.Add(linkNode2);
            //linkNodeList.Add(linkNode3);

            //foreach (LinkNode item in linkNodeList)
            //{
            //    Console.WriteLine(item.NodeName);
            //}
            //Console.ReadKey();

            //TestCancellation.Test();
            //testAttribute.Test();
            //Program.TestAssembly();
            // DynamicObjectTest.Test();
            //TestCancellation.CancelTask();
            //TestTask.TestOrder();
            //TaskSyncData.TestTaskSync();
            //  for (int i = 0; i < 5; i++)
            //{
            //    TaskSyncData.TestJob();
            //}
            //TestProcess.GetProcessModule();
            //TestAppDomain.TestAppDomain1();
            //TestAppDomain.TestAppdomain2();
            //Console.WriteLine("当前域输出");
            //TestMonitor.TestMonitor1();
            //testAutoResetEvent.TestAutoResetEvent();
            // TestWaithandle.TestWaithandle1();
            // testRegex.TestRegex();
            //TestFileStream.ReadFileUsingFileStream(@"E:\c#Project\测试文本.txt");
            TestFileStream.TestBinaryWriter(@"E:\c#Project\二进制.txt");
            //TestTimer.Test2();
            Console.ReadKey();
        }
    }
}

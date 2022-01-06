using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
   
    class Program
    {



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
            testAttribute.Test();
            Console.ReadKey();
        }
    }
}

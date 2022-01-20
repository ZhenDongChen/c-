using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
    public class testAutoResetEvent
    {
        //通俗的来讲只有等myResetEven.Set() 成功运行后,
        //myResetEven.WaitOne()才能够获得运行机会;
        //Set是发信号，WaitOne是等待信号，只有发了信号，
        //等待的才会执行。如果不发的话，
        //WaitOne后面的程序就永远不会执行。
        //下面我们来举一个例子：我去书店买书，
        //当我选中一本书后我会去收费处付钱，
        //付好钱后再去仓库取书。
        //这个顺序不能颠倒，我作为主线程，
        //收费处和仓库做两个辅助线程，代码如下
        static AutoResetEvent myResetEvent = new AutoResetEvent(false);
        static AutoResetEvent ChangeEvent = new AutoResetEvent(false);
        static int number; //这是关键资源
        const int numIterations = 5;
        public static void TestAutoResetEvent()
        {
            Thread payMoneyThread = new Thread(new ThreadStart(PayMoneyProc));
            payMoneyThread.Name = "付钱线程";
            Thread getBookThread = new Thread(new ThreadStart(GetBookProc));
            getBookThread.Name = "取书线程";
            payMoneyThread.Start();
            getBookThread.Start();

            for (int i = 1; i <= numIterations; i++)
            {
                Console.WriteLine("买书线程：数量{0}", i);
                number = i;
                //Signal that a value has been written.
                myResetEvent.Set();
                ChangeEvent.Set();
                Thread.Sleep(10);

            }
           
            payMoneyThread.Abort();
            getBookThread.Abort();

        }
        static void PayMoneyProc()
        {
            while (true)
            {
                myResetEvent.WaitOne();
                //myResetEvent.Reset();
                Console.WriteLine("{0}：数量{1}", Thread.CurrentThread.Name, number);
            }
        }
        static void GetBookProc()
        {
            while (true)
            { 
                ChangeEvent.WaitOne();
                // ChangeEvent.Reset();               
                Console.WriteLine("{0}：数量{1}", Thread.CurrentThread.Name, number);
                Console.WriteLine("------------------------------------------");
                Thread.Sleep(0);
            }
        }

    }
}

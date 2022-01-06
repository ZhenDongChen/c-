using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
    public  class TestInterlocked
    {
        public static void NoSyncTest()
        {
            //缓冲区，只能容纳一个字符
            char buffer = ',';
            string str = "这里面的字会一个一个读取出来，一个都不会少，，";
            //线程：写入数据
            Thread writer = new Thread(() =>
            {
                for (int i = 0; i < str.Length; i++)
                {
                    buffer = str[i];
                    Thread.Sleep(20);
                }
            }
            );
            //线程：读出数据
            Thread Reader = new Thread(() =>
            {
                for (int i = 0; i < str.Length; i++)
                {
                    char chartemp = buffer;
                    Console.Write(chartemp);
                    Thread.Sleep(30);
                }
            }
            );
            writer.Start();
            Reader.Start();
            Console.ReadKey();
        }

        static long numberOfUsedSpace = 0;
        public static void SyncTest()
        {
            char buffer = '1';
            Thread writer = new Thread(delegate ()
            {
            string str = "这里面的字会一个一个读取出来，一个都不会少，，,,,";

            for (int i = 0; i < 24; i++)
            {

                    while (Interlocked.Read(ref numberOfUsedSpace) == 1)
                    {
                        Thread.Sleep(50);
                    }
                    buffer = str[i];
                    Interlocked.Increment(ref numberOfUsedSpace);
                }
            });

            Thread read = new Thread(delegate ()
            {
                for (int i = 0; i < 24; i++)
                {
                    while (Interlocked.Read(ref numberOfUsedSpace) == 0)
                    {
                        Thread.Sleep(50);
                    }
                    char ch = buffer;    //从缓冲区读取数据
                    Console.Write(ch);
                    Interlocked.Decrement(ref numberOfUsedSpace);
                }
            });

            //启动线程
            writer.Start();
            read.Start();
            Console.ReadKey();

        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace 测试工程
{
    public class TestTask
    {
        private static object s_logLock = new object();


        static void DoOnFirst()
        {
            Console.WriteLine($"doing some task {Task.CurrentId}");
            Task.Delay(3000).Wait();
        }

        static void DoOnSecond(Task t)
        {
            Console.WriteLine($"task{t.Id} finished");
            Console.WriteLine($"this task id {Task.CurrentId}");
            Console.WriteLine("do some cleanup");
            Task.Delay(3000).Wait();
        }

        public static Tuple<int, int> TaskWithResult(object division)
        {
            Tuple<int, int> div = (Tuple<int, int>)division;
            int result = div.Item1 / div.Item2;
            int reminder = div.Item1 % div.Item2;
            Console.WriteLine("task creates a result...");
            return Tuple.Create(result,reminder);
        }

        public static void RunsynchronousTask()
        {
            TaskMethod("just the main thread");
            var t1 = new Task(TaskMethod,"run sync");
            t1.RunSynchronously();
        }

        public static void TaskMethod(object o)
        {
            Log(o?.ToString());
        }
        public static void Log(string title)
        {
            lock (s_logLock)
            {
                Console.WriteLine(title);
                Console.WriteLine($"Task id :{Task.CurrentId?.ToString() ?? "no task"}"+
                    $"thread : {Thread.CurrentThread.ManagedThreadId}");

#if (!DNXCORE)
                Console.WriteLine($"is pool thread:{Thread.CurrentThread.IsThreadPoolThread}");
#endif
                Console.WriteLine($"is background thread:{Thread.CurrentThread.IsBackground}");
            }
        }


        public static void Test()
        {
            var tf = new TaskFactory();
            Task t1 = tf.StartNew(TaskMethod,"using a task factory");
            Task t2 = Task.Factory.StartNew(TaskMethod,"factory via a task");
            var t3 = new Task(TaskMethod,"using a task constructor and Start");
            t3.Start();
            Task t4 = Task.Run(()=>TaskMethod("using the run method"));
        }

        public static void TestOrder()
        {
            for (int i = 0; i < 5; i++)
            {
                Task.Delay(1000).Wait();
                Console.WriteLine(i);
            }
        }
    }
}

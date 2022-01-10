using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
    public class TestParallel
    {
        static string result;
        public static void ThreadAndTaskDiffrent()
        {
            saySomthing();
            Console.WriteLine(result);
        }

        static string saySomthing()
        {
            Thread.Sleep(5);
            result = "hello word";
            return "something";
        }

        static async Task<string>  TaskSaySomthing()
        {
            await Task.Delay(5);
            result = "hello word";
            Console.WriteLine(result+"xxxxxxxx");
            return "something";
        }

        static void Log(string prefix)
        {
            Console.WriteLine($"{prefix} ,task:{Task.CurrentId}"
                +$"thread:{Thread.CurrentThread.ManagedThreadId}");
        }
        public static void Test()
        {
            ParallelLoopResult parallelLoopResult = Parallel.For(0,10,(i,state)=>
            {
                Log($"S {i}");
                if (i > 5)
                    state.Break();
                Task.Delay(1000).Wait();
                Log($"E {i}");
            });
            Console.WriteLine($"is Complete:{parallelLoopResult.IsCompleted}");
        }

    }
}

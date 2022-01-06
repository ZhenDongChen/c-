using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace 测试工程
{
    public  class TestTaskFactory
    {
        public static void Test()
        {
            Task[] tasks = new Task[2];
            String[] files = null;
            String[] dirs = null;
            String docsDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            tasks[0] = Task.Factory.StartNew(() => files = Directory.GetFiles(docsDirectory));
            tasks[1] = Task.Factory.StartNew(() => dirs = Directory.GetDirectories(docsDirectory));

           Task.Factory.StartNew(() =>
           {
                Thread.Sleep(3000);
               Console.WriteLine("结束任务");
            }).ContinueWith(completeTask =>
            {
                Console.WriteLine("结束任务1");
            });


            Task.Factory.ContinueWhenAll(tasks, completedTasks =>
            {
                Console.WriteLine("{0} contains: ", docsDirectory);
                Console.WriteLine("   {0} subdirectories", dirs.Length);
                Console.WriteLine("   {0} files", files.Length);
            });
            Console.ReadKey();
        }
    
        
    }
}

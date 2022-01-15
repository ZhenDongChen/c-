using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace 测试工程
{
    public  class TestDataFlow
    {
        #region BufferBlock
        static BufferBlock<string> s_buffer = new BufferBlock<string>();

        private static void Producer()
        {
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    s_buffer.Post(input);
                }
            }
        }

        private static async Task ConsumerAsync()
        {
            while (true)
            {
                string data = await s_buffer.ReceiveAsync();
                Console.WriteLine($"user input {data}");
            }
        }

        public static void BuffBlockTest()
        {
            Task t1 = Task.Run(()=>
            {
                Producer();
            });

            Task t2 = Task.Run(async ()=>await ConsumerAsync());
            Task.WaitAll(t1,t2);
        }

        #endregion

        #region ActionBlock

        static ActionBlock<int> absync = new ActionBlock<int>((i) =>
        {
            Console.WriteLine(i+"start");
            Thread.Sleep(1000);
            Console.WriteLine(i+"Thread:"+Thread.CurrentThread.ManagedThreadId+"execute Time:"+DateTime.Now);
        });

        static ActionBlock<int> absyncTask = new ActionBlock<int>(async (i) =>
        {
            Console.WriteLine(i + "start");
            await Task.Delay(1000);
            Console.WriteLine(i + "Thread:" + Thread.CurrentThread.ManagedThreadId + "execute Time:" + DateTime.Now);
            Console.WriteLine(i + "Task:" + Task.CurrentId + "execute Time:" + DateTime.Now);
        },new ExecutionDataflowBlockOptions() { MaxDegreeOfParallelism = 3});


        public static void TestSync()
        {
            for (int i = 0; i < 10; i++)
            {
                absyncTask.Post(i);
            }
            Console.WriteLine("Post finished");
            absyncTask.Complete();
            Console.WriteLine("Post finished");
            absyncTask.Completion.Wait();
            Console.WriteLine("process finished");
        }

        #endregion


        #region TransformBlock
        static TransformBlock<string, string> tabUrl = new TransformBlock<string, string>((url)=>
        {
            WebClient webClient = new WebClient();
            return url;
        });

        public static void  TestTransformBlock()
        {
            tabUrl.Post("www.baidu.com");
            tabUrl.Post("www.sina.com.cn");

            try
            {
                string baiduHTML = tabUrl.Receive();
                string sinaHTML = tabUrl.Receive();
                Console.WriteLine($"baiduHTML:{baiduHTML}");
                Console.WriteLine($"sinaHTML:{sinaHTML}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ex.message:{ex.Message}");
            }
            //try
            //{
            //    WebClient webClient = new WebClient();
            //    string result = webClient.DownloadString(new Uri("www.sina.com.cn"));
            //    Console.WriteLine(result);
            //} catch (Exception ex)
            //{
            //    Console.WriteLine( ex.Message);
            //}

        }



        #endregion

        #region 连接块
        static IEnumerable<string> GetFileName(string path)
        {
            foreach (var fileName in Directory.EnumerateFiles(path,"*.cs"))
            {
                yield return fileName;
            }
        }

        static IEnumerable<string> LoadingLines(IEnumerable<string> fileNames)
        {
            foreach (var item in fileNames)
            {
                using (FileStream stream = File.OpenRead(item))
                {
                    var reader = new StreamReader(stream);
                    string line = null;
                    while ((line = reader.ReadLine()) != null)
                    {
                        yield return line;
                    }
                }
            }
        }

        static IEnumerable<string> GetWords(IEnumerable<string> lines)
        {
            foreach (var item in lines)
            {
                string[] words = item.Split(' ',',',':','(',')','{','}','.');
                foreach (var item1 in words)
                {
                    if (!string.IsNullOrEmpty(item1))
                    {
                        yield return item1;
                    }
                }
            }
        }

        static ITargetBlock<string> SetupPipeLine()
        {
            var fileNamesForPath = new TransformBlock<string, IEnumerable<string>>(path=>
            {
                return GetFileName(path);
            });

            var lines = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(fileNames=>
            {
                return LoadingLines(fileNames);
            });

            var words = new TransformBlock<IEnumerable<string>, IEnumerable<string>>(line2=>
            {
                return GetWords(line2);
            });

            var display = new ActionBlock<IEnumerable<string>>(coll=>
            {
                foreach (var item in coll)
                {
                    Console.WriteLine(item);
                }
            });

            fileNamesForPath.LinkTo(lines);
            lines.LinkTo(words);
            words.LinkTo(display);
            return fileNamesForPath;
        }

        public static void TestBlock()
        {
            var target = SetupPipeLine();
            target.Post(@"E:\c#Project\c-\测试工程\测试工程");
           
        }

        #endregion

        public static void Test()
        {
            var processInput = new ActionBlock<string>((s)=>
            {
                Console.WriteLine($"user input is {s}");
            });
            bool exit = false;
            while (!exit)
            {
                string input = Console.ReadLine();
                if (string.Compare(input, "exit", ignoreCase: true) == 0)
                {
                    exit = true;
                }
                else
                {
                    processInput.Post(input);
                }
            }
        }

    }
}

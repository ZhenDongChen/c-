using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{
    public   class testYieldReturn1
    {
        static List<int> _numArray = new List<int>();
        public static void Test1()
        {
            
            for (int i = 0; i < 100; i++)
            {
                _numArray.Add(i);
            }
            TestMethod();

            Console.ReadKey();
        }

        static void TestMethod()
        {
            foreach (var item in GetAllEvenNumber())
            {
                Console.WriteLine(item);
            }
        }

        static IEnumerable<int> GetAllEvenNumber()
        {
            ////这个是一次性把所有的需求都加到内存里面
            List<int> result = new List<int>();
            foreach (var item in _numArray)
            {
                if (item % 2 == 0)
                {
                    result.Add(item);
                }
            }
            return result;
        }
    }

    public class testYieldReturn2
    {
        static List<int> _numArray = new List<int>();
        public static void Test1()
        {

            for (int i = 0; i < 100; i++)
            {
                _numArray.Add(i);
            }
            TestMethod();


        }

        static void TestMethod()
        {
            foreach (var item in GetAllEvenNumber())
            {
                Console.WriteLine(item);
            }
        }

        static IEnumerable<int> GetAllEvenNumber()
        {
            ///这个是按需
            foreach (var item in _numArray)
            {
                if (item % 2 == 0)
                {
                    yield return item;
                }
            }
            yield break;
        }
    }
}

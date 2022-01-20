using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace 测试工程
{
    public class testRegex
    {

        static string inputStr1 = @"notify_world_npc_init 6501 {";
        public static void TestRegex()
        {
            Console.WriteLine(inputStr1.Trim()); 
            if (Regex.Match(inputStr1.Trim(), @"^notify_").Success)
            {
                Console.WriteLine("ssssssssss");
            }
            else
            {
                Console.WriteLine($"当前加入的Sproto的数据结构不符合匹配结构 {inputStr1}");
            }

            // Console.WriteLine(result.Success);
        }

    }
}

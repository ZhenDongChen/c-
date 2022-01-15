using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{
    public class TestProcess
    {
        public static void GetProgramerProcess()
        {
            var processList = Process.GetProcesses().OrderBy(x=>x.Id);

            foreach (var item in processList)
            {
                Console.WriteLine($"processID:{item.Id} processName:{item.ProcessName}");
            }
        }

        public static void GetProcessModule()
        {
            var ModuleList = Process.GetCurrentProcess().Modules;
            foreach (System.Diagnostics.ProcessModule module in ModuleList)
            {
                Console.WriteLine($"module.ModuleName:{module.ModuleName} {module.FileVersionInfo.FileVersion}");
            }
        }

    }
}

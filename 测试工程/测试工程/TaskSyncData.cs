using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 测试工程
{
   public class TaskSyncData
    {
        #region 竞争条件测试
        class StateObject {
            private int _index = 5;
            private object async = new object();
            public void ChangeState(int loop)
            {
                //lock (async)
                //{
                    if (_index == 5)
                    {
                        _index++;
                        Trace.Assert(_index == 6, $"Race condition occurred after{loop} loops");
                    }
                    _index = 5;
                //}
            }
        }

        static void Racecondition(object o)
        {
            Trace.Assert(o is StateObject, $"o must be of type StateObject");
            StateObject temp = o as StateObject;
            int i = 0;
            while (true)
            {
                lock (temp)
                {
                    temp.ChangeState(i++);
                }
               
            }
        }

        public static void TestTaskSync()
        {
            var stateObject = new StateObject();
            for (int i = 0; i < 3; i++)
            {
                Task.Run(()=>
                {
                    Racecondition(stateObject);
                });
            }
        }
        #endregion

        #region lock语句和现成安全

        class ShardeState
        { 
            public int State { get; set; }
        }

        class Job
        {
            ShardeState _shardeState;
            object _sync = new object();
            public Job(ShardeState shardeState)
            {
                _shardeState = shardeState;
            }
            public void DoTheJob()
            {
                lock (_shardeState)
                {
                    for (int i = 0; i < 50000; i++)
                    {
                        _shardeState.State += 1;
                    }
                }

            }
        }

        public static void  TestJob()
        {
            int numTasks = 20;
            var state = new ShardeState();
            var tasks = new Task[numTasks];
            for (int i = 0; i < numTasks; i++)
            {
                tasks[i] = Task.Run(()=>
                {
                    new Job(state).DoTheJob();
                });
            }
            Task.WaitAll(tasks);
            Console.WriteLine($"summarized {state.State}");
        }

        #endregion


    }
}

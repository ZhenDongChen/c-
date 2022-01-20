using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace 测试工程
{
    public class TestTimer
    {
        private static System.Timers.Timer aTimer;
        public static void Test() {

            SetTimer();

            Console.WriteLine("\nPress the Enter key to exit the application...\n");
            Console.WriteLine("The application started at {0:HH:mm:ss.fff}", DateTime.Now);
            Console.ReadLine();
            aTimer.Stop();
            aTimer.Dispose();

            Console.WriteLine("Terminating the application...");

        }


        public static void Test2()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += async (sender, e) => await HandleTimer();
            timer.Start();
            Console.Write("Press any key to exit... ");
            Console.ReadKey();
        }
        private static Task HandleTimer()
        {
            Console.WriteLine("\nHandler not implemented...");
            throw new NotImplementedException();
        }
        static void SetTimer()
        {
            // Create a timer with a two second interval.
            aTimer = new System.Timers.Timer(2000);
            // Hook up the Elapsed event for the timer. 
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

         static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                              e.SignalTime);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Quartz;
using Quartz.Impl;
using Quartz.Impl.Triggers;

namespace HelloWorldQuartzNetWithRamJobStore
{
   class Program
    {
        static void Main(string[] args)
        {
            try
            {
                while (true)
                {
                    var sj = new ScheduledJob();
                    sj.Run();

                    Console.WriteLine("Hello World");
                    Console.WriteLine("{0}Press Ctrl^C to close the window. The job will continue to run via Quartz.Net windows service, ",
                                        Environment.NewLine);

                    Thread.Sleep(100);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed: {0}", ex.Message);
                Console.ReadKey();
            }
        }
    }
}

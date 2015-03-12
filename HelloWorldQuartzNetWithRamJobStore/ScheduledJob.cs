using System;
using System.Collections.Specialized;
using Quartz;
using Quartz.Impl;

namespace HelloWorldQuartzNetWithRamJobStore
{
    public class ScheduledJob : IScheduledJob
    {
        public void Run()
        {
            //You can define your custom scheduler service With GetScheduler Method
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            var schd = schedulerFactory.GetScheduler();

           

            if (!schd.IsStarted)
                schd.Start();

            // Define the Job to be scheduled
            var job = JobBuilder.Create<HelloWorldJob>()
                .WithIdentity("WriteHelloToConsole", "IT")
                .RequestRecovery()
                .Build();

            // Associate a trigger with the Job
            var trigger = (ICronTrigger) TriggerBuilder.Create()
                .WithIdentity("WriteHelloToConsole", "IT")
                .WithCronSchedule("0 0/1 * 1/1 * ? *")
                .StartAt(DateTime.UtcNow)
                .WithPriority(1)
                .Build();

            // Validate that the job doesn't already exists
            if (schd.CheckExists(new JobKey("WriteHelloToConsole", "IT")))
            {
                schd.DeleteJob(new JobKey("WriteHelloToConsole", "IT"));
            }

            var schedule = schd.ScheduleJob(job, trigger);
            Console.WriteLine("Job '{0}' scheduled for '{1}'", "WriteHelloToConsole", schedule.ToString("r"));
         

        }

        //private static IScheduler GetScheduler()
        //{
        //    try
        //    {
        //        var properties = new NameValueCollection();
        //        properties["quartz.scheduler.instanceName"]="ServerScheduler";
        //        properties["quartz.scheduler.proxy"] = "true";
        //        properties["quartz.scheduler.proxy.address"] = string.Format("tcp://{0}:{1}/{2}", "localhost", "5555", "QuartzScheduler");
               
        //        var sf = new StdSchedulerFactory(properties);
        //        return sf.GetScheduler();

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Scheduler not available: '{0}'", ex.Message);
        //        throw;
        //    }
        //}
    }
}
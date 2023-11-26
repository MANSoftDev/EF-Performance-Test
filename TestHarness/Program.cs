using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;


namespace TestHarness
{
    class Program
    {
        private const int ITERATIONS = 5;
        private const int NUM_CUSTOMERS = 1000;
        private const int NUM_PHONES = 5;

        static void Main(string[] args)
        {
            Stopwatch = new Stopwatch();
            Tests = new List<TestRun>();

            Tests.Add(new TestRun("Clear", new Action(ADOTests.Clear)));
            Tests.Add(new TestRun("ADO Insert", new Action(ADOTests.InsertCustomers)));
            Tests.Add(new TestRun("ADO Get", new Action(ADOTests.GetCustomers)));
            Tests.Add(new TestRun("Clear", new Action(ADOTests.Clear)));
            Tests.Add(new TestRun("EF Batch Insert", new Action(EFTests.InsertCustomers_Batch)));
            Tests.Add(new TestRun("Clear", new Action(ADOTests.Clear)));
            Tests.Add(new TestRun("EF Single Insert", new Action(EFTests.InsertCustomers_Single)));
            Tests.Add(new TestRun("EF Get", new Action(EFTests.GetCustomers)));
            Tests.Add(new TestRun("EF Get Compiled", new Action(EFTests.GetCustomers_Compiled)));
            Tests.Add(new TestRun("EF Get Compiled NoTracking", new Action(EFTests.GetCustomers_CompiledNoTracking)));
            Tests.Add(new TestRun("EF Get Execute", new Action(EFTests.GetCustomers_Execute)));
            Tests.Add(new TestRun("EF Get NoTracking", new Action(EFTests.GetCustomers_NoTracking)));

            do
            {
                for(int x = 0; x < 5; x++)
                {
                    foreach(var test in Tests)
                    {
                        test.ExecuteTime = DoTest(test.Test, test.Name);
                        LogComplete(test);
                    }

                    LogToFile();
                }

                Console.WriteLine("Press X to exit");

            } while(Console.ReadKey().Key != ConsoleKey.X);
        }

        private static double DoTest(Action test, string testName)
        {
            // Clear only needs to be run once.
            if(testName == "Clear")
            {
                test();
                return 0;
            }

            double executeTime = 0;
            for(int x = 0; x < ITERATIONS; x++)
            {
                Console.WriteLine("{0} test: {1}", testName, x + 1);
                Stopwatch.Reset();
                Stopwatch.Start();

                test();

                Stopwatch.Stop();
                executeTime += Stopwatch.Elapsed.TotalSeconds;

                Console.WriteLine("Total Seconds for {0}: {1}", testName, Stopwatch.Elapsed.TotalSeconds);
                Console.WriteLine("---------------------------");
            }

            GC.Collect();

            return executeTime;
        }

        private static void LogComplete(TestRun test)
        {
            Console.WriteLine("Total Seconds for {0} test: {1}", test.Name, test.ExecuteTime);
            Console.WriteLine("Avg Seconds for {0} test: {1}", test.Name, test.ExecuteTime / ITERATIONS);
            Console.WriteLine("---------------------------");
        }

        private static void LogToFile()
        {
            TextWriter writer = null;
            if(!File.Exists("log.csv"))
            {
                writer = File.CreateText("log.csv");

                List<string> testNames = Tests.Select(e => e.Name).Where(e => e != "Clear").ToList();

                writer.WriteLine(string.Join(",", testNames.ToArray()));
            }
            else
            {
                writer = new StreamWriter(File.Open("log.csv", FileMode.Append));
            }

            List<string> times = new List<string>();
            foreach(TestRun test in Tests.Where(e => e.Name != "Clear"))
            {
                times.Add((test.ExecuteTime / ITERATIONS).ToString());
            }

            writer.WriteLine(string.Join(",", times.ToArray()));

            writer.Close();
        }

        private static Stopwatch Stopwatch { get; set; }

        private static EFTests EFTests { get { return new EFTests(NUM_CUSTOMERS, NUM_PHONES); } }
        private static ADOTests ADOTests { get { return new ADOTests(NUM_CUSTOMERS, NUM_PHONES); } }

        private static List<TestRun> Tests { get; set; }

        private class TestRun
        {
            public TestRun(string name, Action test)
            {
                Name = name;
                Test = test;
            }

            public string Name { get; private set; }
            public Action Test { get; set; }
            public double ExecuteTime { get; set; }
        }
    }
}
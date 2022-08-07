using System;
using System.IO;
using CianLogger;
using NUnit.Framework;

namespace Tests
{
    public class LoggerTests
    {
        [SetUp]
        public void Setup()
        {
            //this.l = new Logger();
        }

        [Test]
        public void TestWriting()
        {
            var logFilePath = $"{System.Environment.CurrentDirectory}//testlog.log";
            Logger.WriteToLog(logFilePath, "test");
            string log = "";
            using (var sr = new StreamReader(logFilePath))
            {
                log = sr.ReadToEnd();
            }
            File.Delete(logFilePath);
            Assert.AreEqual($"test{Environment.NewLine}", log);
        }
    }
}
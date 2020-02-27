using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FormApp1.Tests.App
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void LoggerTestUTF8()
        {
            var Logger = new FormApp1.App.Logger(System.Environment.CurrentDirectory, Encoding.UTF8);
            bool ret = false;
            try
            { 
                Logger.Info("Log Info");
                Logger.Error("Log Error");
                Logger.Debug("Log Debug");
                ret = true;
            }
            catch (Exception ex )
            {
                ret = false;
                Console.Write(ex.Message);
            }
            Assert.IsTrue(ret);
        }
    }
}

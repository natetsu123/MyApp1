using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormsApp1.Model;
using FormsApp1.DatabaseConnectors;
using FormsApp1.Entity;

namespace FormApp1.Tests.Model
{
    [TestClass]
    public class AppConfigModelTests
    {
        [TestMethod]
        public void AppConfigModelTest()
        {
            Boolean ret;
            SqliteDatabaseInfo db = new SqliteDatabaseInfo { Path = System.Environment.CurrentDirectory, Filename = "test_" + DateTime.Now.ToString("yyyyMMddHHmmss") };
            AppConfigModel appConfigModel = new AppConfigModel(db);
            AppConfig[] entity = new AppConfig[]
            {
                new AppConfig() { Key = "setting.view.enable",       Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.visible",      Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.disable",      Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.LineColor",    Value = "red",   Description = "Test" },
                new AppConfig() { Key = "setting.view.BackColor",    Value = "blue",  Description = "Test" },
                new AppConfig() { Key = "setting.view.TaskBarColor", Value = "green", Description = "Test" }
            };

            try
            {
                appConfigModel.CreateTable();
                foreach (var item in entity) appConfigModel.Insert(item);                
                ret = true;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                ret = false;
                
            }
            Assert.IsTrue(ret);
            
        }
    }
}

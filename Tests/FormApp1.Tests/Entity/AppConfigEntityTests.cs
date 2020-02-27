using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FormApp1.DatabaseConnectors;
using FormApp1.Entity;

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
            AppConfigEntity appConfigEntity = new AppConfigEntity(db);
            
            /**********************************************************************************************************/
            AppConfig[] insert_entity = new AppConfig[]
            {
                new AppConfig() { Key = "setting.view.enable",       Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.visible",      Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.disable",      Value = "false", Description = "Test" },
                new AppConfig() { Key = "setting.view.LineColor",    Value = "red",   Description = "Test" },
                new AppConfig() { Key = "setting.view.BackColor",    Value = "blue",  Description = "Test" },
                new AppConfig() { Key = "setting.view.TaskBarColor", Value = "green", Description = "Test" }
            };

            /**********************************************************************************************************/
            AppConfig[] delete_entity = new AppConfig[]
            {
                new AppConfig() { Id = 2 },
                new AppConfig() { Id = 4 }
            };

            /**********************************************************************************************************/
            AppConfig[] update_entity = new AppConfig[2];

            update_entity[0] = insert_entity[2]; update_entity[0].Id = 3; update_entity[0].Value = "true";
            update_entity[1] = insert_entity[4]; update_entity[1].Id = 5; update_entity[1].Value = "black";
            
            /**********************************************************************************************************/
            try
            {
                appConfigEntity.CreateTable();
                foreach (var item in insert_entity) appConfigEntity.Insert(item);
                AppConfig[] select_entity = appConfigEntity.Select();
                foreach (var item in delete_entity) appConfigEntity.Delete(item);
                foreach (var item in update_entity) appConfigEntity.Update(item);

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

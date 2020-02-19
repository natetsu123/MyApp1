using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfrastructureLayer.DB;
using System.Reflection;
using System.Collections.Generic;

namespace InfrastructureLayer.Tests.DB
{
    [TestClass]
    public class SQLiteDataAccessTest
    {
        private SQLiteDataAccess SQLiteDataAccess;

        [TestMethod]
        public void Test1()
        {
            CreateInstance();
            CreateTable();
            Insert();

        }

        private bool CreateInstance()
        {
            var dn = DateTime.Now.ToString("yyyyMMddHHmmss");
            var conString = "Data Source = test_" + dn + ".db";
            try
            {
                SQLiteDataAccess = new SQLiteDataAccess(conString);
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("Result: {0}", "true");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("Error : {0}", ex.Message);
                return false;
            }
        }
        private bool CreateTable()
        {
            var sql = new System.Text.StringBuilder();
            sql.Append("CREATE TABLE TEST( Id INTEGER NOT NULL, Value Text, PRIMARY KEY(Id) ) ");
            try
            {
                SQLiteDataAccess.ExecuteNonQuery(sql.ToString());
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("Result: {0}", "true");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("Error : {0}", ex.Message);
                return false;
            }
            
        }
        private bool Insert()
        {
            List<string> list = new List<string>();
            string insert_query = "INSERT INTO TEST VALUES({0}, '{1}')";
            for (int i = 0; i < 20; i++)
            {
                list.Add(string.Format(insert_query, i, i + "行目"));
            }
            try
            {
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                foreach (var item in list)
                {
                    SQLiteDataAccess.ExecuteNonQuery(item);
                    Console.WriteLine("Query : {0}", item);
                }
                Console.WriteLine("Result: {0}", "true");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Method: {0}", MethodBase.GetCurrentMethod().Name);
                Console.WriteLine("Error : {0}", ex.Message);
                return false;
            }
        }
    }
}

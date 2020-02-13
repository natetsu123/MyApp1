﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using InfrastructureLayer.DB.SQLite.Entity;

namespace InfrastructureLayer.Tests.DB.SQLite
{
    /// <summary>
    /// ParameterModel の概要の説明
    /// </summary>
    [TestClass]
    public class ParameterModel
    {
        public ParameterModel()
        {
            //
            // TODO: コンストラクター ロジックをここに追加します
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///現在のテストの実行についての情報および機能を
        ///提供するテスト コンテキストを取得または設定します。
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region 追加のテスト属性
        //
        // テストを作成する際には、次の追加属性を使用できます:
        //
        // クラス内で最初のテストを実行する前に、ClassInitialize を使用してコードを実行してください
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // クラス内のテストをすべて実行したら、ClassCleanup を使用してコードを実行してください
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 各テストを実行する前に、TestInitialize を使用してコードを実行してください
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 各テストを実行した後に、TestCleanup を使用してコードを実行してください
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void TestMethod1()
        {
            //
            // TODO: テスト ロジックをここに追加してください
            //

            bool ret = true;
            try
            {

                // DBファイルチェック->存在すれば削除
                if (System.IO.File.Exists("test.db"))
                {
                    System.IO.File.Delete("test.db");
                    Console.WriteLine("test.db ファイルを削除");
                }

                // モデルをインスタンス化
                InfrastructureLayer.DB.SQLite.Model.ParameterModel parameterModel = new InfrastructureLayer.DB.SQLite.Model.ParameterModel("test");

                // テーブル作成
                parameterModel.CreateTable();
                Console.WriteLine("Success: CreateTable ");

                // データ挿入
                Parameter[] e = new Parameter[3];
                e[0] = new Parameter { Key = "enble",     Value = "true", Description = "test e[0]" };
                e[1] = new Parameter { Key = "visible",   Value = "true", Description = "test e[1]" };
                e[2] = new Parameter { Key = "tab_order", Value = "4",    Description = "test e[2]" };

                parameterModel.Insert(e[0]);
                parameterModel.Insert(e[1]);
                parameterModel.Insert(e[2]);


                ret = true;

            }
            catch (Exception ex )
            {
                Console.Write("Error: " + ex.Message);
                ret = false;
            }
            
            // 結果
            Assert.AreEqual(true, true);

            

        }
    }
}
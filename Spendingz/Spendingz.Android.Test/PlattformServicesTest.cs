using System;
using NUnit.Framework;
using Spendingz.Droid.Services;
using Spendingz.Model.Data;
using System.IO;
using System.Collections.Generic;

namespace Spendingz.Android.Test
{
    [TestFixture]
    public class PlattformServicesTest
    {
        public DbStorage DbStorage;
        public LocalStorage LocalStorage;
        public string Folder;
        [SetUp]
        public void Setup() {

            DbStorage = new DbStorage();
            LocalStorage = new LocalStorage(MainActivity.Context);
            Folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            DbStorage.CreateDatabase<Category>();

        }


        [TearDown]
        public void Tear()
        {
            File.Delete(Path.Combine(Folder, "Category.db"));
        }

        [Test]
        public void LocalStorageTest()
        {
            LocalStorage.SaveBool("Tested",true);
            var actual= LocalStorage.GetBool("Tested");
            Assert.True(actual);
        }

        [Test]
        public void LocalStorageTestSpellingMistake()
        {
            LocalStorage.SaveBool("Tested", true);
            var actual = LocalStorage.GetBool("Testeded");
            Assert.False(actual);
        }

        [Test]
        public void DbStorageExists()
        {
            var actual = File.Exists(Path.Combine(Folder,"Category.db"));
            Assert.IsTrue(actual);
        }

        [Test]
        public void DbStorageInsert()
        {
          
            var data = new Category { Description = "test Running", Title = "Testeloni" };
            var actual = DbStorage.CreateOrUpdateEntry(data);
            Assert.IsTrue(actual!=null && actual.Value!=0);
        }

        [Test]
        public void DbStorageInsertMultiple()
        {
           
            var list = new List<Category> { new Category { Title="test 1" }, new Category { Title="Test 2" } };
            var actual = DbStorage.CreateEntries(list);
            Assert.IsTrue(actual.Count==2);
        }

        [Test]
        public void DbStorageDelete()
        {
            var data = new Category { Description = "test Running", Title = "Testeloni" };
            var id = DbStorage.CreateOrUpdateEntry(data);
            data.Id = id.Value;
            var actual = DbStorage.DeleteEntry(data);
            Assert.IsTrue(actual);
        }
    }
}
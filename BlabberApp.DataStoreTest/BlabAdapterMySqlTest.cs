using System.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlabberApp.DataStore.Adapters;
using BlabberApp.DataStore.Plugins;
using BlabberApp.Domain.Entities;

namespace BlabberApp.DataStoreTest
{
    [TestClass]
    public class BlabAdapter_MySql_UnitTests
    {
        private Blab _blab;
        private BlabAdapter _harness = new BlabAdapter(new MySqlBlab());
        private readonly User _user = new User("fooabar@example.com");

        [TestInitialize]
        public void Setup()
        {
            _blab = new Blab("This is a test blab", _user);
        }
        [TestCleanup]
        public void TearDown()
        {
            _harness.Remove(_blab);
        }

        [TestMethod]
        public void Canary()
        {
            Assert.AreEqual(true, true);
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MightyFX.TestUtilities;
using MightyFX.Users;

namespace MightyFX.Data.Tests
{
    [TestClass]
    public class DataServerTests : AutoTest
    {
        [TestMethod]
        public void DataServer_BasicProperties()
        {
            Assert.AreEqual(2, _server.Providers.Count);
            Assert.AreEqual(3, _server.Sources.Count);
        }

        [TestMethod]
        public void DataServer_AddAndRemoveSources()
        {
            Assert.AreEqual(3, _server.Sources.Count);
            
            var source = new TestDataSource("haha");
            _server.AddSource(source);
            Assert.AreEqual(4, _server.Sources.Count);

            _server.RemoveSource(source);
            Assert.AreEqual(3, _server.Sources.Count);

            ExceptionAssert.Throws<Exception>(() => _server.AddSource(_server.Sources.Values.First()));
        }

        [TestMethod]
        public async void DataServer_Query()
        {
            var table = new DataTable();
            await table.AddFieldAsync(_server, "1:banana");
            await table.AddFieldAsync(_server, "2:cheers");

            await _server.Query(TestUser.DummyUser, table);
            DatedSample[] samples1 = table.Fields.SelectMany(f => f.DatedSamples).ToArray();
            Assert.AreEqual(2, samples1.Length);

            await _server.Query(TestUser.DummyUser, table);
            DatedSample[] samples2 = table.Fields.SelectMany(f => f.DatedSamples).ToArray();
            Assert.AreEqual(2, samples2.Length);

            CollectionAssert.AreNotEqual(samples1, samples2);
        }

        private readonly DataServer _server = new DataServer(
            new SimpleDataSourceProvider(new TestDataSource("1")),
            new SimpleDataSourceProvider(new TestDataSource("2"), new TestDataSource("3")));

        /// <summary>
        /// A test data source.
        /// </summary>
        private sealed class TestDataSource : SimpleDataSourceBase
        {
            public TestDataSource(string name)
                : base(name)
            {
                AddTag("banana");
                AddTag("cheers");
            }

            #region Overrides of SimpleDataSourceBase

            /// <inheritdoc />
            public override async Task QueryFieldsAsync(IUser user, DataField[] fields)
            {
                Assert.IsTrue(fields.All(f => f.Tag.Identifier.Source == Name));

                foreach (var field in fields)
                {
                    Assert.IsNull(field.DatedSamples);
                    field.DatedSamples = new[] { new DatedSample(DateTime.UtcNow, this) };

                    Assert.IsNull(field.RawSamples);
                    field.RawSamples = new[] { this };
                }

                await Task.Delay(100);
            }

            #endregion
        }
    }
}

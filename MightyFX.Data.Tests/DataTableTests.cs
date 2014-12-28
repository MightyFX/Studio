using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MightyFX.Data.Tests
{
    [TestClass]
    public class DataTableTests
    {
        [TestMethod]
        public void DataTable_AddField()
        {
            var table = new DataTable();
            var field = table.AddField(new SimpleTag("haha:123"));
            
            Assert.AreEqual(table.Fields[0], field);
            Assert.AreEqual("haha:123", table.Fields[0].Tag.Identifier);
        }
        
        [TestMethod]
        public void DataTable_ClearSamples_ClearsSamplesFromAllFields()
        {
            var table = new DataTable();

            for (int i = 0; i < 2; ++i)
            {
                var field = table.AddField(new SimpleTag("haha:123"));
                field.RawSamples = new List<object>();
                field.DatedSamples = new List<DatedSample>();
            }

            table.ClearSamples();

            Assert.AreEqual(2, table.Fields.Count);
            Assert.IsTrue(table.Fields.Any(f => f.RawSamples == null));
            Assert.IsTrue(table.Fields.Any(f => f.DatedSamples == null));
        }
    }
}

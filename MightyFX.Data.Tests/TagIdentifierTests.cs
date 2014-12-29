using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MightyFX.TestUtilities;

namespace MightyFX.Data.Tests
{
    [TestClass]
    public class TagIdentifierTests : AutoTest
    {
        [TestMethod]
        public void TagIdentifier_FromString_Valid()
        {
            TagIdentifier id = "haha:123";
            Assert.AreEqual("haha", id.Source);
            Assert.AreEqual("123", id.Name);
        }

        [TestMethod]
        public void TagIdentifier_FromString_InvalidFormat_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => new TagIdentifier("haha"));
            ExceptionAssert.Throws<ArgumentException>(() => new TagIdentifier("haha:123:yes"));
        }

        [TestMethod]
        public void TagIdentifier_Equality()
        {
            Assert.AreEqual("haha:123", new TagIdentifier("haha:123"));
            Assert.AreNotEqual("whatever:123", new TagIdentifier("yes:456"));
        }
    }
}

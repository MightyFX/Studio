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
            TagIdentifier id = "haha.123";
            Assert.AreEqual("haha", id.Source);
            Assert.AreEqual("123", id.Name);

            TagIdentifier id2 = "haha.123.asdf";
            Assert.AreEqual("haha", id2.Source);
            Assert.AreEqual("123.asdf", id2.Name);
        }

        [TestMethod]
        public void TagIdentifier_FromString_InvalidFormat_ThrowsException()
        {
            ExceptionAssert.Throws<ArgumentException>(() => new TagIdentifier("haha"));
        }

        [TestMethod]
        public void TagIdentifier_Equality()
        {
            Assert.AreEqual("haha.123", new TagIdentifier("haha.123"));
            Assert.AreEqual("haha.123", new TagIdentifier("HAHA.123"));
            Assert.AreNotEqual("whatever.123", new TagIdentifier("yes.456"));
        }

        [TestMethod]
        public void TagIdentifier_GetHashCode()
        {
            Assert.AreEqual(new TagIdentifier("HAHA.123").GetHashCode(), new TagIdentifier("haha.123").GetHashCode());
            Assert.AreNotEqual(new TagIdentifier("34.123").GetHashCode(), new TagIdentifier("haha.123").GetHashCode());
        }
    }
}

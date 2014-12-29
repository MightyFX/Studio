using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MightyFX.TestUtilities
{
    /// <summary>
    /// The base class of all tests.
    /// </summary>
    public abstract class AutoTest
    {
        [TestInitialize]
        public void AutoTest_TestInitialize()
        {
            TestInitialize();
        }

        public virtual void TestInitialize()
        {
        }

        [TestCleanup]
        public void AutoTest_TestCleanup()
        {
            TestCleanup();
        }

        public virtual void TestCleanup()
        {
        }
    }
}

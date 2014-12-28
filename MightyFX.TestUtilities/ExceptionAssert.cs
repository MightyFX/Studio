using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MightyFX.TestUtilities
{
    /// <summary>
    /// Provides functionality to test exception throwing.
    /// </summary>
    public static class ExceptionAssert
    {
        /// <summary>
        /// Asserts if the provided action does not throw the excepted exception type.
        /// </summary>
        /// <typeparam name="TException">The type of exception to expect.</typeparam>
        /// <param name="test">The test action to execute.</param>
        public static void Throws<TException>(Action test)
            where TException : Exception
        {
            Throws<TException>("Did not catch excepted exception type.", test);
        }

        /// <summary>
        /// Asserts if the provided action does not throw the excepted exception type.
        /// </summary>
        /// <param name="message">The failure message to display.</param>
        /// <typeparam name="TException">The type of exception to expect.</typeparam>
        /// <param name="test">The test action to execute.</param>
        public static void Throws<TException>(string message, Action test)
            where TException : Exception
        {
            Exception caughtException = null;
            
            try
            {
                test();
            }
            catch (Exception e)
            {
                caughtException = e;
            }

            Assert.IsInstanceOfType(caughtException, typeof(TException), message);
        }
    }
}

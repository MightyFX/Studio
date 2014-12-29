using MightyFX.Users;

namespace MightyFX.TestUtilities
{
    /// <summary>
    /// An implementation of <see cref="IUser"/> for testing purposes.
    /// </summary>
    public class TestUser : IUser
    {
        #region Implementation of IUser

        /// <inheritdoc />
        public string UserName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string DisplayName
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Email
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// A dummy user to use.
        /// </summary>
        public static IUser DummyUser = new TestUser
            {
                DisplayName = "Dummy User", 
                Email = "dummy@test.com",
                UserName = "dummy"
            };
    }
}

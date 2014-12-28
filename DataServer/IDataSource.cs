using System.Collections.Generic;
using System.Threading.Tasks;
using MightyFX.Users;

namespace MightyFX.Data
{
    /// <summary>
    /// An asynchronous data source that can be queried.
    /// </summary>
    public interface IDataSource
    {
        /// <summary>
        /// Gets the unique name of the data source as used by <see cref="TagIdentifier.Source"/>.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// Gets a description of the data source.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// Returns the tags in this data source.
        /// </summary>
        /// <returns>The tags.</returns>
        Task<IEnumerable<ITag>> ListTagsAsync();

        /// <summary>
        /// Returns the tag identified or null if it is not found.
        /// </summary>
        /// <param name="tagId">The tag identifier to look for.</param>
        /// <returns>The tag or null if it was not found.</returns>
        Task<ITag> FindTagAsync(TagIdentifier tagId);

        /// <summary>
        /// Returns the access roles for the given user for this data source.
        /// </summary>
        /// <param name="user">The user to check.</param>
        /// <returns>The roles this user has in this data source.</returns>
        Task<AccessRoles> CheckAccessAsync(IUser user);

        /// <summary>
        /// Queries and fills the given table.
        /// </summary>
        /// <param name="user">The user that is requesting the data.</param>
        /// <param name="fields">The fields to fill.</param>
        /// <returns>A task representing when the operation is complete.</returns>
        Task QueryFieldsAsync(IUser user, DataField[] fields);
    }
}
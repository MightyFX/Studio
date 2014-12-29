using System.Collections.Generic;
using System.Linq;
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
        /// <remarks>
        /// Cannot change once added to the <see cref="DataServer"/>.
        /// </remarks>
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
        Task<IEnumerable<ITag>> GetTagsAsync();

        /// <summary>
        /// Returns the tag identified or null if it is not found.
        /// </summary>
        /// <param name="tagId">The tag identifier to look for.</param>
        /// <returns>The tag or null if it was not found.</returns>
        Task<ITag> ResolveTagAsync(TagIdentifier tagId);

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

    /// <summary>
    /// An implementation of <see cref="IDataSource"/> for sources that have a constant set of tags that are known upon construction.
    /// </summary>
    public abstract class SimpleDataSourceBase : IDataSource
    {
        /// <summary>
        /// Constructs a new instance of <see cref="SimpleDataSourceBase"/>.
        /// </summary>
        /// <param name="name">The name of the data source.</param>
        protected SimpleDataSourceBase(string name)
        {
            Name = name;
            Description = string.Empty;
            _tags = new Dictionary<TagIdentifier, ITag>();
        }

        #region Implementation of IDataSource

        /// <inheritdoc />
        public string Name
        {
            get;
            private set;
        }

        /// <inheritdoc />
        public string Description
        {
            get;
            protected set;
        }

        /// <inheritdoc />
        public Task<IEnumerable<ITag>> GetTagsAsync()
        {
            return Task.FromResult(_tags.Values.AsEnumerable());
        }

        /// <inheritdoc />
        public Task<ITag> ResolveTagAsync(TagIdentifier tagId)
        {
            ITag tag;
            _tags.TryGetValue(tagId, out tag);
            return Task.FromResult(tag);
        }

        /// <inheritdoc />
        public virtual Task<AccessRoles> CheckAccessAsync(IUser user)
        {
            return Task.FromResult(AccessRoles.ReadOnly);
        }

        /// <inheritdoc />
        public abstract Task QueryFieldsAsync(IUser user, DataField[] fields);

        #endregion

        /// <summary>
        /// The dictionary of tags.
        /// </summary>
        private readonly IDictionary<TagIdentifier, ITag> _tags;

        /// <summary>
        /// Adds a <see cref="SimpleTag"/> to this data source with the given name (using the name of the data source) and returns the tag for further modification.
        /// </summary>
        /// <param name="name">The name of the tag in this data source.</param>
        /// <returns>The tag for further modification.</returns>
        protected SimpleTag AddTag(string name)
        {
            var tag = new SimpleTag(new TagIdentifier(Name, name));
            AddTag(tag);
            return tag;
        }

        /// <summary>
        /// Adds the tag to this data source.
        /// </summary>
        /// <param name="tag">The tag to add.</param>
        protected void AddTag(ITag tag)
        {
            _tags.Add(tag.Identifier, tag);
        }
    }
}
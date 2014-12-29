using System;
using System.Threading.Tasks;

namespace MightyFX.Data
{
    /// <summary>
    /// Helper methods for dealing with the types in this assembly.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Adds a field to a data table with a given tag.
        /// </summary>
        /// <param name="table">The data to which to add a field.</param>
        /// <param name="tag">The tag to add as a field.</param>
        /// <returns>The added field.</returns>
        public static DataField AddField(this DataTable table, ITag tag)
        {
            if (tag == null)
            {
                throw new ArgumentNullException("tag");
            }

            var field = new DataField(table, tag);
            table.Fields.Add(field);
            return field;
        }

        /// <summary>
        /// Adds a field to the data table given a tag identifier and a server to search the tag in.
        /// </summary>
        /// <param name="table">The data to which to add a field.</param>
        /// <param name="server">The data server to search for the tag.</param>
        /// <param name="tagId">The tag to add as a field.</param>
        /// <returns>The added field.</returns>
        public static async Task<DataField> AddFieldAsync(this DataTable table, DataServer server, TagIdentifier tagId)
        {
            return table.AddField(await server.ResolveTagAsync(tagId));
        }

        /// <summary>
        /// Returns the tag identified or null if it is not found.
        /// </summary>
        /// <param name="server">The data server to search for the tag.</param>
        /// <param name="tagId">The tag identifier to look for.</param>
        /// <returns>The tag or null if it was not found.</returns>
        public static Task<ITag> ResolveTagAsync(this DataServer server, TagIdentifier tagId)
        {
            return server.Sources[tagId.Source].ResolveTagAsync(tagId.Name);
        }
    }
}
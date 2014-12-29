using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using MightyFX.Users;

namespace MightyFX.Data
{
    /// <summary>
    /// An MEF singleton that provides querying abilities for the data sources exported by <see cref="IDataSourceProvider"/>.
    /// </summary>
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class DataServer
    {
        /// <summary>
        /// Creates an instance of <see cref="DataServer"/> with MEF-exported providers.
        /// </summary>
        /// <param name="providers">The data source providers we will serve.</param>
        [ImportingConstructor]
        public DataServer(params IDataSourceProvider[] providers)
        {
            _sources = new Dictionary<string, IDataSource>(StringComparer.InvariantCultureIgnoreCase);

            Providers = providers;
            Providers.ForEach(p => p.ConfigureDataServer(this));
        }

        /// <summary>
        /// Gets the data source providers we are serving.
        /// </summary>
        public IReadOnlyList<IDataSourceProvider> Providers
        {
            get;
            private set;
        }

        /// <summary>
        /// Backing for <see cref="Sources"/>.
        /// </summary>
        private readonly Dictionary<string, IDataSource> _sources;

        /// <summary>
        /// Gets the sources we support for querying across all providers.
        /// </summary>
        public IReadOnlyDictionary<string, IDataSource> Sources
        {
            get
            {
                return new ReadOnlyDictionary<string, IDataSource>(_sources);
            }
        }

        /// <summary>
        /// Adds a data source to the server.
        /// </summary>
        /// <param name="source">The source to add. Must have a unique name.</param>
        /// <remarks>
        /// Not currently thread-safe.
        /// </remarks>
        public void AddSource(IDataSource source)
        {
            _sources.Add(source.Name, source);
        }

        /// <summary>
        /// Removes a data source from the server.
        /// </summary>
        /// <param name="source">The source to remove.</param>
        /// <remarks>
        /// Not currently thread-safe.
        /// </remarks>
        public void RemoveSource(IDataSource source)
        {
            _sources.Remove(source.Name);
        }

        /// <summary>
        /// Queries and fills the given table.
        /// </summary>
        /// <param name="user">The user that is requesting the data. If null, it is the anonymous user.</param>
        /// <param name="table">The table to fill.</param>
        public Task<DataField[]>[] QueryAsync(IUser user, DataTable table)
        {
            table.ClearSamples();

            IEnumerable<Task<DataField[]>> queryTasks =
                table.Fields.GroupBy(f => _sources[f.Tag.Identifier.Source]).Select(async fs =>
                    {
                        DataField[] fields = fs.ToArray();
                        await fs.Key.QueryFieldsAsync(user, fields);
                        return fields;
                    });

            return queryTasks.ToArray();
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "Providers = {0}, Sources = [{1}]",
                Providers.Count, Sources.Keys.ToCsv());
        }

        #endregion
    }
}
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MightyFX.Users;

namespace MightyFX.Data
{
    /// <summary>
    /// Provides data sources for the data server. Export this interface to add data sources.
    /// </summary>
    public interface IDataSourceProvider
    {
        /// <summary>
        /// Gets the sources that are provided. Not yet thread-safe to change while a query is happening.
        /// </summary>
        /// <remarks>
        /// Source names must be unique for a data server. We don't currently check this.
        /// </remarks>
        IEnumerable<IDataSource> Sources
        {
            get;
        }
    }

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
        public DataServer(IEnumerable<IDataSourceProvider> providers)
        {
            Providers = providers;
        }

        /// <summary>
        /// Gets the data source providers we are serving.
        /// </summary>
        public IEnumerable<IDataSourceProvider> Providers
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of sources we support for querying across all providers.
        /// </summary>
        public IEnumerable<IDataSource> Sources
        {
            get
            {
                return Providers.SelectMany(p => p.Sources);
            }
        }

        /// <summary>
        /// Queries and fills the given table.
        /// </summary>
        /// <param name="user">The user that is requesting the data.</param>
        /// <param name="result">The table to fill.</param>
        public async void Query(IUser user, DataTable result)
        {
            result.ClearSamples();

            var fieldsBySource = result.Fields.GroupBy(f => this.FindSource(f.Tag.Identifier.Source)).ToList();
            await Task.WhenAll(fieldsBySource.Select(fs => fs.Key.QueryFieldsAsync(user, fs.ToArray())));
        }
    }
}
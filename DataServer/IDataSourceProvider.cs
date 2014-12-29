using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MightyFX.Data
{
    /// <summary>
    /// Provides data sources for the data server. Export this interface to add data sources.
    /// </summary>
    public interface IDataSourceProvider
    {
        /// <summary>
        /// Configures the data server by adding sources and/or storing the <see cref="DataServer"/> instance to add/remove sources in the future.
        /// </summary>
        /// <param name="server">The data server to configure.</param>
        void ConfigureDataServer(DataServer server);
    }

    /// <summary>
    /// An implementation of <see cref="IDataSourceProvider"/> for basic general-use.
    /// </summary>
    public sealed class SimpleDataSourceProvider : IDataSourceProvider
    {
        /// <summary>
        /// Creates an instance of <see cref="IDataSourceProvider"/>.
        /// </summary>
        /// <param name="sources">The sources that will be added by this provider.</param>
        public SimpleDataSourceProvider(params IDataSource[] sources)
        {
            Sources = sources;
        }

        /// <summary>
        /// Gets the data sources that are registered by this provider.
        /// </summary>
        public IReadOnlyList<IDataSource> Sources
        {
            get;
            private set;
        }

        #region Implementation of IDataSourceProvider

        /// <inheritdoc />
        public void ConfigureDataServer(DataServer server)
        {
            foreach (var source in Sources)
            {
                server.AddSource(source);
            }
        }

        #endregion

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "Sources = [{0}]",
                Sources.Select(s => s.Name).ToCsv());
        }

        #endregion
    }
}
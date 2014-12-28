using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Security.Authentication;
using System.Threading.Tasks;
using MightyFX.Users;

namespace MightyFX.Data
{
    public interface IDataSourceProvider
    {
        /// <summary>
        /// Gets the sources that are provided. Not yet thread-safe to change while a query is happening.
        /// </summary>
        IEnumerable<IDataSource> Sources
        {
            get;
        }
    }

    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class DataServer
    {
        [ImportingConstructor]
        public DataServer(IEnumerable<IDataSourceProvider> providers)
        {
            Providers = providers;
        }

        public IEnumerable<IDataSourceProvider> Providers
        {
            get;
            private set;
        }

        public IEnumerable<IDataSource> Sources
        {
            get
            {
                return Providers.SelectMany(p => p.Sources);
            }
        }

        public async void Query(IUser user, DataTable result)
        {
            result.ClearSamples();

            var fieldsBySource = result.Fields.GroupBy(f => this.FindSource(f.Tag.Source)).ToList();
            
            var accessRoles = await Task.WhenAll(fieldsBySource.Select(fs => fs.Key.CheckAccess(user)));
            if (accessRoles.Any(r => !r.CanRead()))
            {
                throw new InvalidCredentialException();
            }

            await Task.WhenAll(fieldsBySource.Select(fs => fs.Key.QueryField(fs.ToArray())));
        }
    }
}
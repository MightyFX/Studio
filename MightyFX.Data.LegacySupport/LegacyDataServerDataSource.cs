using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data;
using System.Globalization;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using MightyFX.Data.LegacySupport.WebService;
using MightyFX.Users;

namespace MightyFX.Data.LegacySupport
{
    [Export(typeof(IDataSourceProvider))]
    public sealed class LegacyDataSourceProvider : SimpleDataSourceProvider
    {
        internal dataserverSoapClient WebService;

        public LegacyDataSourceProvider()
        {
            WebService = new dataserverSoapClient(new BasicHttpBinding(), new EndpointAddress("http://pgcs04.carmeusena.com/dataserver/dataserver.asmx"));

            string[] servers = WebService.login("abarreto", string.Empty, WebService.Endpoint.Address.Uri.Host, true);
            foreach (string server in servers)
            {
                AddSource(new LegacyDataServerDataSource(this, server));
            }
        }
    }

    public sealed class LegacyDataServerDataSource : IDataSource
    {
        private readonly LegacyDataSourceProvider _provider;

        public LegacyDataServerDataSource(LegacyDataSourceProvider provider, string name)
        {
            _provider = provider;
            Name = name;
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
            get
            {
                return string.Format(CultureInfo.InvariantCulture,
                    "Legacy DataServer adapter for <{0}> running on <{1}>.",
                    Name, _provider.WebService.Endpoint);
            }
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ITag>> GetTagsAsync()
        {
            // var x = await _provider.WebService.get_tagsAsync(Name);
            return null;
        }

        /// <inheritdoc />
        public async Task<ITag> ResolveTagAsync(TagIdentifier tagId)
        {
            return new SimpleTag(tagId);
        }

        /// <inheritdoc />
        public Task<AccessRoles> CheckAccessAsync(IUser user)
        {
            return Task.FromResult(AccessRoles.ReadOnly);
        }

        /// <inheritdoc />
        public async Task QueryFieldsAsync(IUser user, DataField[] fields)
        {
            DataTable table = fields[0].Table;
            string[] tagNames = fields.Select(f => f.Tag.Identifier.Name).ToArray();
            int intervalAsSeconds = (int)Math.Ceiling(table.SampleInterval.TotalSeconds);

            DataSet results = await _provider.WebService.queryAsync(Name, 1, tagNames, table.StartTime, table.EndTime, intervalAsSeconds);

            object[][] data = new object[fields.Length][];
            int rowCount = results.Tables[0].Rows.Count;
            for (int i = 0; i < fields.Length; ++i)
            {
                data[i] = new object[rowCount];
                fields[i].SetRawSamples(data[i]);
            }

            int rowIndex = 0;
            foreach (DataRow row in results.Tables[0].Rows)
            {
                for (int columnIndex = 0; columnIndex < fields.Length; ++columnIndex)
                {
                    data[columnIndex][rowIndex] = row[columnIndex + 1];
                }
                rowIndex++;
            }
        }

        #endregion
    }
}

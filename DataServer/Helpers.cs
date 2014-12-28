using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Threading.Tasks;

namespace MightyFX.Data
{
    public static class Helpers
    {
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

        public static async Task<DataField> AddFieldAsync(this DataTable table, DataServer server, TagIdentifier tagId)
        {
            return table.AddField(await server.ResolveTagAsync(tagId));
        }

        public static Task<ITag> ResolveTagAsync(this DataServer server, TagIdentifier tagId)
        {
            return server.FindSource(tagId.Source).FindTagAsync(tagId.Name);
        }

        public static IDataSource FindSource(this DataServer server, string name)
        {
            return server.Sources.First(source => source.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
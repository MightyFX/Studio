using System;
using System.Linq;
using System.Threading.Tasks;

namespace MightyFX.Data
{
    public static class Helpers
    {
        public static DataField AddField(this DataTable table, ITag tag)
        {
            var field = new DataField(table, tag);
            table.Fields.Add(field);
            return field;
        }

        public static async Task<DataField> AddField(this DataTable table, DataServer server, string tag)
        {
            return table.AddField(await server.ResolveTag(tag));
        }

        public static Task<ITag> ResolveTag(this DataServer server, string tag)
        {
            string[] parts = tag.Split(':');
            return server.FindSource(parts[0]).FindTag(parts[1]);
        }

        public static IDataSource FindSource(this DataServer server, string name)
        {
            return server.Sources.First(source => source.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
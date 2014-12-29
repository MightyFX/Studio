using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MightyFX.Data;
using MightyFX.Data.LegacySupport;
using MightyFX.Users;
using Newtonsoft.Json;

namespace MightyFX.Studio.Controllers
{
    public class AwesomeController : ApiController
    {
        // GET: api/Awesome
        public async Task<IEnumerable<string>> Get([FromUri] DataTable table)
        {
            DataServer server = new DataServer(
                new SimpleDataSourceProvider(new StocksDataSource()),
                new LegacyDataSourceProvider());

            ////var table = new DataTable();
            ////table.StartTime = startTime;
            ////table.EndTime = endTime;
            ////table.SampleInterval = sampleInterval;
            ////foreach (string t in ticker)
            ////{
            ////    await AddStock(server, table, t);
            ////}
            await Task.WhenAll(server.QueryAsync(null, table));

            return table.Fields.Select(f => f.RawSamples[0].ToString());
        }

        ////// GET: api/Awesome/5
        ////public async Task<string> Get(int ticker)
        ////{
        ////    DataServer server = new DataServer(new SimpleDataSourceProvider(new StocksDataSource()));
            
        ////    var table = new DataTable();
        ////    await AddStock(server, table, "NATI");
        ////    await AddStock(server, table, "MSFT");
        ////    server.QueryAsync(null, table);

        ////    return table.Fields[0].RawSamples[0] + " -- " + table.Fields[1].RawSamples[0];
        ////}

        private static async Task AddStock(DataServer server, DataTable table, string ticker)
        {
            var field = await table.AddFieldAsync(server, ticker);
            field.QueryType = QueryType.LatestSample;
        }

        // POST: api/Awesome
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Awesome/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Awesome/5
        public void Delete(int id)
        {
        }
    }

    public class StocksDataSource : SimpleDataSourceBase
    {
        public StocksDataSource()
            : base("stocks")
        {
            AddTag("NATI");
            AddTag("MSFT");
            AddTag("A");
        }

        #region Overrides of SimpleDataSourceBase

        public override async Task QueryFieldsAsync(IUser user, DataField[] fields)
        {
            if (fields.Any(f => f.QueryType != QueryType.LatestSample))
            {
                throw new InvalidOperationException("Only supports latest sample.");
            }

            using (var client = new HttpClient())
            {
                string tickers = fields.Select(f => f.Tag.Identifier.Name).ToCsv();
                string data = await client.GetStringAsync("http://finance.google.com/finance/info?q=" + tickers);
                dynamic stocks = JsonConvert.DeserializeObject(data.Replace("//", ""));

                int i = 0;
                foreach (var field in fields)
                {
                    field.SetRawSamples(new double[] { stocks[i].l_cur });
                    ++i;
                }
            }
        }

        #endregion
    }
}

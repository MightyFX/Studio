using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace MightyFX.Data
{
    /// <summary>
    /// A data table of query results.
    /// </summary>
    public sealed class DataTable
    {
        /// <summary>
        /// Constructs an empty <see cref="DataTable"/>.
        /// </summary>
        public DataTable()
        {
            Fields = new List<DataField>();
        }

        /// <summary>
        /// Gets the list of fields this table contains.
        /// </summary>
        public IList<DataField> Fields
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the start time of the query.
        /// </summary>
        public DateTime StartTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the end time of the query.
        /// </summary>
        public DateTime EndTime
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time interval of the query.
        /// </summary>
        public TimeSpan SampleInterval
        {
            get;
            set;
        }

        /// <summary>
        /// Clears the sample data from all fields.
        /// </summary>
        public void ClearSamples()
        {
            Fields.ForEach(f => f.ClearSamples());
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, 
                "StartTime = {0}, EndTime = {1}, SampleInterval = {2}, Tags: [{3}]",
                StartTime, EndTime, SampleInterval, Fields.Select(f => f.Tag.Identifier).ToCsv());
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace MightyFX.Data
{
    /// <summary>
    /// The type of query data the field contains.
    /// </summary>
    public enum QueryType
    {
        InterpolatedOverInterval,
        SampleClosestToInterval,
        AverageOverInterval,
        MinimumOverInterval,
        MaximumOverInterval,
        IntegralOverInterval,
        UniquePointsOnly,
        AllPoints,
    }

    /// <summary>
    /// A dated sample.
    /// </summary>
    public struct DatedSample
    {
        /// <summary>
        /// The time of the sample.
        /// </summary>
        public DateTime Time;

        /// <summary>
        /// The sample value.
        /// </summary>
        public object Value;
    }

    /// <summary>
    /// A field in a data table.
    /// </summary>
    public sealed class DataField
    {

        /// <summary>
        /// Gets the table this field belongs to.
        /// </summary>
        public DataTable Table
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the tag this field represents.
        /// </summary>
        public ITag Tag
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the type of the query.
        /// </summary>
        public QueryType QueryType
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets extra options to pass onto the tag's data source. May be ignored.
        /// </summary>
        public string ExtraOptions
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of dated samples for this field. Can be null.
        /// </summary>
        public IList<DatedSample> DatedSamples
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a list of raw samples for this field. Can be null.
        /// </summary>
        public IList RawSamples
        {
            get;
            set;
        }

        /// <summary>
        /// Constructs a new instance of <see cref="DataField"/>.
        /// </summary>
        /// <param name="table">The table this field belongs to.</param>
        /// <param name="tag">The tag this field represents.</param>
        public DataField(DataTable table, ITag tag)
        {
            Table = table;
            Tag = tag;
        }

        /// <summary>
        /// Clears this field's sample data.
        /// </summary>
        public void ClearSamples()
        {
            DatedSamples = null;
            RawSamples = null;
        }
    }

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
    }
}

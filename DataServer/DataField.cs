using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace MightyFX.Data
{
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
        public IEnumerable<DatedSample> DatedSamples
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

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            string sampleCount = RawSamples == null ? "null" : RawSamples.Count.ToString();
            return string.Format(CultureInfo.InvariantCulture,
                "Tag = {0}, QueryType = {1}, SampleCount = {2}",
                Tag.Identifier, QueryType, sampleCount);
        }

        #endregion
    }
}
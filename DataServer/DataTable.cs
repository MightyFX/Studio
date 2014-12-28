using System;
using System.Collections.Generic;

namespace MightyFX.Data
{
    public enum FieldType
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

    public sealed class DataField
    {
        public struct Sample
        {
            public DateTime Time;

            public object Value;
        }

        public DataTable Table
        {
            get;
            private set;
        }

        public ITag Tag
        {
            get;
            private set;
        }

        public FieldType FieldType
        {
            get;
            set;
        }

        public string ExtraOptions
        {
            get;
            set;
        }

        public IList<Sample> DatedSamples
        {
            get;
            set;
        }

        public IList<object> Samples
        {
            get;
            set;
        }

        public DataField(DataTable table, ITag tag)
        {
            Table = table;
            Tag = tag;
        }

        public void ClearSamples()
        {
            DatedSamples = null;
            Samples = null;
        }
    }

    public sealed class DataTable
    {
        public DataTable()
        {
            SyncLock = new object();
            Fields = new List<DataField>();
        }

        public object SyncLock
        {
            get;
            private set;
        }

        public IList<DataField> Fields
        {
            get;
            private set;
        }

        public DateTime StartTime
        {
            get;
            set;
        }

        public DateTime EndTime
        {
            get;
            set;
        }

        public TimeSpan SampleInterval
        {
            get;
            set;
        }

        public void ClearSamples()
        {
            Fields.ForEach(f => f.ClearSamples());
        }
    }
}

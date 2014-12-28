namespace MightyFX.Data
{
    public enum TagType
    {
        /// <summary>
        ///  Represents a true/false value. Coded as a numeric 0 or 1.
        /// </summary>
        Boolean,

        /// <summary>
        /// Represents a floating-point value (double).
        /// </summary>
        Numeric,

        /// <summary>
        /// Represents a string value.
        /// </summary>
        String
    }

    public interface ITag
    {
        string Source
        {
            get;
        }

        string Name
        {
            get;
        }

        string Description
        {
            get;
        }

        string Units
        {
            get;
        }

        TagType Type
        {
            get;
        }

        double MinimumValue
        {
            get;
        }

        double MaximumValue
        {
            get;
        }
    }

    public class SimpleTag : ITag
    {
        #region Implementation of ITag

        public string Source
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public string Units
        {
            get;
            set;
        }

        public TagType Type
        {
            get;
            set;
        }

        public double MinimumValue
        {
            get;
            set;
        }

        public double MaximumValue
        {
            get;
            set;
        }

        #endregion
    }
}
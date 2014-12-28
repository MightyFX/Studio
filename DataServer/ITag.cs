namespace MightyFX.Data
{
    /// <summary>
    /// The type of values returned by a tag.
    /// </summary>
    public enum TagType
    {
        /// <summary>
        /// Returns a true/false value. Coded as a numeric 0 or 1.
        /// </summary>
        Boolean,

        /// <summary>
        /// Returns a floating-point value (double).
        /// </summary>
        Numeric,

        /// <summary>
        /// Returns a string value.
        /// </summary>
        String
    }

    /// <summary>
    /// A tag in a <see cref="IDataSource"/> that represents a series of values.
    /// </summary>
    public interface ITag
    {
        /// <summary>
        /// Gets the tag identifier.
        /// </summary>
        TagIdentifier Identifier
        {
            get;
        }

        /// <summary>
        /// Gets a description of the tag beyond it's name.
        /// </summary>
        string Description
        {
            get;
        }

        /// <summary>
        /// Gets the units that values returned by this tag are in.
        /// </summary>
        string Units
        {
            get;
        }

        /// <summary>
        /// Gets the type of values returned by this tag.
        /// </summary>
        TagType Type
        {
            get;
        }

        /// <summary>
        /// Gets the minimum value for this tag, if it is a <see cref="TagType.Numeric"/>.
        /// </summary>
        double MinimumValue
        {
            get;
        }

        /// <summary>
        /// Gets the maximum value for this tag, if it is a <see cref="TagType.Numeric"/>.
        /// </summary>
        double MaximumValue
        {
            get;
        }
    }

    /// <summary>
    /// A simple implementation for <see cref="ITag"/> for general use.
    /// </summary>
    public class SimpleTag : ITag
    {
        #region Implementation of ITag

        /// <inheritdoc />
        public TagIdentifier Identifier
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Description
        {
            get;
            set;
        }

        /// <inheritdoc />
        public string Units
        {
            get;
            set;
        }

        /// <inheritdoc />
        public TagType Type
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MinimumValue
        {
            get;
            set;
        }

        /// <inheritdoc />
        public double MaximumValue
        {
            get;
            set;
        }

        #endregion

        /// <summary>
        /// Constructs an instance of <see cref="SimpleTag"/>.
        /// </summary>
        /// <param name="identifier">The tag identifier to use.</param>
        public SimpleTag(TagIdentifier identifier)
        {
            Identifier = identifier;
        }
    }
}
namespace MightyFX.Data
{
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
    /// An implementation of <see cref="ITag"/> for basic general-use.
    /// </summary>
    public sealed class SimpleTag : ITag
    {
        #region Implementation of ITag

        /// <inheritdoc />
        public TagIdentifier Identifier
        {
            get;
            private set;
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
            Description = string.Empty;
            Units = string.Empty;
            Type = TagType.Numeric;
            MinimumValue = double.MinValue;
            MaximumValue = double.MaxValue;
        }

        #region Overrides of Object

        /// <inheritdoc />
        public override string ToString()
        {
            return Identifier.ToString();
        }

        #endregion
    }
}
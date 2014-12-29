using System;

namespace MightyFX.Data
{
    /// <summary>
    /// Uniquely identifies a tag among all the sources in a single instance of <see cref="DataServer"/>. Tags are case-insensitive.
    /// </summary>
    public struct TagIdentifier : IEquatable<TagIdentifier>
    {
        /// <summary>
        /// The separator between the source and name of the tag.
        /// </summary>
        public const char Separator = '.';

        /// <summary>
        /// The separator between the source and name of the tag.
        /// </summary>
        private static readonly char[] _separatorArray = { Separator };

        /// <summary>
        /// The name of the <see cref="IDataSource"/> to find this tag.
        /// </summary>
        public readonly string Source;

        /// <summary>
        /// The name of the <see cref="ITag"/> inside it's <see cref="IDataSource"/>.
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Cached hashcode.
        /// </summary>
        private readonly int _hashCode;

        /// <summary>
        /// Copies an existing tag identifier.
        /// </summary>
        /// <param name="copyFrom">The tag identifier to copy.</param>
        public TagIdentifier(TagIdentifier copyFrom)
            : this(copyFrom.Source, copyFrom.Name)
        {
        }

        /// <summary>
        /// Constructs a new instance of <see cref="TagIdentifier"/>.
        /// </summary>
        /// <param name="source">The name of the <see cref="IDataSource"/> to find this tag.</param>
        /// <param name="name">The name of the <see cref="ITag"/> inside it's <see cref="IDataSource"/>.</param>
        public TagIdentifier(string source, string name)
        {
            Source = source;
            Name = name;
            _hashCode = (Source != null ? Source.ToLowerInvariant().GetHashCode() : 0) * 397 ^ (Name != null ? Name.ToLowerInvariant().GetHashCode() : 0);
        }

        #region Equality / Comparison

        /// <inheritdoc />
        public bool Equals(TagIdentifier other)
        {
            return Source.Equals(other.Source, StringComparison.InvariantCultureIgnoreCase)
                   && Name.Equals(other.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is TagIdentifier && Equals((TagIdentifier)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return _hashCode;
        }

        /////// <inheritdoc />
        ////public int CompareTo(TagIdentifier other)
        ////{
        ////    int value = StringComparer.InvariantCultureIgnoreCase.Compare(Source, other.Source);
        ////    if (value == 0)
        ////    {
        ////        value = StringComparer.InvariantCultureIgnoreCase.Compare(Name, other.Name);
        ////    }

        ////    return value;
        ////}

        #endregion
        
        /// <inheritdoc />
        public override string ToString()
        {
            return Source + Separator + Name;
        }

        /// <summary>
        /// Implicitly converts strings into <see cref="TagIdentifier"/>.
        /// </summary>
        /// <param name="tag">The string to attempt to convert.</param>
        /// <returns>The parsed <see cref="TagIdentifier"/>.</returns>
        /// <exception cref="ArgumentException">Thrown if the string is not in the format of a tag.</exception>
        public static implicit operator TagIdentifier(string tag)
        {
            string[] parts = tag.Split(_separatorArray, 2);
            if (parts.Length != 2)
            {
                throw new ArgumentException("Invalid tag format: " + tag);
            }

            return new TagIdentifier(parts[0], parts[1]);
        }
    }
}
using System;

namespace MightyFX.Data
{
    /// <summary>
    /// A dated sample.
    /// </summary>
    public struct DatedSample : IEquatable<DatedSample>
    {
        /// <summary>
        /// The sample time.
        /// </summary>
        public readonly DateTime Time;

        /// <summary>
        /// The sample value.
        /// </summary>
        public readonly object Value;

        /// <summary>
        /// Constructs a new instance of <see cref="DatedSample"/>.
        /// </summary>
        /// <param name="time">The sample time.</param>
        /// <param name="value">The sample value.</param>
        public DatedSample(DateTime time, object value)
        {
            Time = time;
            Value = value;
        }

        #region Equality

        /// <inheritdoc />
        public bool Equals(DatedSample other)
        {
            return Time == other.Time && Value == other.Value;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is DatedSample && Equals((DatedSample)obj);
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            unchecked
            {
                return (Time.GetHashCode() * 397) ^ (Value != null ? Value.GetHashCode() : 0);
            }
        }

        #endregion

        /// <inheritdoc />
        public override string ToString()
        {
            return Time + ": " + Value;
        }
    }
}
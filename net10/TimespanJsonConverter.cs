using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

// Copyright (c) 2019 Blazored
// Modified by dsun on 2026
// MIT License
namespace MetaFrm.Razor.Storage
{
    /// <summary>
    /// The new Json.NET doesn't support Timespan at this time
    /// https://github.com/dotnet/corefx/issues/38641
    /// </summary>
    public class TimespanJsonConverter : JsonConverter<TimeSpan>
    {
        /// <summary>
        /// Format: Days.Hours:Minutes:Seconds:Milliseconds
        /// </summary>
        public const string TimeSpanFormatString = @"d\.hh\:mm\:ss\:FFF";

        /// <summary>
        /// Reads and converts the JSON string representation of a time interval to a <see cref="TimeSpan"/> object.
        /// </summary>
        /// <remarks>The method expects the JSON value to be a string formatted according to the expected
        /// time interval format. If the input does not match the required format, a <see cref="FormatException"/> is
        /// thrown.</remarks>
        /// <param name="reader">The reader to read the JSON value from. The reader must be positioned at a JSON string token representing a
        /// time interval.</param>
        /// <param name="typeToConvert">The type of the object to convert. This parameter is ignored by this implementation.</param>
        /// <param name="options">Options to control the behavior of the deserialization. This parameter is not used by this implementation.</param>
        /// <returns>A <see cref="TimeSpan"/> value parsed from the JSON string. Returns <see cref="TimeSpan.Zero"/> if the input
        /// is null, empty, or consists only of white-space characters.</returns>
        /// <exception cref="FormatException">Thrown if the JSON string is not in the expected time interval format.</exception>
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var s = reader.GetString();
            if (string.IsNullOrWhiteSpace(s))
            {
                return TimeSpan.Zero;
            }

            if (!TimeSpan.TryParseExact(s, TimeSpanFormatString, null, out var parsedTimeSpan))
            {
                throw new FormatException($"Input timespan is not in an expected format : expected {Regex.Unescape(TimeSpanFormatString)}. Please retrieve this key as a string and parse manually.");
            }

            return parsedTimeSpan;
        }

        /// <summary>
        /// Writes a TimeSpan value as a JSON string using the specified format.
        /// </summary>
        /// <remarks>The TimeSpan is formatted as a string according to the format specified by
        /// TimeSpanFormatString before being written to the JSON output. The output format must be compatible with the
        /// expected JSON schema for TimeSpan values.</remarks>
        /// <param name="writer">The Utf8JsonWriter to which the JSON string value will be written. Cannot be null.</param>
        /// <param name="value">The TimeSpan value to convert and write as a JSON string.</param>
        /// <param name="options">The serialization options to use when writing the value. This parameter is not used by this method but is
        /// required by the method signature.</param>
        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            var timespanFormatted = $"{value.ToString(TimeSpanFormatString)}";
            writer.WriteStringValue(timespanFormatted);
        }
    }
}
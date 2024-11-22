using System;

namespace Moongazing.GuidShorter;

/// <summary>
/// Represents a shorter version of a GUID, encoded as a 22-character Base64 string.
/// </summary>
public class ShortGuid
{
    /// <summary>
    /// The original GUID value.
    /// </summary>
    public Guid Value { get; private set; }

    private ShortGuid(Guid value)
    {
        Value = value;
    }

    private ShortGuid(string value)
    {
        Value = Decode(value);
    }

    /// <summary>
    /// Creates a ShortGuid from a regular GUID.
    /// </summary>
    /// <param name="guid">The GUID to convert.</param>
    /// <returns>A ShortGuid instance.</returns>
    public static ShortGuid FromGuid(Guid guid)
    {
        if (guid == Guid.Empty)
            throw new ArgumentException("GUID cannot be empty.", nameof(guid));

        return new ShortGuid(guid);
    }

    /// <summary>
    /// Creates a ShortGuid from a 22-character Base64 string.
    /// </summary>
    /// <param name="shortGuid">The encoded short GUID string.</param>
    /// <returns>A ShortGuid instance.</returns>
    public static ShortGuid FromString(string shortGuid)
    {
        if (string.IsNullOrWhiteSpace(shortGuid))
            throw new ArgumentException("Short GUID string cannot be null or empty.", nameof(shortGuid));

        return new ShortGuid(shortGuid);
    }

    /// <summary>
    /// Encodes the GUID as a 22-character Base64 string.
    /// </summary>
    /// <returns>The encoded string.</returns>
    public override string ToString()
    {
        return Encode(Value);
    }

    /// <summary>
    /// Implicitly converts a ShortGuid to a regular GUID.
    /// </summary>
    /// <param name="shortGuid">The ShortGuid to convert.</param>
    public static implicit operator Guid(ShortGuid shortGuid)
    {
        return shortGuid.Value;
    }

    /// <summary>
    /// Implicitly converts a regular GUID to a ShortGuid.
    /// </summary>
    /// <param name="guid">The GUID to convert.</param>
    public static implicit operator ShortGuid(Guid guid)
    {
        return new ShortGuid(guid);
    }

    /// <summary>
    /// Encodes a GUID as a 22-character Base64 string.
    /// </summary>
    /// <param name="guid">The GUID to encode.</param>
    /// <returns>The encoded string.</returns>
    private static string Encode(Guid guid)
    {
        return Convert.ToBase64String(guid.ToByteArray())
            .Replace("/", "_")
            .Replace("+", "-")
            .Substring(0, 22);
    }

    /// <summary>
    /// Decodes a 22-character Base64 string back into a GUID.
    /// </summary>
    /// <param name="shortGuid">The short GUID string to decode.</param>
    /// <returns>The original GUID.</returns>
    private static Guid Decode(string shortGuid)
    {
        try
        {
            string base64 = shortGuid.Replace("_", "/").Replace("-", "+") + "==";
            byte[] bytes = Convert.FromBase64String(base64);
            return new Guid(bytes);
        }
        catch (FormatException)
        {
            throw new ArgumentException("Invalid short GUID format.", nameof(shortGuid));
        }
    }

    /// <summary>
    /// Checks if this ShortGuid equals another object.
    /// </summary>
    /// <param name="obj">The object to compare with.</param>
    /// <returns>True if equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        if (obj is ShortGuid other)
        {
            return Value.Equals(other.Value);
        }

        return false;
    }

    /// <summary>
    /// Gets the hash code for this ShortGuid.
    /// </summary>
    /// <returns>The hash code.</returns>
    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}

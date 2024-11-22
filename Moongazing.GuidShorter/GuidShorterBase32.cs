using System;

namespace Moongazing.GuidShorter;

/// <summary>
/// Provides functionality for converting between standard GUIDs and their shorter Base32-encoded representations.
/// </summary>
public class ShortGuidBase32
{
    /// <summary>
    /// The underlying GUID value.
    /// </summary>
    public Guid Value { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortGuidBase32"/> class with a GUID value.
    /// </summary>
    /// <param name="value">The GUID to initialize with.</param>
    private ShortGuidBase32(Guid value)
    {
        Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ShortGuidBase32"/> class with a Base32-encoded string.
    /// </summary>
    /// <param name="value">The Base32-encoded string representing a GUID.</param>
    private ShortGuidBase32(string value)
    {
        Value = Decode(value);
    }

    /// <summary>
    /// Creates a new <see cref="ShortGuidBase32"/> from a GUID.
    /// </summary>
    /// <param name="guid">The GUID to convert to a ShortGuidBase32.</param>
    /// <returns>An instance of <see cref="ShortGuidBase32"/> representing the provided GUID.</returns>
    public static ShortGuidBase32 FromGuid(Guid guid)
    {
        return new ShortGuidBase32(guid);
    }

    /// <summary>
    /// Creates a new <see cref="ShortGuidBase32"/> from a Base32-encoded string.
    /// </summary>
    /// <param name="shortGuid">The Base32-encoded string to convert to a ShortGuidBase32.</param>
    /// <returns>An instance of <see cref="ShortGuidBase32"/> representing the provided string.</returns>
    public static ShortGuidBase32 FromString(string shortGuid)
    {
        return new ShortGuidBase32(shortGuid);
    }

    /// <summary>
    /// Returns the Base32-encoded string representation of the GUID.
    /// </summary>
    /// <returns>A Base32-encoded string representing the GUID.</returns>
    public override string ToString()
    {
        return Encode(Value);
    }

    /// <summary>
    /// Implicitly converts a <see cref="ShortGuidBase32"/> to a <see cref="Guid"/>.
    /// </summary>
    /// <param name="shortGuid">The ShortGuidBase32 to convert.</param>
    public static implicit operator Guid(ShortGuidBase32 shortGuid)
    {
        return shortGuid.Value;
    }

    /// <summary>
    /// Implicitly converts a <see cref="Guid"/> to a <see cref="ShortGuidBase32"/>.
    /// </summary>
    /// <param name="guid">The GUID to convert.</param>
    public static implicit operator ShortGuidBase32(Guid guid)
    {
        return new ShortGuidBase32(guid);
    }

    /// <summary>
    /// Encodes a GUID into a Base32-encoded string.
    /// </summary>
    /// <param name="guid">The GUID to encode.</param>
    /// <returns>A Base32-encoded string representation of the GUID, without padding.</returns>
    private static string Encode(Guid guid)
    {
        return Base32Helper.Encode(guid.ToByteArray()).TrimEnd('=');
    }

    /// <summary>
    /// Decodes a Base32-encoded string back into a GUID.
    /// </summary>
    /// <param name="shortGuid">The Base32-encoded string to decode.</param>
    /// <returns>The original GUID represented by the string.</returns>
    private static Guid Decode(string shortGuid)
    {
        string paddedBase32 = shortGuid.PadRight(26, '='); // Ensures the string length matches Base32 decoding requirements.
        byte[] bytes = Base32Helper.Decode(paddedBase32);
        return new Guid(bytes);
    }
}

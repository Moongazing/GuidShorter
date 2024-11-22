using System;
using System.Linq;
using System.Text;

namespace Moongazing.GuidShorter;

public static class Base32Helper
{
    private const string Base32Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ234567";

    public static string Encode(byte[] data)
    {
        StringBuilder result = new();
        int byteIndex = 0, bitBuffer = 0, bitsLeft = 0;

        while (byteIndex < data.Length)
        {
            bitBuffer <<= 8;
            bitBuffer |= data[byteIndex++];
            bitsLeft += 8;

            while (bitsLeft >= 5)
            {
                result.Append(Base32Chars[bitBuffer >> bitsLeft - 5 & 0x1F]);
                bitsLeft -= 5;
            }
        }

        if (bitsLeft > 0)
        {
            result.Append(Base32Chars[bitBuffer << 5 - bitsLeft & 0x1F]);
        }

        return result.ToString();
    }

    public static byte[] Decode(string base32)
    {
        int bitBuffer = 0, bitsLeft = 0, outputIndex = 0;
        byte[] output = new byte[base32.Length * 5 / 8];

        foreach (char c in base32.ToUpperInvariant())
        {
            if (c == '=')
                break;

            int charValue = Base32Chars.IndexOf(c);
            if (charValue < 0)
                throw new ArgumentException("Invalid Base32 character", nameof(base32));

            bitBuffer <<= 5;
            bitBuffer |= charValue;
            bitsLeft += 5;

            if (bitsLeft >= 8)
            {
                output[outputIndex++] = (byte)(bitBuffer >> bitsLeft - 8 & 0xFF);
                bitsLeft -= 8;
            }
        }

        return output.Take(outputIndex).ToArray();
    }
}


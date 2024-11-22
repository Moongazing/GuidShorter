# ShortGuid Helper Class

This project provides a helper class to represent GUIDs in a shorter, 22-character Base64 encoded format. It is useful when a compact representation of a GUID is needed, such as for URLs or database keys.

## Features

- Converts a regular GUID to a 22-character short GUID.
- Converts a short GUID back to a regular GUID.
- Provides implicit conversions between `Guid` and `ShortGuid`.
- Validates input to prevent errors during decoding.
- Overrides equality and hash code methods for proper comparison.

## How to Use

### 1. Creating a ShortGuid from a GUID

```csharp
Guid originalGuid = Guid.NewGuid();
ShortGuid shortGuid = ShortGuid.FromGuid(originalGuid);

Console.WriteLine($"Original GUID: {originalGuid}");
Console.WriteLine($"Short GUID: {shortGuid}");


ShortGuid shortGuid = Guid.NewGuid(); // Implicit conversion from Guid to ShortGuid
Guid guid = shortGuid;               // Implicit conversion from ShortGuid to Guid


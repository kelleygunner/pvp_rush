# Shared Logic
Shared Logic is the common foundation for both the client and the server. Its main purpose is to prevent code duplication while ensuring that both sides strictly follow the same contracts.

## Integration
I integrated Shared Logic in the following way:
- **Server Project:** Files are linked directly into the Server Assembly.
- **Unity Project:** Integrated as a Package via an Assembly Definition. I enabled the **"No Engine References"** toggle to ensure no Unity-specific libraries leak into the Shared Logic.

This approach keeps the client and server codebases fully separated but perfectly aligned.

## The Core Idea
Shared Logic contains neither Unity-specific code nor Server-specific logic. Instead, it holds the "contracts" between them. It defines the interconnection behavior: how packets are structured, how they are constructed, connection types, ports, etc. By sharing the same version of Shared Logic, I can guarantee that the Server and Client are always in sync and "speak the same language."

## How it works

### Configuration
Shared Logic provides centralized config values to keep both sides aligned:

`public const byte SharedLogicVersion = 1;`<br>
`public const int Port = 7777;`<br>
`public const ushort HeaderSize = 4;`<br>
`public const byte VersionByte = 0;`<br>
`public const byte MessageTypeByte = 1;`<br>
`public const int PayloadSizeStartByte = 2;`<br>
`public const int PayloadSizeLength = 2;`<br>

### Packet Sending and Receiving
In Shared Logic, I implemented the core rules for packet creation—defining exactly how many bytes are used for the header and what each byte represents. This creates a consistent byte-interpretation system for both sides.

Since all messages are defined within Shared Logic, I can ensure they are always interpreted correctly and converted into the proper message-specific structures. Shared Logic doesn't need to know *how* a message will be processed; its job is to validate the message, dispatch the correct type, deserialize it into a known structure, and hand it over to the endpoint for processing.

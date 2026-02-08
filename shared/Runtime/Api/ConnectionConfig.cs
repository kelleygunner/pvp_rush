namespace PvpRush.Shared.Api
{
    public static class ConnectionConfig
    {
        public const byte SharedLogicVersion = 1;
        public const int Port = 7777;
        public const ushort HeaderSize = 4;
        public const byte VersionByte = 0;
        public const byte MessageTypeByte = 1;
        public const int PayloadSizeStartByte = 2;
        public const int PayloadSizeLength = 2;
    }
}

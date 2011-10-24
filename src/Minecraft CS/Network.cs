using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Minecraft_CS
{
    /*public enum packetTypes: byte
    {
        KeepAlive = 0x00,
        LoginRequest = 0x01,
        Handshake = 0x02,
        ChatMessage = 0x03,
        TimeUpdate = 0x04,
        EntityEquipment = 0x05,
        SpawnPosition = 0x06,
        UseEntity = 0x07,
        UpdateHealth  = 0x08,
    Respawn = 0x09,
    Player = 0x0A,
    PlayerPosition = 0x0B,
    PlayerLook = 0x0C,
    PlyerPositionAndLook = 0x0D,
    PlayerDigging = 0x0E,
    PlayerBlockPlacement = 0x0F,
    HoldingChange = 0x10,
   UseBed = 0x11,
    Animation = 0x12,
    EntityAction = 0x13 = "EntityAction",
    0x14 = "NamedEntitySpawn",
    0x15 = "PickupSpawn",
    0x16 = "CollectItem",
    0x17 = "AddObject/Vehicle",
    0x18 = "MobSpawn",
    0x19 = "PaintingEntity",
    0x1A = "ExperienceOrb",
    0x1B = "StanceUpdate",
    0x1C = "EntityVelocity",
    0x1D = "DestroyEntity",
    0x1E = "Entity",
    0x1F = "EntityRelativeMove",
    0x20 = "EntityLook",
    0x21 = "EntityLookandRelativeMove",
    0x22 = "EntityTeleport",
    0x26 = "EntityStatus",
    0x27 = "AttachEntity",
    0x28 = "EntityMetadata",
    0x29 = "EntityEffect",
    0x2A = "RemoveEntityEffect",
    0x2B = "Experience",
    0x32 = "PreChunk",
    0x33 = "MapChunk",
    0x34 = "MultiBlockChange",
    0x35 = "BlockChange",
    0x36 = "BlockAction",
    0x3C = "Explosion",
    0x3D = "SoundEffect",
    0x46 = "New/InvalidState",
    0x47 = "Thunderbolt",
    0x64 = "OpenWindow",
    0x65 = "CloseWindow",
    0x66 = "WindowClick",
    0x67 = "SetSlot",
    0x68 = "WindowItems",
    0x69 = "UpdateProgressBar",
    0x6A = "Transaction",
    0x6B = "CreativeInventoryAction",
    0x6C = "EnchantItem",
    0x83 = "ItemData",
    0xC8 = "IncrementStatistics",
    0xC9 = "PlayerListItem",
    0xFE = "ServerListPing",
    0xFF = "Disconnect/Kick",
}*/

    public static class Network
    {
        #region Properties
        private static Boolean _disconnecting = false;
        private static Boolean _loggedin = false;
        private static StreamReader reader;
        private static StreamWriter writer;
        private static TcpClient _client;
        public static Boolean Disconnecting
        {
            get { return _disconnecting; }
        }
        public static Boolean LoggedIn
        {
            get { return _loggedin; }
        }
        public static TcpClient Client
        {
            get { return _client; }
        }
        #endregion

        #region Events
        public struct receivePacketArgs
        {
            private Byte _packetid;
            private Object[] _fieldData;
            
            public Byte PacketID
            {
                get { return _packetid;}
            }
            public Object[] FieldData
            {
                get { return _fieldData; }
            }

            public receivePacketArgs(Byte packetID, Object[] fieldData)
            {
                _packetid = packetID;
                _fieldData = fieldData;
            }
        }
        public delegate void receivePacketHandler(Object sender, receivePacketArgs e);
        public static event receivePacketHandler receivePacket; 
        #endregion

        #region Functions
        public static void Initialize()
        {
            //Hook our own method to the event
            receivePacket += new receivePacketHandler(parsePacket);

            //Setup the client
            _client = new TcpClient();
        }
        public static void parsePacket(Object sender, receivePacketArgs e)
        {
        }
        #endregion
    }
}

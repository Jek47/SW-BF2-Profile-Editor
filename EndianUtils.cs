using System;
using System.IO;

public static class EndianUtils
{
    public static uint ReadUInt32LE(BinaryReader br)
    {
        return br.ReadUInt32();
    }
    public static void WriteUInt32LE(BinaryWriter bw, uint value)
    {
        bw.Write(value); 
    }
    public static ushort ReadUInt16LE(BinaryReader br) => br.ReadUInt16();
    public static void WriteUInt16LE(BinaryWriter bw, ushort value) => bw.Write(value);
}
using System;
using System.Collections;
using System.Text;

namespace Abba;

static class BitArrayHelper
{
    public static string ToBitString(this BitArray ba)
    {
        StringBuilder sb = new StringBuilder(ba.Length);
        for (int i = 0; i < ba.Length; i++)
            sb.Append(ba[i] ? '1' : '0');
        return sb.ToString();
    }

    public static string ToHexString(this BitArray bits)
    {
        StringBuilder sb = new StringBuilder(bits.Length / 4);

        for (int i = 0; i < bits.Length; i += 4)
        {
            int v = (bits[i] ? 8 : 0) |
                    (bits[i + 1] ? 4 : 0) |
                    (bits[i + 2] ? 2 : 0) |
                    (bits[i + 3] ? 1 : 0);

            sb.Append(v.ToString("x1")); // Or "X1"
        }
        return sb.ToString();
    }

    public static BitArray Prepend(this BitArray current, BitArray before)
    {
        var bools = new bool[current.Count + before.Count];
        before.CopyTo(bools, 0);
        current.CopyTo(bools, before.Count);
        return new BitArray(bools);
    }

    public static BitArray Append(this BitArray current, BitArray after)
    {
        var bools = new bool[current.Count + after.Count];
        current.CopyTo(bools, 0);
        after.CopyTo(bools, current.Count);
        return new BitArray(bools);
    }

    public static BitArray Append(this BitArray current, bool after)
    {
        var bools = new bool[current.Count + 1];
        current.CopyTo(bools, 0);
        bools[current.Count] = after;
        return new BitArray(bools);
    }
}
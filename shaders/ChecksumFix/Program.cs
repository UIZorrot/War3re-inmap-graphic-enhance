using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChecksumFix
{
    class Program
    {
        // For testing / disassembling hand-edited shaders

        static void Main(string[] args)
        {
            byte[] shader = File.ReadAllBytes(args[0]);
            int[] checksum = DXBCChecksum.DXBCChecksum.CalculateDXBCChecksum(shader);
            byte[] bChecksum = new byte[16];
            Buffer.BlockCopy(checksum, 0, bChecksum, 0, 16);
            bChecksum.CopyTo(shader, 4);
            File.WriteAllBytes(args[0], shader);
        }
    }
}

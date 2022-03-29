using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector
{
    class Util
    {

        public const int BUFFER_SIZE_56KB = 56 * 1024;
        public const int BUFFER_SIZE_2MB = 2 * 1024 * 1024;

        public static Func<string, byte[]> StringToBytes = str => System.Text.Encoding.UTF8.GetBytes(str);
        public static Func<string, ArraySegment<byte>> StringToByteSegment = str => new ArraySegment<byte>(StringToBytes(str));
        public static Func<byte[], string> BytesToString = bytes => System.Text.Encoding.UTF8.GetString(bytes);
        public static Func<ArraySegment<byte>, string> SegmentToString = arraySegment => System.Text.Encoding.UTF8.GetString(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
    }
}
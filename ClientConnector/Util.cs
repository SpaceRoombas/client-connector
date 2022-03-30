using System;
using System.Text;
using ClientConnector.messages;

namespace ClientConnector
{
    class Util
    {

        public const int BUFFER_SIZE_56KB = 56 * 1024;
        public const int BUFFER_SIZE_2MB = 2 * 1024 * 1024;

        public static Func<string, byte[]> StringToBytes = str => Encoding.UTF8.GetBytes(str);
        public static Func<string, ArraySegment<byte>> StringToByteSegment = str => new ArraySegment<byte>(StringToBytes(str));
        public static Func<byte[], string> BytesToString = bytes => Encoding.UTF8.GetString(bytes);
        public static Func<ArraySegment<byte>, string> SegmentToString = arraySegment => Encoding.UTF8.GetString(arraySegment.Array, arraySegment.Offset, arraySegment.Count);

        public static Func<string, CarrierPigeon<InternalErrorMessage>> generateInternalErrorMessage = (message) => new CarrierPigeon<InternalErrorMessage>(new InternalErrorMessage(message), "error", "internal_error");
    }
}
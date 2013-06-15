using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MCM.Utils
{
    public class StreamHelper
    {
        public static byte[] StreamToByteArray(Stream input)
        {
            byte[] total_stream = new byte[0];byte[] stream_array = new byte[0];
            // Setup whatever read size you want (small here for testing)
            byte[] buffer = new byte[32];// * 1024];
            int read = 0;

            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                stream_array = new byte[total_stream.Length + read];
                total_stream.CopyTo(stream_array, 0);
                Array.Copy(buffer, 0, stream_array, total_stream.Length, read);
                total_stream = stream_array;
            }
            input.Close();
            return total_stream;
        }
    }
}

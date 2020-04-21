using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using zlib;

namespace ConsoleApp1
{
    class Program
    {
        public static void CopyStream(System.IO.Stream input, System.IO.Stream output)
        {
            byte[] buffer = new byte[2000];
            int len;
            while ((len = input.Read(buffer, 0, 2000)) > 0)
            {
                output.Write(buffer, 0, len);
            }
            output.Flush();
        }
        private static Stream deCompressStream(Stream sourceStream)
        {
            MemoryStream outStream = new MemoryStream();
            ZOutputStream outZStream = new ZOutputStream(outStream);
            CopyStream(sourceStream, outZStream);
            outZStream.finish();
            return outStream;
        }
        private static byte[] deCompressBytes(byte[] sourceByte)
        {
            MemoryStream inputStream = new MemoryStream(sourceByte);
            Stream outputStream = deCompressStream(inputStream);
            byte[] outputBytes = new byte[outputStream.Length];
            outputStream.Position = 0;
            outputStream.Read(outputBytes, 0, outputBytes.Length);
            outputStream.Close();
            inputStream.Close();
            return outputBytes;
        }
        static void Main(string[] args)
        {
            
            FileStream fs = new FileStream("D:\\bloomextract.bls", FileMode.Open, FileAccess.Read);
            try
            {
                ZInputStream zIn = new ZInputStream(fs);
                int curPosition = 0;
                Byte[] data = new Byte[4096];
                Byte[] bytes = new Byte[0];
                while (true)
                {
                    int size = zIn.Read(data, 0, data.Length);
                    if (size > 0)
                    {
                        curPosition += size;
                        Byte[] tempBytes = new Byte[bytes.LongLength + data.Length];

                        bytes.CopyTo(tempBytes, 0);
                        data.CopyTo(tempBytes, bytes.LongLength);

                        bytes = tempBytes;

                    }
                    else
                    {
                        break;
                    }
                }
                string str = System.Text.Encoding.Default.GetString(bytes);
                Console.WriteLine(str);
                Console.WriteLine("ddddddddddd");
                bytes = deCompressBytes(bytes);
                str = System.Text.Encoding.Default.GetString(bytes);
                Console.WriteLine(str);
                fs.Close();
                fs.Dispose();

                //string strDecompress = System.Text.Encoding.UTF8.GetString(bytesDecompress);
                //Console.WriteLine(strDecompress);
                //BLS p = new BLS(buffur);
                //byte[] re = new ShaderBlock(buffur).Serialize();
 
                using (FileStream fs1 = new FileStream("D:\\bloom.bls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                   
                    using (BinaryWriter bw = new BinaryWriter(fs1))
                    {
                        bw.Write(bytes);
                        bw.Close();
                    }
                }
            }
            catch
            { 
            }
            finally
            {
                if (fs != null)

                {

                    //关闭资源 
                    fs.Close();
                }
            }


            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}

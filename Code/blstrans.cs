using System;
using System.Collections.Generic;
using System.IO;
using Warcraft.BLS;

namespace blstrans
{
    class Program
    {
        static void Main(string[] args)
        {
            
            FileStream fs = new FileStream("D:\\bloomextract.bls", FileMode.Open, FileAccess.Read);
            
            try
            {
                string st = "";
                byte[] buffur = new byte[fs.Length];
                fs.Read(buffur, 0, (int)fs.Length);
                foreach (byte b in buffur)
                {
                    st += b.ToString();
                }
                Console.WriteLine(st);
                //BLS p = new BLS(buffur);
                byte[] re = new ShaderBlock(buffur).Serialize();
 
                using (FileStream fs1 = new FileStream("D:\\bloom.bls", FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    byte[] buffur1 = re;
                    using (BinaryWriter bw = new BinaryWriter(fs1))
                    {
                        bw.Write(buffur1);
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

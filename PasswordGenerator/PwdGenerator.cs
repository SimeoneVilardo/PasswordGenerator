using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace PasswordGenerator
{
    public class PwdGenerator
    {
        private byte[] combineArrays(params byte[][] arrays)
        {
            byte[] numArray = new byte[((IEnumerable<byte[]>)arrays).Sum<byte[]>((byte[] a) => (int)a.Length)];
            int length = 0;
            byte[][] numArray1 = arrays;
            for (int i = 0; i < (int)numArray1.Length; i++)
            {
                byte[] numArray2 = numArray1[i];
                Buffer.BlockCopy(numArray2, 0, numArray, length, (int)numArray2.Length);
                length += (int)numArray2.Length;
            }
            return numArray;
        }

        private string formatPassword(string password)
        {
            char[] charArray = password.ToCharArray();
            bool flag = false;
            for (int i = 0; i < (int)charArray.Length; i++)
            {
                if (char.IsLetter(charArray[i]))
                {
                    if (flag)
                    {
                        charArray[i] = char.ToLower(charArray[i]);
                    }
                    flag = !flag;
                }
            }
            return string.Concat("[#", new string(charArray), "#]");
        }

        private byte[] generateHash(string fileName, bool utf8 = true)
        {
            byte[] numArray;
            string str = string.Concat(Path.GetFileName(fileName), "\r\n");
            byte[] bytes = Encoding.Default.GetBytes(str);
            byte[] numArray1 = File.ReadAllBytes(fileName);
            numArray = (!utf8 ? bytes.Concat<byte>(numArray1).ToArray<byte>() : numArray1.Take<byte>(3).Concat<byte>(bytes).Concat<byte>(numArray1.Skip<byte>(3)).ToArray<byte>());
            return (new SHA1CryptoServiceProvider()).ComputeHash(numArray);
        }

        public string GeneratePassword(string FileName, bool utf8 = true)
        {
            return this.formatPassword(this.generateStrHash(FileName, utf8));
        }

        private string generateStrHash(string fileName, bool utf8 = true)
        {
            byte[] numArray = this.generateHash(fileName, utf8);
            return BitConverter.ToString(numArray).Replace("-", "");
        }
    }
}
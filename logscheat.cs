using System.Security.Cryptography;
using System.Text;

namespace Nagomi.utils;

public class logscheat
{
    
    private static readonly int[] Lookup = GenerateLookup();

    public static string Encrypt(string text, string lineNum)
    {
        string testStr = (text + '|' + lineNum);
        byte[] testBytes = Encoding.UTF8.GetBytes(testStr);
        byte[] hashBytes = ComputeSha256Hash(testBytes);
        return U49152(hashBytes);
    }

    private static byte[] ComputeSha256Hash(byte[] input)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(input);
        }
    }

    private static string U49152(byte[] byteStr)
    {
        char[] res = new char[16];
        for (int i = 0; i < 8; i++)
        {
            int num = Lookup[byteStr[i]];
            res[2 * i] = (char)(num % 128);
            res[2 * i + 1] = (char)((num >> 16) % 128);
        }
        return new string(res);
    }

    private static int[] GenerateLookup()
    {
        int[] numArray = new int[256];
        for (int i = 0; i < 256; i++)
        {
            string hexString = i.ToString("X2");
            numArray[i] = hexString[0] + (hexString[1] << 16);
        }
        return numArray;
    }
}
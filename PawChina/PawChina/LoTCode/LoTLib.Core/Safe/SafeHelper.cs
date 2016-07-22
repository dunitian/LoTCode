using System;

public static partial class SafeHelper
{
    /// <summary>
    /// SHA-1加密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string GetShaOne(string input)
    {
        var shaOne = System.Security.Cryptography.SHA1.Create();
        byte[] shaOneBuffer = shaOne.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input));
        shaOne.Clear();
        return BitConverter.ToString(shaOneBuffer).Replace("-", "").ToLower();//另一种简单的方法
    }
}

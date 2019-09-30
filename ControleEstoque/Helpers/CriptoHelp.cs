using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ControleEstoque
{
    public static class CriptoHelp
    {
        public static string HashMD5(string val)
        {
            var bytes = Encoding.ASCII.GetBytes(val); //Criptografa a String
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(bytes); //Obtem a criptografia
            var ret = string.Empty;
            for(int i =0; i < hash.Length; i++)
            {
                ret = ret + hash[i].ToString("x2");
            }
            return ret;
        }
    }
}
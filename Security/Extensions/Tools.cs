using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Security.Extensions
{
    public static class Tools
    {
        /// <summary>
        /// Возвращает хэш SHA1 массива байтов
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static byte[] GetSHA1HashBytes(this byte[] bytes)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(bytes);
        }

        /// <summary>
        /// Возвращает хэш SHA1 введенной строки 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] GetSHA1HashBytes(this string value)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            return sha1.ComputeHash(value.GetBytes());
        }

        /// <summary>
        /// Преобразует строку в массив байтов
        /// </summary>
        /// <param name="value">Исходная строка</param>
        /// <returns></returns>
        public static byte[] GetBytes(this string value)
        {
            return Encoding.Unicode.GetBytes(value);
        }

    }
}

using System;
using System.Linq;
using UrlShortening.Service.IService;

namespace UrlShortening.Service.Service
{
    public class ConversionService : IConversionService
    {
        /// <summary>
        /// Converts long value to Base62 string
        /// </summary>
        /// <param name="value">Long value</param>
        /// <returns>Base62 string</returns>
        public string LongToBase62String(long value)
        {
            long x = 0;
            var y = Math.DivRem(value, 62, out x);
            if (y > 0)
                return LongToBase62String(y) + LongToChar(x);
            return LongToChar(x).ToString();
        }

        /// <summary>
        /// Converts long value to Ascii character
        /// </summary>
        /// <param name="value">Long value</param>
        /// <returns>Ascii character</returns>
        private static char LongToChar(long value)
        {
            if (value <= 9) 
                return value.ToString()[0];

            var asciiIndex = (((int) value - 10) + 65);

            if (asciiIndex <= 90) 
                return (char) asciiIndex;

            asciiIndex += 6;
            return (char) asciiIndex;
        }

        /// <summary>
        /// Converts Base62 string to long value
        /// </summary>
        /// <param name="input">Base62 string</param>
        /// <returns>long value</returns>
        public long Base62StringToLong(string input)
        {
            const string base62Characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

            var srcBase = base62Characters.Length;
            var inputReversed = ReverseString(input);

            return
                inputReversed.Select(t => base62Characters.IndexOf(t))
                    .Select((charIndex, i) => charIndex*(long) Math.Pow(srcBase, i))
                    .Sum();
        }

        /// <summary>
        /// Reverses a string value
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        private static string ReverseString(string s)
        {
            var arr = s.ToCharArray();
            Array.Reverse(arr);
            return new string(arr);
        } 
    }
}
using System;

namespace ClassLibrary.Helpers
{
    public class OleDb
    {
        /// <summary>
        /// Decode Ole Db Image
        /// </summary>
        /// <param name="src"></param>
        /// <returns></returns>
        public static string FixOleDBBase64(string src)
        {
            byte[] imageByte = Convert.FromBase64String(src);
            string res = Convert.ToBase64String(imageByte, 78, imageByte.Length - 78);
            return res;
        }
    }
}

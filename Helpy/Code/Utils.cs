using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using Helpy.Pages;

namespace Helpy.Code
{
    public static class Utils
    {
        public static void RunJS(Page page, string js, string key)
        {
            page.Page.ClientScript.RegisterStartupScript(page.GetType(), key, js, true);
        }

        public static void AlertJs(Page page, string message, string key)
        {
            RunJS(page, message, key);
        }

        public static class Crypto
        {
            public static string CalculateMD5Hash(string input)
            {
                MD5 mmd5 = MD5.Create();

                byte[] minputBytes = Encoding.ASCII.GetBytes(input);

                byte[] mhash = mmd5.ComputeHash(minputBytes);

                StringBuilder msb = new StringBuilder();

                foreach (byte mt in mhash)
                    msb.Append(mt.ToString("X2"));

                return msb.ToString();

            }

        }
    }
}
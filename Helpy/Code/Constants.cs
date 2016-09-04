using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Helpy.Code
{
    public static class Constants
    {
        public const string MYSQL_CONNECTION_STRING = "Server=localhost;Database=helpy;Uid=helpy;Pwd=helpy;";
        public const string PROJECT_NAME = "Helpy";
        public const string CURRENCY = "$";
        public const string CREATOR = "Created";

        public const int ITEMS_PER_PAGE = 20;

        public static CultureInfo DefaultCulture = new CultureInfo("en-US");
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpy.Code;
using Helpy.Model;

namespace Helpy.Classes
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Pass { get; set; }

        public string FullName { get; set; }

        public string Mail { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public string Cnp { get; set; }

        public bool Deleted { get; set; }

        public static int Add(User user)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.AddUser(user);
        }

        public static User GetByName(string name)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.GetUserByName(name);
        }

        public static User GetById(int id)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.GetUserById(id);
        }
    }
}
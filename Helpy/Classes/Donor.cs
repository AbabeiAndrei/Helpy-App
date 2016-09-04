using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpy.Code;
using Helpy.Model;

namespace Helpy.Classes
{
    public class Donor
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public int ChildId { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Message { get; set; }

        public decimal Amount { get; set; }

        public string CardDetails { get; set; }

        public DateTime Date { get; set; }

        public string ChildName { get; set; }

        public Donor()
        {
        }

        public Donor(string name, decimal amount)
        {
            Name = name;
            Amount = amount;
        }

        public static void Add(Donor donor)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                mctx.AddDonor(donor);
        }

        public static IEnumerable<Donor> TopDonor(int limit, DateTime? date = null)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.GetDonors(limit, date)
                           .OrderByDescending(d => d.Amount);
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Helpy.Code;
using Helpy.Model;

namespace Helpy.Classes
{
    public class Child
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Photo { get; set; }
        public string Address { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ShortStory { get; set; }
        public string Story { get; set; }
        public decimal Amount { get; set; }
        public decimal Total { get; set; }
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public bool Top { get; set; }
        public bool Deleted { get; set; }
        public int UserId { get; set; }
        public float Procent => Math.Min((float) (Total / Amount * 100), 100);

        public int? Age => BirthDate.HasValue ? DateTime.Now.Year - BirthDate.Value.Year : (int?)null;
        public TimeSpan Remained => To - DateTime.Now;

        public static IEnumerable<Child> GetAll(int page = 0, int limit = 10000)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.GetChilds(page * limit, limit)
                           .Where(c => !c.Deleted);
        }

        public static void Add(Child child)
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                mctx.AddChild(child);//
        }
    }
}
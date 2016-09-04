using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpy.Classes;
using Helpy.Code;
using Helpy.Model;

namespace Helpy
{
    public partial class index : System.Web.UI.Page
    {
        protected bool _fromDonation;

        protected void Page_Load(object sender, EventArgs e)
        {
            _fromDonation = Request.QueryString.Get("donate") == "1" ||
                            Request.QueryString.Get("add_cause") == "1";
        }

        protected IEnumerable<Donor> GetTopDonor(int limit)
        {
            return Donor.TopDonor(limit, DateTime.Now);
        }

        protected Donor GetFistDonor()
        {
            return GetTopDonor(1).FirstOrDefault();
        }
    }
}
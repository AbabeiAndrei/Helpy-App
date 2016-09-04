using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpy.Classes;
using Helpy.Code;

namespace Helpy.Pages
{
    public partial class cause : System.Web.UI.Page
    {
        protected bool _needLogin;

        protected void Page_Load(object sender, EventArgs e)
        {
            _needLogin = string.IsNullOrEmpty(Request.Cookies.Get("userId")?.Value);
        }

        protected void btnOk_OnClick(object sender, EventArgs e)
        {
            var muidc = Request.Cookies.Get("userId");

            if (string.IsNullOrEmpty(muidc?.Value))
            {
                Response.Redirect("cause.aspx?need_login=1");
                return;
            }

            string mname = txtName.Text;
            string maddress = txtAddress.Text;
            string mshortStory = txtShortStory.Text;

            DateTime mbrith;
            DateTime mtill = DateTime.MaxValue;

            if (!DateTime.TryParse(txtBirthDate.Text.Split(' ')[0], Constants.DefaultCulture, DateTimeStyles.AssumeUniversal,  out mbrith))
            {
                Utils.AlertJs(this, "Incorect birthdate format", "Validate");
                return;
            }

            if (!string.IsNullOrEmpty(txtUntil.Text) && !DateTime.TryParse(txtUntil.Text.Split(' ')[0], Constants.DefaultCulture, DateTimeStyles.AssumeUniversal, out mtill))
            {
                Utils.AlertJs(this, "Incorect until format", "Validate");
                return;
            }

            decimal mneed;

            if (!decimal.TryParse(txtNeed.Text, out mneed))
            {
                Utils.AlertJs(this, "Incorect amount format", "Validate");
                return;
            }

            if(string.IsNullOrEmpty(mname))
            {
                Utils.AlertJs(this, "Name cannot be empty", "Validate");
                return;
            }

            if (string.IsNullOrEmpty(maddress))
            {
                Utils.AlertJs(this, "Address cannot be empty", "Validate");
                return;
            }

            if (string.IsNullOrEmpty(mshortStory))
            {
                Utils.AlertJs(this, "Short story cannot be empty", "Validate");
                return;
            }


            Child.Add(new Child
            {
                FullName = mname,
                Address = maddress,
                Amount = mneed,
                BirthDate = mbrith == DateTime.MinValue ? (DateTime?)null : mbrith,
                To = mtill,
                ShortStory = mshortStory,
                Story = txtMessage.Value,
                UserId = Convert.ToInt32(muidc.Value)
            });
            
            Response.Redirect("~/home.aspx?add_cause=1");
        }
    }
}
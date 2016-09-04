using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpy.Classes;
using Helpy.Code;
using Helpy.Model;

namespace Helpy.Pages
{
    public partial class donate : System.Web.UI.Page
    {
        protected int _childId;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void OnClickDonate(object sender, EventArgs e)
        {
            int mchildId;
            if (ViewState["child_id"] == null || !int.TryParse(ViewState["child_id"].ToString(), out mchildId))
            {
                Utils.RunJS(this, "activaTab(1);alert(\"No child selected\");", "Incomplete");
                return;
            }
            
            decimal mamount;


            if (!decimal.TryParse(txtAmount.Text, out mamount) || mamount <= 0)
            {
                Utils.RunJS(this, "activaTab(3);alert(\"Incorect amount\");", "Incomplete");
                return;
            }

            string mname = txtName.Text;
            string mphone = txtPhone.Text;
            string maddress = txtAddress.Text;

            if (string.IsNullOrEmpty(mname) || string.IsNullOrEmpty(mphone) || string.IsNullOrEmpty(maddress))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Incomplete", "activaTab(2);alert(\"All fields must be completed\");", true);
                return;
            }

            string mcard = txtCard.Text;
            string mcholder = txtCardHolder.Text;

            DateTime mexpire;

            if(!DateTime.TryParse(txtCardExpire.Text.Split(' ')[0], Constants.DefaultCulture, DateTimeStyles.AssumeUniversal, out mexpire))
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "Incomplete", "activaTab(3);alert(\"Incorect date time format\");", true);
                return;
            }

            string mmsg = txtStory.Value;

            if (string.IsNullOrEmpty(mcard) || string.IsNullOrEmpty(mcholder))
            {
                Utils.RunJS(this, "activaTab(3);alert(\"All fileds must be compelted\");", "Incomplete");
            }

            Donor.Add(new Donor
            {
                ChildId = mchildId,
                UserId = null, //todo
                Name = mname,
                Address = maddress,
                CardDetails = mcard + "|" + mcholder + "|" + mexpire,
                Date = DateTime.Now.Date,
                Phone = mphone,
                Message = mmsg,
                Amount = mamount
            });

            Response.Redirect("~/home.aspx?donate=1");
        }

        public IEnumerable<Child> GetChilds(int page = 0, int limit = 10000)
        {
            return Child.GetAll();
        }

        protected int TotalNumberOfKids()
        {
            using (var mctx = new Connector(Constants.MYSQL_CONNECTION_STRING))
                return mctx.CountChilds();
        }

        protected void OnNextClick(object sender, EventArgs e)
        {
            var mbtn = sender as Button;

            if(mbtn == null)
                return;
            
            if(!int.TryParse(mbtn.CommandArgument, out _childId))
                return;

            ViewState.Add("child_id", mbtn.CommandArgument);

            Utils.RunJS(this, "actvateTab(2);", "Page");
        }
    }
}
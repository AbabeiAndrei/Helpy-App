using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Helpy.Classes;
using Helpy.Code;

namespace Helpy
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected string _name;
        protected string _fromLoginError;
        protected string _fromRegError;

        protected void Page_Load(object sender, EventArgs e)
        {
            

            var mc = Request.Cookies.Get("userId");
            if(mc == null)
                return;

            int muid;

            if(!int.TryParse(mc.Value, out muid))
                return;

            var mu = User.GetById(muid);

            if (mu == null)
            {
                Request.Cookies.Remove("userId");
                return;
            }

            _name = mu.FullName;
        }

        protected void btnLogin_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtName.Text))
            {
                SetLoginError("Name canot be empty");
                return;
            }

            if (string.IsNullOrEmpty(txtPass.Text))
            {
                SetLoginError("Pass canot be empty");
                return;
            }

            var mu = User.GetByName(txtName.Text);

            if (mu == null || mu.Pass != Utils.Crypto.CalculateMD5Hash(txtPass.Text))
            {
                SetLoginError("User or pass is wrong");
                return;
            }
            
            Request.Cookies.Add(new HttpCookie("userId", mu.Id.ToString()));

            _name = mu.FullName;
            ClearError();
        }

        protected void btnRegister_OnClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtRegName.Text))
            {
                SetRegisterError("Username cannot be empty");
                return;
            }
            if (string.IsNullOrEmpty(txtRegPass1.Text))
            {
                SetRegisterError("Password cannot be empty");
                return;
            }
            if (txtRegPass1.Text != txtRegPass2.Text)
            {
                SetRegisterError("Passwords don't match");
                return;
            }
            if (string.IsNullOrEmpty(txtRegFullName.Text))
            {
                SetRegisterError("Name cannot be empty");
                return;
            }

            int muid;

            try
            {
                muid = User.Add(new User
                {
                    Name = txtRegName.Text,
                    Pass = Utils.Crypto.CalculateMD5Hash(txtRegPass1.Text),
                    FullName = txtRegFullName.Text,
                    Mail = txtRegMail.Text,
                    Phone = txtRegPhone.Text
                });
            }
            catch (Exception mex)
            {
                SetRegisterError(mex.Message);
                return;
            }

            Request.Cookies.Add(new HttpCookie("userId", muid.ToString()));

            _name = txtRegFullName.Text;

            ClearError();
        }

        private void SetLoginError(string text)
        {
            //lblRegResult.Text = $"<div class=\"alert alert-warning\"> <strong>Warning!</strong> {text}</div>";
            
            _fromLoginError = text;
        }

        private void SetRegisterError(string text)
        {
            //lblRegResult.Text = $"<div class=\"alert alert-warning\"> <strong>Warning!</strong> {text}</div>";
            
            _fromRegError = text;
        }

        protected void ClearError()
        {
            _fromLoginError =
                _fromRegError = null;
        }

        protected void OnLogOut(object sender, EventArgs e)
        {
            ClearError();
            PerformLogout();
        }

        protected void PerformLogout()
        {
            Response.Cookies.Remove("userId");
            _name = null;
        }
    }
}
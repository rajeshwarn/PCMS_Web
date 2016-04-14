﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace PCMS_Web.Admin
{
    public partial class adminMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["userType"].ToString() != "admin" || Session["userType"] == null)
            {
                Response.Redirect("../General/destroySession.aspx");
            }
            else
            {
                docName_lbl.Text = Session["fullName"].ToString();
                docNameTop_lbl.Text = Session["fullName"].ToString();
                userType_lbl.Text = Session["userType"].ToString();
                docNameMain_lbl.Text = Session["fullName"].ToString();
            }
        }
    }
}
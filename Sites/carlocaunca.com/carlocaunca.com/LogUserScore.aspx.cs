using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace carlocaunca.com
{
    public partial class LogUserScore : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.Request.HttpMethod == "POST")
            {
                string initials = (Request.Form["Initials"]);
                string score = (Request.Form["Score"]);
                
                Response.Clear();
                Response.ContentType = "application/text; charset=utf-8";
                Response.Write("Total Score: 200");
                Response.End();
            }
        }
    }
}
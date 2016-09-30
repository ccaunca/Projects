using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using carlocaunca.com.Models;

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
                
                using (var db = new FallDownContext())
                {
                    HighScore highScore = new HighScore
                    {
                        Name = "ZZZ",
                        Score = Convert.ToInt32("23"),
                        InsertDatetime = DateTime.Now
                    };
                    db.HighScores.Add(highScore);
                    db.SaveChanges();
                }
                Response.Clear();
                Response.ContentType = "application/text; charset=utf-8";
                Response.Write("Total Score: 200");
                Response.End();
            }
        }
    }
}
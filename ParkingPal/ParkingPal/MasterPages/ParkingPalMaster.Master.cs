using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ParkingPal.MasterPages
{
    public partial class ParkingPalMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Enforce the application to not store in the cache to resolve some back button issues:
            Response.Cache.SetNoStore();
            Response.Cache.AppendCacheExtension("no-cache");
            Response.Expires = 0;

            if (Session["User"] == null)
            {
                unauthenticatedNav.Visible = true;
            }
            else
            {
                authenticatedNav.Visible = true;
            }
        }
    }
}
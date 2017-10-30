using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ParkingPal.BL;
using ParkingPal.Models;
using System.Drawing; 
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
// using Tesseract; 
using System.Web.Script.Services;
using System.Web.Services;
using System.IO;
//using Tesseract;

namespace ParkingPal.UL
{
    [ScriptService]
    public partial class ULInspectorDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strNewURL = null;

            // Authenticate the user:
            AppUser appUser = (AppUser)Session["AppUser"];
            try
            {
                string pageURL = HttpContext.Current.Request.Url.AbsolutePath;
                string redirect = Authenticator.AuthenticateUser(appUser, pageURL);
                if (redirect != null)
                {
                    strNewURL = "~" + redirect;
                }
                else
                {
                    Inspector inspector = (Inspector)Session["Inspector"];
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script", "$(function () { init(); });", true);

                    regoSearch.Visible = false;

                    if (Session["Rego"] != null)
                    {
                        regoSearch.InnerText = "You have searched for registration " + Session["Rego"];
                        regoSearch.Visible = true;
                        TicketSearch(Session["Rego"].ToString());
                        Session.Remove("Rego");
                    }
                }
            }
            catch (Exception exception)
            {
                strNewURL = "~/UL/ULError.aspx";
                Session["exception"] = exception;
            }
            finally
            {
                // Redirect to the next page:
                if (strNewURL != null)
                {
                    Response.Redirect(strNewURL);
                }
            }
        }

        // Searches for Tickets associated with a registration number.
        protected void SearchForTickets(object sender, EventArgs e)
        {
            Session["Rego"] = Tbx_RegistrationNum.Text;
            Response.Redirect("~/UL/ULInspectorDashboard.aspx");
        }

        public void TicketSearch(string rego)
        {
            List<Ticket> tickets = BLInspectorDashboard.GetCurrentTicketsForRego(rego);
            LVTickets.DataSource = tickets;
            LVTickets.DataBind();
        }

        [WebMethod(EnableSession = true)]
        public static void UploadImage(string imageData)
        {
            // Convert base 64 string to byte[]
            byte[] imageBytes = Convert.FromBase64String(imageData);
            // Convert byte[] to Image
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                string rootPath = HttpRuntime.AppDomainAppPath;
                string filePath = HttpRuntime.AppDomainAppPath + "CapturedImages\\"; //FOR DEBUGGING
                image.Save(filePath + "test.jpeg"); // FOR DEBUGGING

                // LICENCE PLATE RECOGNITION:
                string dataPath = rootPath + "\\tessdata";
                var img = new Bitmap(image);
            //    var ocr = new TesseractEngine(dataPath, "eng", EngineMode.TesseractAndCube);
            //    var page = ocr.Process(img);
            //    string rego = page.GetText();

              //  HttpContext.Current.Session["Rego"] = rego;
            }
        }
    }
}
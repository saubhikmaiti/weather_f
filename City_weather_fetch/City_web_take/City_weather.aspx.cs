using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using City_weather_fetch;
namespace City_web_take
{
    public partial class _Default : System.Web.UI.Page
    {
        // create object of Weather class in WCF file
           Weather client = new Weather(); 
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // block executing page load after 1st time
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {
           //none
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            // initialize the city name and URL of WCF file in Business Layer(City_weather_fetch)
            City_info.City_name = TextBox1.Text;
            City_info.Url = "http://www.google.com/ig/api?weather=" + string.Format(City_info.City_name);
         
            // call method of Weather class return int value
            int j= client.get_weather_of_city();
           if (j == 0)
           {
               Response.Write("<script>alert('City value is NOT validated')</script>");
           }
            client.Close();

        }
    }
}

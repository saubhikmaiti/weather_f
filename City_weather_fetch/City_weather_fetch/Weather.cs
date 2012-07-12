using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Net;
using System.Xml;
using System.Web;
namespace City_weather_fetch
{
	public class Weather : City_weather
	{
        HttpWebRequest Request;
        HttpWebResponse Response;
        System.Xml.XmlDocument xmldoc;
        public static string city_name;
        public static string url;
        public Weather()
        {
            city_name = "";
            url = "";
        }
       
        public  int get_weather_of_city()
        {
             int i = 0;
             city_name= City_info.City_name;
             url = City_info.Url;
            
             try
             {
               
                XmlNode root = Weather_report(city_name,url);
             
                              
               XmlNodeList nodeList1 = root.SelectNodes("weather/forecast_information");
               // printing city name from 1st node of google XML api
               HttpContext.Current.Response.Write("<center><b>City Name You Choose : " + nodeList1.Item(0).SelectSingleNode("city").Attributes["data"].InnerText + "</b></center>");


               // printing current weather temp. in °C | °F and  conditions of wind and humidity from 2nd nodelist of google XML api(today)
               XmlNodeList nodeList2 = root.SelectNodes("weather/current_conditions");
              
               HttpContext.Current.Response.Write("<center><table class=\'bordered\' bgcolor='#FFCC99' cellpadding=\'5\'><tr>");
               HttpContext.Current.Response.Write("<td><b><big><nobr>Temp:" + nodeList2.Item(0).SelectSingleNode("temp_c").Attributes["data"].InnerText + " °C | " + nodeList2.Item(0).SelectSingleNode("temp_f").Attributes["data"].InnerText + " °F</nobr></big></b><br />");
               HttpContext.Current.Response.Write("<b>Current :</b> " + nodeList2.Item(0).SelectSingleNode("condition").Attributes["data"].InnerText + "<br />");
               HttpContext.Current.Response.Write("<b>" + nodeList2.Item(0).SelectSingleNode("wind_condition").Attributes["data"].InnerText + "</b><br />");
               HttpContext.Current.Response.Write( "<b>"+nodeList2.Item(0).SelectSingleNode("humidity").Attributes["data"].InnerText+"</b>");
            
               
               XmlNodeList nodeList3 = root.SelectNodes("descendant::weather/forecast_conditions");
               
               // printing next 4 days weather forcast  information 
               foreach (XmlNode node in nodeList3)  // loop etarating 4 times
               {

                   HttpContext.Current.Response.Write("</td><td align=\'center\'>" + node.SelectSingleNode("day_of_week").Attributes["data"].InnerText + "<br />");
               //print images for information of weather forcusing taking image path src in google XML node API
                   HttpContext.Current.Response.Write("<img src='http://www.google.com" + node.SelectSingleNode("icon").Attributes["data"].InnerText + "' alt='" + node.SelectSingleNode("condition").Attributes["data"].InnerText + "' /><br />");
                  //low temp in C
                   HttpContext.Current.Response.Write("Low:" + convert_to_centegrade(node.SelectSingleNode("low").Attributes["data"].InnerText) + "°C | ");
                   //high temp in C
                   HttpContext.Current.Response.Write("High:" + convert_to_centegrade(node.SelectSingleNode("high").Attributes["data"].InnerText) + "°C");
                   
                   HttpContext.Current.Response.Write("<br><b>Current :</b> " + node.SelectSingleNode("condition").Attributes["data"].InnerText + "<br />");
             
               }
               
               
                   HttpContext.Current.Response.Write("</td></tr></table></center>");
                    i++;
           }
           catch (Exception r) 
           {
               return i;  
           //
           }
           return i;
       }


        // converting farenhit to Centrigrade


        public string convert_to_centegrade(string p)
         {
           int f = int.Parse(p);
           double c = (f - 32) * 5 / 9;
           return c.ToString();
         }
          
        


        // to make the web request from specific URL and take Response from the google API

         public  System.Xml.XmlNode Weather_report(string city_name,string url)
          {
            
          
            try
            { 
                //     Initializes a new System.Net.WebRequest instance for the specified URI scheme.
                //
                // Parameters:
                //   requestUriString:
                //     The URI that identifies the Internet resource.
                //
                // Returns:
                //     A System.Net.WebRequest descendant for the specific URI scheme.
                
                Request = (HttpWebRequest)WebRequest.Create(url); 
                
                Response = (HttpWebResponse)Request.GetResponse();//return a response from google API resource
                //initialize a XMLDocument class 
                xmldoc = new XmlDocument();
                xmldoc.Load(Response.GetResponseStream()); //load XML document from this specified stream 
                // take the root of current google XML document...
                XmlNode root =xmldoc.DocumentElement;
                return root; 
             }
            catch (System.Exception ex) 
            {
                //ex.Message
            }
            finally
            {
                Response.Close();
            }

            return null;
        }

         public void Close()
         {
            //Response.Close();
         }
    }

	}


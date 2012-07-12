using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace City_weather_fetch
{
	// NOTE: If you change the interface name "IService1" here, you must also update the reference to "IService1" in App.config.
	[ServiceContract]
	public interface City_weather
	{
		[OperationContract]
	 	int get_weather_of_city();
        System.Xml.XmlNode Weather_report(string city_name,string url);
        string convert_to_centegrade(string p);
	}
    [DataContract]
    public  class City_info
    {
        [DataMember]
        public static string City_name { get; set; }

        [DataMember]
        public static string Url { get; set; }


       
    }
}

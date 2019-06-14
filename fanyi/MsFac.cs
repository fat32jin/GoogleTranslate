using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
 

namespace fanyi
{
    public class MsFac:ITranFac
    {
        public string TranTxt(string source  , string from, string to)
        {

            return Translatesoap(source, from, to);
        }

        public string TranTxt2(string source, string from, string to)
        {

            return Translate(source, from, to);
        }

        public String Translatesoap(String strTranslateString, string flang, string tlang)
        {
            string result;

            string appid = "DF9E54CA96F73F2E289AEC059F407DE8295A6515";
            try
            {
                ServiceReference1.LanguageServiceClient client = new ServiceReference1.LanguageServiceClient();
                result = client.Translate(appid, strTranslateString, flang, tlang, "text/html", "general", "");
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

            static string KEY = "DF9E54CA96F73F2E289AEC059F407DE8295A6515";

        public string HttpGet(string source)
        {
            string text = "Use pixels to express measurements for padding and margins.";
            string from = "en";
            string to = "zh";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;
            string authToken = "Bearer" + " " + KEY; //admToken.access_token;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);

            return "";

        }

        public String Translate(String strTranslateString, string flang, string tlang)
        {
            string appId = "AFC76A66CF4F434ED080D245C30CF1E71C22959C";   //http://www.bing.com/toolbox/bingdeveloper/ 申请自己的appid
            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?appId=" + appId + "&text=" + System.Web.HttpUtility.UrlEncode(strTranslateString) + "&from=" + flang + "&to=" + tlang + "";
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    return (string)dcs.ReadObject(stream);
                }
            }
            catch (WebException ex)
            {
                return ex.Message;
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.Specialized;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Web;
using System.Security.Cryptography;

namespace fanyi
{
    public class YouDaoFac : ITranFac
    {
        string ITranFac.TranTxt(string source, string from, string to)
        {
            return fanyi(source, from, to);
        }

        public  string fanyi(string source, string from,  string to)
        {

            string q = source;
            string appKey = "181e013f6c6021f9";
            //string from = "auto";
             
            string salt = DateTime.Now.Millisecond.ToString();
            string appSecret = "7AA2s9xLdFhi6DklLaNtCDeJeE2lbCNX";
            MD5 md5 = new MD5CryptoServiceProvider();
            string md5Str = appKey + q + salt + appSecret;
            byte[] output = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(md5Str));
            string sign = BitConverter.ToString(output).Replace("-", "");

            string url = string.Format("http://openapi.youdao.com/api?appKey={0}&q={1}&from={2}&to={3}&sign={4}&salt={5}", appKey, System.Web.HttpUtility.UrlDecode(q, System.Text.Encoding.GetEncoding("UTF-8")), from, to, sign, salt);
            WebRequest translationWebRequest = WebRequest.Create(url);

            WebResponse response = null;

            response = translationWebRequest.GetResponse();
            Stream stream = response.GetResponseStream();

            Encoding encode = Encoding.GetEncoding("utf-8");

            StreamReader reader = new StreamReader(stream, encode);
            string result = reader.ReadToEnd();
            ReJsonYoudao reobj = JsonConvert.DeserializeObject<ReJsonYoudao>(result);


            return reobj.translation[0];

        }
    }

    public class ReJsonYoudao
    {
        public string errorCode;
        public string query;
        public List<string> translation;
        public string l;
        public string speakUrl;

    }
}

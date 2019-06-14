using System;
using System.Collections.Generic;
using System.Text;

using System.Web;
using System.Net;
using System.Text.RegularExpressions;
using System.IO;
using Newtonsoft.Json.Serialization;
using Microsoft.CSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
 
using GoogleTranslateToken;


namespace fanyi
{
    public class GoogleDotNetFac :ITranFac
    {
        public string TranTxt(string source ,string from ,string to)
        {
            

            string dd = GoogleTranslate(source, from, to);

            return dd;
        }

        /// <summary>

        /// 谷歌翻译

        /// </summary>

        /// <param name="text">待翻译文本</param>

        /// <param name="fromLanguage">自动检测：auto</param>

        /// <param name="toLanguage">中文：zh-CN，英文：en</param>

        /// <returns>翻译后文本</returns>
        public string GoogleTranslate(string text, string fromLanguage, string toLanguage)
        {
            CookieContainer cc = new CookieContainer();          

            string tk = new Token().GetToken(text, new TKK().GetTKK());

            string googleTransUrl = "https://translate.google.cn/translate_a/single?client=t&sl=auto" + "&tl=" 
                + toLanguage 
                + "&hl=en&dt=at&dt=bd&dt=ex&dt=ld&dt=md&dt=qca&dt=rw&dt=rm&dt=ss&dt=t&ie=UTF-8&oe=UTF-8&otf=1&ssel=0&tsel=0&kc=1&tk=" 
                + tk + "&q=" + HttpUtility.UrlEncode(text);
           

           
            var ResultHtml = GetResultHtml(googleTransUrl, cc);
           
            StringBuilder ResultText = new StringBuilder();

            JArray TempResult = null;
            //JObject TempResult2 = null;
            try
            {
                TempResult = Newtonsoft.Json.JsonConvert.DeserializeObject<JArray>(ResultHtml);
                //TempResult2 = Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(ResultHtml);
            }
            catch (Exception ex)
            {
                return  ex.Message + ":" + ResultHtml.ToString();
            }

            var xx = TempResult[0];
            int i = 0;
            foreach (var item in xx)
            {
               
                foreach( var s in item)
                {
                    if(s== null)
                    {
                        break;
                    }
                    ResultText.Append(s.Value<string>());
                    break;
                }              

                i++;
            }

            


            return ResultText.ToString();
        }


        public string GetResultHtml(string url, CookieContainer cc)
        {

            var html = "";
            var webRequest = WebRequest.Create(url) as HttpWebRequest;           

            webRequest.Method = "GET";
            // webRequest.CookieContainer = cookie;
            webRequest.Referer = "http://www.123.com";
            webRequest.Timeout = 10000;
            webRequest.Headers.Add("X-Requested-With:XMLHttpRequest");
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/39.0.2171.71 Safari/537.36";

            try
            {
                using (var webResponse = (HttpWebResponse)webRequest.GetResponse())
                {
                    using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding.UTF8))
                    {
                        html = reader.ReadToEnd();
                        reader.Close();
                        webResponse.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                html = ex.Message;
            }
            return html;
        }


       




    }


    public class Trans
    {
        public String trans;
        public String orig;
        public String translit;
        public String src_translit;
        // setters and getters omitted, 此处略去get，set方法  
    }

    public class TransWrapper
    {
        public Trans[] sentences;
        public String src;
        public String server_time;

        // setters and getters omitted, 此处略去get，set方法  
    }

}

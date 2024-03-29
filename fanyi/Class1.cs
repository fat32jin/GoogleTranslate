﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Text.RegularExpressions;

namespace fanyi
{
    class Class1 : ITranFac
    {
        public string TranTxt(string source, string from, string to)
        {
            return googleTranslation( source);
        }

        public static string googleTranslation(string text)
        {
            if (text == "" || text == null)
            {
                return "";
            }
            else
            {
                string result = "";
                string url = "https://translate.google.cn/translate_a/single?client=gtx&sl=en&tl=zh-CN&dt=t&q=" + text;
                string jsonData = GetInfo(url);
                string pattern = "\"([^\"]*)\"";
                int count = Regex.Matches(jsonData, pattern).Count;
                                 MatchCollection matches = Regex.Matches(jsonData, pattern);
                for (int i = 0; i < count - 1; i += 2)
                {
                    result += matches[i].Value.Trim().Replace("\"", "");
                }


                return result;
            }
        }
        public static bool InChinese(string StrChineseString)
        {
            return Regex.IsMatch(StrChineseString, ".*[\\u4e00-\\u9faf].*");
        }
        public static string GetInfo(string url)
        {
            ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            //访问http方法  
            string strBuff = "";
            Uri httpURL = new Uri(url);
            ///HttpWebRequest类继承于WebRequest，并没有自己的构造函数，需通过WebRequest的Creat方法建立，并进行强制的类型转换     
            HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(httpURL);
            ///通过HttpWebRequest的GetResponse()方法建立HttpWebResponse,强制类型转换     
            HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
            ///GetResponseStream()方法获取HTTP响应的数据流,并尝试取得URL中所指定的网页内容     
            ///若成功取得网页的内容，则以System.IO.Stream形式返回，若失败则产生ProtoclViolationException错误。在此正确的做法应将以下的代码放到一个try块中处理。这里简单处理     
            Stream respStream = httpResp.GetResponseStream();
            ///返回的内容是Stream形式的，所以可以利用StreamReader类获取GetResponseStream的内容，并以     
            //StreamReader类的Read方法依次读取网页源程序代码每一行的内容，直至行尾（读取的编码格式：UTF8）     
            StreamReader respStreamReader = new StreamReader(respStream, Encoding.UTF8);
            strBuff = respStreamReader.ReadToEnd();
            return strBuff;
        }
        public static bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            //直接确认，否则打不开  
            return true;
        }
    }
}
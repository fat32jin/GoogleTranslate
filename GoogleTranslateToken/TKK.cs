using System;
using System.Net;
using System.Text.RegularExpressions;
//using System.Threading.Tasks;

namespace GoogleTranslateToken
{
    public class TKK
    {
        /// <summary>
        /// 获取谷歌页面响应数据
        /// </summary>
        /// <returns></returns>
        private string GetResponse()
        {
            WebClient wc = new WebClient();
            WebHeaderCollection whc = new WebHeaderCollection();
            whc.Add("Accept", "text/html");
            whc.Add("Accept-Charset", "utf-8");
            wc.Headers = whc;

            try
            {
                whc.Add("Host", "translate.google.cn");
                return wc.DownloadString(new Uri("https://203.208.39.255:443"));
            }
            catch
            {
                return wc.DownloadString(new Uri("https://translate.google.cn"));
            }

            //Task<string> t = new Task<string>(() =>
            //{
               

            //});
            //t.Start();
            //try
            //{
            //    t.Wait();
            //}
            //catch
            //{
            //    throw;
            //}
        }

        /// <summary>
        /// 获取TKK值
        /// </summary>
        /// <returns></returns>
        public string GetTKK()
        {
            //string response = @"TKK=eval('((function(){var a\x3d2377932982;var b\x3d-1426326282;return 408506+\x27.\x27+(a+b)})())');";
            string response = GetResponse();
            if (string.IsNullOrEmpty(response)) return null;

            Regex reg = new Regex(@"tkk:(['])(?:(?!\1).)*?\1");      //获取TKK字符串
            Match match = reg.Match(response);
            string TKKStr1 = match.Value; //"429158.3316831488";//
            Regex reg2 = new Regex(@"\d+\.\d+");      //获取TKK字符串
            Match match2 = reg2.Match(TKKStr1);
            string TKKStr = match2.Value;
            return TKKStr;
            //reg = new Regex(@"\\x3d-?\d*");     // 可能出现负值
            //MatchCollection matches = reg.Matches(TKKStr);     // 取得ab两值
            //long TKKFloat = 0;
            //foreach (Match m in matches)
            //{
            //    TKKFloat += Convert.ToInt64(m.Value.Remove(0, 4));      // 计算 a+b 的值
            //}

            ////string TKKInt = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds / 3600000).toString();   // 如果电脑时间差距太多算取的时间戳则稍微不一样
            //reg = new Regex(@"return\s\d*");
            //string TKKInt = reg.Match(TKKStr).Value.Remove(0, 7);       // 取得时间戳处理值
            //return TKKInt + "." + TKKFloat;
        }
    }
}
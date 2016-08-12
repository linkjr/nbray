using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Nbray.Security;
using Newtonsoft.Json;

namespace Nbray.Net.Push
{
    public class XgPush : IPush
    {
        //qgzh
        //private string ACCESS_ID = "2100105035";
        //private string SECRET_KEY = "0bb1962bc2d391bc88440d87f6c7a061";

        //app
        private const string ACCESS_ID = "2100169540";
        private const string SECRET_KEY = "84f0b85cc722b2f3182641df5212fb63";

        //demo app
        //private string ACCESS_ID = "2100184170";
        //private string ACCESS_KEY = "A86I21ECF4JM";
        //private string SECRET_KEY = "ddc574410cb908e03f169e3f4b50e86c";

        private const string HTTP_METHOD = "POST";

        public string Single(SingleNotification notify)
        {
            var url = "http://openapi.xg.qq.com/v2/push/single_device";
            var action = new Dictionary<string, object>
            {
                {"action_type", 1},
                {"activity", notify.Activity}
            };
            var message = new Dictionary<string, object>
            {
                {"title", notify.Title },
                {"content", notify.Content },
                {"icon_type", notify.Icon_Type},
                {"icon_res", notify.Icon_Res},
                {"custom_content", JsonConvert.SerializeObject(notify.Args)},
                {"action", JsonConvert.SerializeObject(action)}
            };

            var @params = new Dictionary<string, object>
            {
                {"access_id" , ACCESS_ID},
                {"timestamp",  Math.Floor((DateTime.Now-new DateTime(1970,1,1).ToLocalTime()).TotalSeconds)},

                {"device_token", notify.Token},
                {"message_type", notify.MessageType.GetHashCode()},
                {"message", JsonConvert.SerializeObject(message)},
                //{"message", new Dictionary<string,object>
                //    {
                //        {"title", notify.Title},
                //        {"content", notify.Content},
                //        {"custom_content", notify.Args},
                //        {"action", new Dictionary<string,object>
                //            {
                //                {"action_type", 1},
                //                {"activity", notify.Activity}
                //            }
                //        }
                //    }
                //}
            };

            @params = @params.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);
            var paras = string.Join(string.Empty, @params.Select(m => string.Format("{0}={1}", m.Key, m.Value)));
            var sign = string.Concat(
                HTTP_METHOD,
                url.Replace("http://", string.Empty),
                paras,
                SECRET_KEY
                );
            @params.Add("sign", HashEncrypt.MD5Hash(sign).ToLower());


            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_METHOD;

            var queryString = string.Join("&", @params.Select(m => string.Format("{0}={1}", m.Key, m.Value)));
            var buffer = Encoding.UTF8.GetBytes(queryString);
            if (buffer.Length > 0)
            {
                //request.Headers.Add("ContentType", "application/x-www-form-urlencoded");
                //request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }


            //var queryString = string.Join("&", @params.Select(m => string.Format("{0}={1}", m.Key, HttpUtility.UrlEncode(m.Value.ToString()))));
            //url += "?" + queryString;
            //var request = (HttpWebRequest)WebRequest.Create(url);


            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream = new StreamReader(response.GetResponseStream());
                var msg = stream.ReadToEnd();
                return msg;
            }
        }


        public string Multi(MultipleNotification notify)
        {
            throw new NotImplementedException();
        }


        public void AndroidPush(SingleNotification notify)
        {
            var @params = new
            {
                title = notify.Title,
                content = notify.Content,
                accept_time = new List<dynamic>
                {
                    new {
                        start = new
                        {
                            hour = "00",
                            min = "09"
                        },
                        end = new
                        {
                            hour = "13",
                            min = "00"
                        }
                    },
                    new{
                        start = new
                        {
                            hour = "00",
                            min = "09"
                        },
                        end = new
                        {
                            hour = "13",
                            min = "00"
                        }
                    }
                },
                n_id = 0,
                builder_id = 0,
                ring = 1,
                ring_raw = 1,
                vibrate = 1,
                lights = 1,
                clearable = 1,
                icon_type = 0,
                icon_res = "xg",
                sytle_id = 1,
                small_icon = "xg",
                action = new
                {
                    action_type = 1,
                    activity = notify.Activity,
                    aty_attr = new
                    {
                        @if = 0,
                        pf = 0
                    },
                    browser = new
                    {
                        url = "xxx",
                        confirm = 1
                    },
                    intent = "xxx"
                }
            };
        }


        public void IosPush(SingleNotification notify)
        {
            var @params = new
            {
                aps = new
                {   //  apns规定的key-value
                    alert = new
                    {
                        body = "Bob wants to play poker",
                        //action-loc-key= "PLAY"
                    },
                    badge = 5,
                    category = "INVITE_CATEGORY",
                },
                accept_time = new List<dynamic>{ //允许推送给用户的时段，选填。accept_time不会占用payload容量
                    new{
                        start = new{ hour="13", min="00" },
                        end= new{hour="14",min="00"}
                    },
                    new{
                        start=new{hour="13",min="00"},
                        end= new{hour="14",min="00"}
                    }
                }, // 仅0~9点和13~14点这两个时段可推送
                custom1 = "bar",   // 合法的自定义key-value，会传递给app
                custom2 = new string[] { "bang", "whiz" },  // 合法的自定义key-value，会传递给app
                xg = "oops"  // 错误！xg为信鸽系统保留key，其value会被信鸽系统覆盖，应避免使用
            };
        }


        public void PushWithDict(SingleNotification notify)
        {
            var paras = new Dictionary<string, object>();
            paras.Add("title", notify.Title);
            paras.Add("content", notify.Content);
            paras.Add("accept_time",
                @"[{
                    ""start"":{""hour"":""00"",""min"":""00""},
                    ""end"":{""hour"":""09"",""min"":""00""},
                },
                {
                    ""start"":{""hour"":""13"",""min"":""00""},
                    ""end"":{""hour"":""14"",""min"":""00""},
                }]");//表示消息将在哪些时间段允许推送给用户，选填
            paras.Add("n_id", 0);//通知id，选填。若大于0，则会覆盖先前弹出的相同id通知；若为0，展示本条通知且不影响其他通知；若为-1，将清除先前弹出的所有通知，仅展示本条通知。默认为0
            paras.Add("builder_id", 0);// 本地通知样式，必填
            paras.Add("ring", 1);// 是否响铃，0否，1是，下同。选填，默认1
            paras.Add("ring_raw", "ring");// 指定应用内的声音（ring.mp3），选填
            paras.Add("vibrate", 1);// 是否振动，选填，默认1
            paras.Add("lights", 1);// 是否呼吸灯，0否，1是，选填，默认1
            paras.Add("clearable", 1);// 通知栏是否可清除，选填，默认1
            paras.Add("icon_type", 0);//默认0，通知栏图标是应用内图标还是上传图标,0是应用内图标，1是上传图标,选填
            paras.Add("icon_res", "xg");// 应用内图标文件名（xg.png）或者下载图标的url地址，选填
            paras.Add("style_id", 1);//Web端设置是否覆盖编号的通知样式，默认1，0否，1是,选填
            paras.Add("small_icon", "xg");//指定状态栏的小图片(xg.png),选填

            var action = new Dictionary<string, object>();
            action.Add("action_type", 1);// 动作类型，1打开activity或app本身，2打开浏览器，3打开Intent
            action.Add("activity", notify.Activity);
            action.Add("aty_attr",
                @"{
                                ""if"":0,
                                ""pf"":0
                            }");// activity属性，只针对action_type=1的情况
            action.Add("browser", @"{""url"":""xxx"",""confirm"":1}");// url：打开的url，confirm是否需要用户确认    
            action.Add("intent", "xxx");

            paras.Add("action", action);// 动作，选填。默认为打开app
        }


        public string All(AllNotification notify)
        {
            var url = "http://openapi.xg.qq.com/v2/push/all_device";

            var action = new Dictionary<string, object>
            {
                {"action_type", 1},
                {"activity", notify.Activity}
            };

            var message = new Dictionary<string, object>
            {
                {"title", notify.Title },
                {"content", notify.Content },
                {"icon_type", notify.Icon_Type},
                {"icon_res", notify.Icon_Res},
                {"custom_content", notify.Args},
                {"action",JsonConvert.SerializeObject(action)}
            };

            var @params = new Dictionary<string, object>
            {
                {"access_id" , ACCESS_ID},
                {"timestamp",  Math.Floor((DateTime.Now-new DateTime(1970,1,1).ToLocalTime()).TotalSeconds)},
                {"message_type", notify.MessageType.GetHashCode()},
                {"message", JsonConvert.SerializeObject(message)},
                //{"message", new Dictionary<string,object>
                //    {
                //        {"title", notify.Title},
                //        {"content", notify.Content},
                //        {"custom_content", notify.Args},
                //        {"action", new Dictionary<string,object>
                //            {
                //                {"action_type", 1},
                //                {"activity", notify.Activity}
                //            }
                //        }
                //    }
                //}
            };

            @params = @params.OrderBy(m => m.Key).ToDictionary(m => m.Key, m => m.Value);
            var paras = string.Join(string.Empty, @params.Select(m => string.Format("{0}={1}", m.Key, m.Value)));
            var sign = string.Concat(
                HTTP_METHOD,
                url.Replace("http://", string.Empty),
                paras,
                SECRET_KEY
                );
            @params.Add("sign", HashEncrypt.MD5Hash(sign).ToLower());

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = HTTP_METHOD;
            request.Headers.Add("contenttype", "application/x-www-form-urlencoded");
            //request.ContentType = "application/x-www-form-urlencoded";

            var queryString = string.Join("&", @params.Select(m => string.Format("{0}={1}", m.Key, m.Value)));
            var buffer = Encoding.UTF8.GetBytes(queryString);
            if (buffer.Length > 0)
            {
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
            }
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                var stream = new StreamReader(response.GetResponseStream());
                var msg = stream.ReadToEnd();
                return msg;
            }
        }
    }
}

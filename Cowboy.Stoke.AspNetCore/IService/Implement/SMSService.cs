using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Caching;
using Cowboy.Stroke.AspNetCore.Options;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.IService.Implement
{
    public class SMSService:ISMSService
    {
        #region Field
        /// <summary>
        /// 行程数据上下文
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        ///  日志
        /// </summary>
        /// <value>The logger.</value>
        private ILogger Logger { get; set; }

        private readonly IHttpContextAccessor contextAccessor;
        /// <summary>
        /// The memory
        /// </summary>
       private  readonly IMemoryCache memory;
        private readonly SMSOptions options;
        #endregion

        #region Constructor        
        /// <summary>
        /// </summary>
        /// <param name="strokeContext">The stroke context.</param>
        /// <param name="logger"></param>
        /// <param name="options"></param>
        /// <param name="contextAccessor"></param>
        /// <param name="memory"></param>
        public SMSService(StrokeContext strokeContext, ILogger<SMSService> logger, 
            IOptions<SMSOptions> options,
            IHttpContextAccessor contextAccessor, IMemoryCache memory)
        {
            this.strokeContext = strokeContext;
            this.Logger = logger;
            this.contextAccessor = contextAccessor;
            this.memory = memory;
            this.options = options.Value;
        }

        /// <summary>
        /// 发送验证码
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        /// <exception cref="Exception">规定时间内不能重复获取验证码</exception>
        public async  Task<Response<bool>> SendIdentifyingCodeAsync(string phone, string prefix)
        {
            var cacheKey = $"sms_{prefix}_{phone}";
            if (this.memory.TryGetValue<CachedIdentifyingCode>(cacheKey, out CachedIdentifyingCode cacheCode))
            {
                if (cacheCode.LastTime.HasValue && (DateTime.Now - cacheCode.LastTime.Value).TotalSeconds < 60)
                {
                    throw new Exception("规定时间内不能重复获取验证码");
                }
            }
            else
            {
                cacheCode = new CachedIdentifyingCode()
                {
                    Code = new Random().Next(1000, 9999).ToString()
                };
                this.memory.Set(cacheKey, cacheCode, DateTimeOffset.Now.AddSeconds(180));
            }
            string message = $"您好，您的验证码：{cacheCode.Code}，请及时输入";
            bool isSuccess = await this._SendMessage2Phone(phone, message, cacheCode.Code);
            if (isSuccess)
            {
                cacheCode.LastTime = DateTime.Now;
            }
            return new Response<bool>
            {
                Data = isSuccess
            };
        }
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="message">The message.</param>
        /// <param name="code">The code.</param>
        /// <returns>Task&lt;System.Boolean&gt;.</returns>
        async Task<bool> _SendMessage2Phone(string phone, string message, string code = "")
        {
            bool isSuccess = false;
            string resultString = null;
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string body = $"account={options.ApiKey}&pswd={options.ApiSecurity}&needstatus=true&msg={message}&mobile={phone}&sendtime={DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}";
                    using (HttpContent content = new StringContent(body, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded"))
                    {
                        string url = "http://send.18sms.com/msg/HttpBatchSendSM";
                        using (HttpResponseMessage responseMessage = await client.PostAsync(url, content))
                        {
                            resultString = await responseMessage.Content.ReadAsStringAsync();
                            isSuccess = !string.IsNullOrEmpty(resultString) && resultString.IndexOf(",0\n") >= 0;
                        }
                    }
                }

                this.strokeContext.SmsSends.Add(new SmsSend
                {
                    Cphone = phone,
                    Content = message,
                    Code = code,
                    IsSuccess = isSuccess,
                });
                this.strokeContext.SaveChanges();
            }
            catch
            {
            }
            return isSuccess;
        }


        /// <summary>
        /// 验证验证码是否正确
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <param name="code">The code.</param>
        /// <param name="prefix">The prefix.</param>
        /// <returns>Task&lt;Response&lt;System.Boolean&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Response<bool> ValidateIdentifyingCodeAsync(string phone, string code, string prefix)
        {
            var cacheKey = $"sms_{prefix}_{phone}";
            if (this.memory.TryGetValue<CachedIdentifyingCode>(cacheKey, out CachedIdentifyingCode cacheCode) && cacheCode.Code == code)
            {
                this.memory.Remove(cacheKey);
                return new Response<bool>
                {
                    Data = true
                };
               // return Task.FromResult(true);
            }
            //return Task.FromResult(false);
            return new Response<bool>
            {
                Data = false
            };
        }
        #endregion
    }
}

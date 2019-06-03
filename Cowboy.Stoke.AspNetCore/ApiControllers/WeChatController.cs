// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-22-2019
//
// Last Modified By : pan
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="WeChatController.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Views.WeChat;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Senparc.Weixin.MP.AdvancedAPIs;
using Senparc.Weixin.TenPay.V2;
using Senparc.Weixin.TenPay.V3;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.ApiControllers
{
    /// <summary>
    /// Class WeChatController.
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    [Route("[controller]/[action]")]
    public class WeChatController: Controller
    {
        #region Filed
        /// <summary>
        /// The logger
        /// </summary>
        private  ILogger Logger { get; set; }
        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;
        /// <summary>
        /// The we chat options
        /// </summary>
        private readonly WeChatOptions weChatOptions;


        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="WeChatController"/> class.
        /// </summary>
        /// <param name="logger">The logger.</param>
        /// <param name="memoryCache">The memory cache.</param>
        /// <param name="weChatOptions">The we chat options.</param>
        public WeChatController(ILogger<WeChatController> logger, IMemoryCache memoryCache,IOptions<WeChatOptions> weChatOptions)
        {
            this.Logger = logger;
            this.memoryCache = memoryCache;
            this.weChatOptions = weChatOptions.Value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 微信JsPay支付
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="state">The state.</param>
        /// <returns>ActionResult.</returns>
        [HttpPost]
        [HttpGet]
        public ActionResult JsPay(string code, string state)
        {
            try
            {
                Logger.LogDebug("进来啦");

                var openIdResult = OAuthApi.GetAccessToken(this.weChatOptions.AppId, this.weChatOptions.AppSecret, code);
                if (openIdResult.errcode != Senparc.Weixin.ReturnCode.请求成功)
                {
                    throw new Exception($"微信支付申请失败,Erro:{openIdResult.errmsg}");
                }
                Logger.LogDebug("获取AccessToken成功");
                if (this.memoryCache.TryGetValue<WeChatPayRequest>(state, out WeChatPayRequest request))
                {
                    Logger.LogDebug(Newtonsoft.Json.JsonConvert.SerializeObject(request));
                }
                else
                {
                    Logger.LogDebug($"未找到对应的付款请求,state:{state}");
                }

                var array = state.Split('|');
                var bcTradeNo = array[0];
                var money = decimal.Parse(array[1]);
                var SuccessUrl = array[2].ToString();//成功跳转地址 
                var FailedUrl = array[3].ToString();//失败跳转地址

                var notifyUrl ="https://bcl.baocailang.com:8995/api/Payment/WeChatPayNotify";//回调地址

                var nonceStr = TenPayUtil.GetNoncestr();
                var timeStamp = TenPayUtil.GetTimestamp();

                string billBody = "商城--订单支付";

                var xmlDataInfo = new TenPayV3UnifiedorderRequestData(
                    this.weChatOptions.AppId,
                    this.weChatOptions.MchId,
                    billBody,
                    request.PaymentId,
                    (int)(request.TotalFee * 100),
                    "192.168.2.1",
                    notifyUrl,
                    Senparc.Weixin.TenPay.TenPayV3Type.JSAPI,
                       openIdResult.openid,
                    this.weChatOptions.ApiKey,
                    nonceStr
                    );
                Logger.LogDebug($"申请支付结果：{0}", Newtonsoft.Json.JsonConvert.SerializeObject(xmlDataInfo));
                var result = TenPayV3.Unifiedorder(xmlDataInfo);

                Logger.LogDebug($"申请JsPay支付返回code：{result.result_code}");
                Logger.LogDebug($"申请JsPay支付返回：{result.ResultXml}");

                Logger.LogDebug($"申请JsPay支付返回结果：{result.return_msg}");

                var package = string.Format("prepay_id={0}", result.prepay_id);

                Logger.LogDebug($"Package={package}");

                var jsPayParam = new WeChatPayParameter
                {
                    AppId = result.appid,
                    MchId = result.mch_id,
                    NonceStr = result.nonce_str,
                    PaySign = TenPayV3.GetJsPaySign(this.weChatOptions.AppId, timeStamp, nonceStr, package, this.weChatOptions.ApiKey),
                    SuccessUrl = request.SuccessUrl,
                    FailedUrl = request.FailedUrl,
                    Package = package,
                    TimeStamp = timeStamp
                };

                return View(jsPayParam);
            }
            catch (Exception ex)
            {
                Logger.LogDebug("启动微信支付失败--{0}", ex.Message);
                throw new Exception(ex.Message);
            }
        } 
        #endregion
    }
}

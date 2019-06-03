// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-23-2019
//
// Last Modified By : pan
// Last Modified On : 01-23-2019
// ***********************************************************************
// <copyright file="PaymentService.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Options;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.IService.Implement
{
    /// <summary>
    /// Class PaymentService.
    /// </summary>
    /// <seealso cref="Cowboy.Stroke.AspNetCore.IService.IPaymentService" />
    public class PaymentService:IPaymentService
    {
        #region Filed
        /// <summary>
        /// The stoke context
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger Logger { get; set; }

        /// <summary>
        /// The we chat options
        /// </summary>
        private readonly WeChatOptions weChatOptions;

        /// <summary>
        /// The configuration options
        /// </summary>
        private readonly ConfigOptions configOptions;

        /// <summary>
        /// The HTTP context accessor
        /// </summary>
        private readonly IHttpContextAccessor httpContextAccessor;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentService"/> class.
        /// </summary>
        /// <param name="strokeContext">The stoke context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="weChatOptions">The we chat options.</param>
        /// <param name="configOptions"></param>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        public PaymentService(StrokeContext strokeContext, ILogger<PaymentService> logger,IOptions<WeChatOptions> weChatOptions, IOptions<ConfigOptions> configOptions, IHttpContextAccessor httpContextAccessor)
        {
            this.strokeContext = strokeContext;
            this.Logger = logger;
            this.weChatOptions = weChatOptions.Value;
            this.configOptions = configOptions.Value;
            this.httpContextAccessor = httpContextAccessor;
        }

        #endregion


        /// <summary>
        /// 微信支付回调
        /// </summary>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response> WeChatPayNotify()
        {
            Logger.LogDebug("微信回调返回成功");
            var resHandler = new Senparc.Weixin.TenPay.V3.ResponseHandler(httpContextAccessor.HttpContext);

            var returnCode = resHandler.GetParameter("return_code");//返回状态码 
            var returnMsg = resHandler.GetParameter("return_msg");//返回信息
            var outTradeNo = resHandler.GetParameter("out_trade_no");//商户订单号
            var transactionId = resHandler.GetParameter("transaction_Id");//微信支付订单号
            var appId = resHandler.GetParameter("appid");//微信开发平台审核通过的应用AppId
            var mchId = resHandler.GetParameter("mch_id");//微信支付分配的商户号 
            var openId = resHandler.GetParameter("openid");//用户在商户appid下的唯一标识
            var total_fee = resHandler.GetParameter("total_fee");//订单总金额，单位为分

            Logger.LogDebug("微信回调，获取回调信息成功");

            resHandler.SetKey(this.weChatOptions.ApiKey);
            Logger.LogDebug("微信支付回调，开始验签。密钥:{0}", this.weChatOptions.ApiKey);
            //验证请求是否从微信发送过来的，是否安全
            var isTenpaySign = resHandler.IsTenpaySign();
            Logger.LogDebug("验签：{0}", resHandler.ToString());
            Logger.LogDebug("验签结果:{0}", isTenpaySign);
            Logger.LogDebug("返回状态码:{0}", returnCode);
            if (returnCode.ToUpper() == "SUCCESS" && isTenpaySign)
            {
                Logger.LogDebug("微信回调,验签成功。");

                var paymentInfo = await this.strokeContext.Payments.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(outTradeNo));

                if (paymentInfo.Status == false)//订单没有支付
                {

                    #region 更新支付订单的支付状态
                    paymentInfo.Status = true;
                    paymentInfo.UpdateTime = DateTime.Now;
                    this.strokeContext.Payments.Update(paymentInfo);
                    #endregion

                    #region 添加支付返回信息

                    this.strokeContext.PaymentCallbacks.Add(new PaymentCallback
                    {
                        PaymentId = paymentInfo.Id,
                        OutTradeStatus = true,
                        TradeNo = transactionId,
                        OutTradeParameter = returnMsg
                    });
                    #endregion

                    var userId = 0l;
                    #region 更新订单的支付状态
        
                        var orderInfo = await this.strokeContext.Orders.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(paymentInfo.OrderIds));
                        if (orderInfo == null)
                        {
                            throw new Exception("获取订单信息失败");
                        }
                        userId = orderInfo.UserId;
                        orderInfo.OrderStatus = OrderStatus.Paid;
                        orderInfo.UpdateTime = DateTime.Now;
                        this.strokeContext.Orders.Update(orderInfo);
                    
                    #endregion

                    this.strokeContext.SaveChanges();

                    #region 添加用户积分
                    var integral =await  this.strokeContext.Integrals.FirstOrDefaultAsync(x => x.UserId == userId);
                    if (integral == null)
                    {
                        this.strokeContext.Integrals.Add(new Integral
                        {
                            UserId = userId,
                            Number = this.configOptions.Initialize
                        });
                        if (this.configOptions.Initialize > 0)
                        {
                            //添加增加积分记录
                            this.strokeContext.IntegralRecords.Add(new IntegralRecord
                            {
                                UserId =userId,
                                Number = this.configOptions.Initialize,
                                Type = 1
                            });
                        }
                    }
                    integral.Number += this.configOptions.Increment;
                    integral.UpdateTime = DateTime.Now;
                    this.strokeContext.Update(integral);
                    this.strokeContext.IntegralRecords.Add(new IntegralRecord
                    {
                        UserId = userId,
                        Type = 1,//1为增加积分，2为减少积分
                        Number = this.configOptions.Increment,
                    });
                    this.strokeContext.SaveChanges(); 
                    #endregion

                    Logger.LogDebug("微信支付回调成功");
                }
                else
                {
                    throw new Exception($"微信支付回调成功---支付订单状态已经改变，商户订订单号：{outTradeNo},返回信息：{returnMsg}.");
                }
            }
            else {
                Logger.LogDebug("订单支付成功");
            }
            return Response.Success();
        }
    }
}

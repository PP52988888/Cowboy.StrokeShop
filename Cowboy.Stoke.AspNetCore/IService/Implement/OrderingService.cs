// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-24-2019
// ***********************************************************************
// <copyright file="OrderingService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.ExtensionMethods;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Model;
using Cowboy.TravelShop.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Senparc.Weixin.MP.AdvancedAPIs;

namespace Cowboy.Stoke.AspNetCore.IService.Implement
{
    /// <summary>
    /// 订单服务
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.IService.IOrderingService" />
    public class OrderingService:IOrderingService
    {
        #region Fields
        /// <summary>
        /// 数据上下文
        /// </summary>
        private readonly StrokeContext strokeContext;

        /// <summary>
        /// The logger
        /// </summary>
        private ILogger Logger { get; set; }

        /// <summary>
        /// The memory cache
        /// </summary>
        private readonly IMemoryCache memoryCache;


        /// <summary>
        /// The context accessor
        /// </summary>
        private readonly IHttpContextAccessor contextAccessor;

        /// <summary>
        /// 微信支付参数
        /// </summary>
        private readonly WeChatOptions weChatOptions;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingService" /> class.
        /// </summary>
        /// <param name="strokeContext">The stroke context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="weChatOptions">The we chat options.</param>
        /// <param name="contextAccessor"></param>
        /// <param name="memoryCache"></param>
        public OrderingService(StrokeContext strokeContext,ILogger<OrderingService> logger,IOptions <WeChatOptions> weChatOptions,IHttpContextAccessor contextAccessor, IMemoryCache memoryCache)
        {
            this.strokeContext = strokeContext;
            this.Logger = logger;
            this.weChatOptions = weChatOptions.Value;
            this.contextAccessor = contextAccessor;
            this.memoryCache = memoryCache;
        }
        #endregion

        #region Methods

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetOrderingResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<Response<GetOrderingResponse[]>> GetOrderingInfo(GetOrderingRequest request)
        {
            var userId = contextAccessor.UserId();

            var tempRow =await  (from ordering in  this.strokeContext.Orders
                           where ordering.UserId == userId && ordering.OrderStatus==request.OrderStatus
                           select new GetOrderingResponse
                           {
                               Id = ordering.Id,
                               OrderNo = ordering.OrderNo,
                               OrderStatus = ordering.OrderStatus,
                               Price = ordering.Price,
                               ImageUrl = ordering.ImageUrl,
                               Remark = ordering.Remark,
                               Title = ordering.Title,
                               StrokeId = ordering.StrokeId,
                               City = ordering.City,
                               Destination = ordering.Destination,
                               NumberDay = ordering.NumberDay
                           }).ToArrayAsync();

            return new Response<GetOrderingResponse[]>
            {
                Data = tempRow
            };
        }

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetOrderDetailsResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<GetOrderDetailsResponse>> GetOrderDetails(GetOrderDetailsRequest request)
        {
            var userId = contextAccessor.UserId();
            var tempRows = await (from ordering in this.strokeContext.Orders
                                  where ordering.UserId == userId && ordering.Id == request.OrderId
                                  select new GetOrderDetailsResponse
                                  {
                                      Id = ordering.Id,
                                      OrderNo = ordering.OrderNo,
                                      OrderStatus = ordering.OrderStatus,
                                      Price = ordering.Price,
                                      ImageUrl = ordering.ImageUrl,
                                      Remark = ordering.Remark,
                                      Title = ordering.Title,
                                      StrokeId = ordering.StrokeId,
                                      City = ordering.City,
                                      Destination = ordering.Destination,
                                      NumberDay = ordering.NumberDay
                                  }).FirstOrDefaultAsync();

            if (tempRows == null)
            {
                throw new Exception("获取订单信息失败");
            }
            return new Response<GetOrderDetailsResponse>
            {
                Data = tempRows
            };
        }

        /// <summary>
        /// 未支付订单的支付
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;OrderPayResponse&gt;&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response<OrderPayResponse>> OrderPay(OrderPayRequest request)
        {
            var userId = contextAccessor.UserId();
            var userInfo = await this.strokeContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (userInfo == null)
            {
                throw new Exception("获取用户信息失败");
            }
            if (!userInfo.IsActive)
            {
                throw new Exception("此用户已经被禁用");
            }
            var orderInfo = await this.strokeContext.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId);
            if (orderInfo==null)
            {
                throw new Exception("获取订单信息失败");
            }
            //支付
            var weChatPay = new WeChatPayRequest
            {
                Body = HttpUtility.UrlEncode(orderInfo.Title),
                TotalFee = orderInfo.TotalAmount,
                SuccessUrl = request.SuccessUrl,
                FailedUrl = request.FaildUrl,
                NotifyUrl = weChatOptions.HostUrl + "/api/Payment/WeChatpayNotify",
                SignType = "MD5",
                OrderIds = orderInfo.Id.ToString()
            };


            //获取微信公众号中，微信js   支付信息
            var result = GetJsPayRedirect(weChatPay, PaymentType.WeChat);
            Logger.LogDebug($"支付链接-----{result}");

            return new Response<OrderPayResponse>
            {
                Data = new OrderPayResponse
                {
                    MwebUrl = result
                }
            };
        }

        /// <summary>
        /// 创建微信JsPay支付请求信息
        /// </summary>
        /// <param name="weChatPay">The we chat pay.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns>System.String.</returns>
        public string GetJsPayRedirect(WeChatPayRequest weChatPay, PaymentType paymentType)
        {
            Logger.LogDebug("创建支付订单信息");
            var paymentInfo = CreatePaymentInfo(weChatPay.OrderIds, paymentType);
            Logger.LogDebug($"订单支付PaymentId:{paymentInfo.Id}");
            var returnUrl = string.Format("https://bcl.baocailang.com/WeChat/JsPay");
            var state = string.Format("{0}|{1}|{2}|{3}|{4}", paymentInfo.Id, weChatPay.TotalFee, weChatPay.SuccessUrl, weChatPay.FailedUrl,weChatPay.Body);//申请时携带的状态参数

            //var returnUrl = HttpUtility.UrlEncode(weChatOptions.HostUrl + "/WeChat/JsPay");//不能加端口号 
            //var state = Guid.NewGuid().ToString("N");
            //this.memoryCache.Set(state, weChatPay, TimeSpan.FromMinutes(2));
            Logger.LogDebug("创建state成功");
            return OAuthApi.GetAuthorizeUrl(this.weChatOptions.AppId, returnUrl, state, Senparc.Weixin.MP.OAuthScope.snsapi_base);
        }

        /// <summary>
        /// 创建支付订单信息
        /// </summary>
        /// <param name="orderIds">The order ids.</param>
        /// <param name="paymentType">Type of the payment.</param>
        /// <returns>System.Object.</returns>
        public Payment CreatePaymentInfo(string orderIds, PaymentType paymentType)
        {
            var paymentInfo = this.strokeContext.Payments.Add(new Payment
            {
                OrderIds = orderIds,
                PaymentType = paymentType,
                Status = false
            });
            this.strokeContext.SaveChanges();
            return paymentInfo.Entity;
        }

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        /// <exception cref="NotImplementedException"></exception>
        public async  Task<Response> CancelOrder(OrderPayRequest request)
        {
            var userId = contextAccessor.UserId();
            var orderInfo = await this.strokeContext.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId);
            if (orderInfo==null)
            {
                throw new Exception("获取订单信息失败");
            }
            if (orderInfo.OrderStatus!=OrderStatus.NoPaid)
            {
                throw new Exception("订单已完成，不能取消!");
            }
            orderInfo.OrderStatus = OrderStatus.Cancel;
            orderInfo.UpdateTime = DateTime.Now;
            this.strokeContext.Orders.Update(orderInfo);
            this.strokeContext.SaveChanges();
            return Response.Success();
        }

        ///// <summary>                下订单和支付
        ///// 未支付订单的支付
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>Task&lt;Response&lt;PlaceAnOrderInfoResponse&gt;&gt;.</returns>
        ///// <exception cref="Exception">请添加支付密码信息
        ///// or
        ///// 支付密码错误</exception>
        ///// <exception cref="NotImplementedException">请添加支付密码信息
        ///// or
        ///// 支付密码错误</exception>
        //public async  Task<Response<PlaceAnOrderInfoResponse>> OrderPayment(OrderPaymentRequest request)
        //{
        //    var userId = contextAccessor.UserId();
        //    //var paySecurity = await this.strokeContext.PaySecurities.FirstOrDefaultAsync(x => x.UserId == userId);
        //    //if (paySecurity == null)
        //    //{
        //    //    throw new Exception("请添加支付密码信息");
        //    //}
        //    //if (paySecurity.PayPassWord == request.PayPassWord.EncryptPassword())
        //    //{
        //    //    throw new Exception("支付密码错误");
        //    //}
        //    var orderInfo = await this.strokeContext.Orders.FirstOrDefaultAsync(x => x.Id == request.OrderId);
        //    if (orderInfo==null)
        //    {
        //        throw new Exception("获取订单信息失败");
        //    }
        //    //支付
        //    var weChatPay = new WeChatPayRequest
        //    {
        //        Body = HttpUtility.UrlEncode("订单支付"),
        //        TotalFee = orderInfo.TotalAmount,
        //        SuccessUrl = request.SuccessUrl,
        //        FailedUrl = request.FaildUrl,
        //        NotifyUrl = "https://bcl.baocailang.com/api/Payment/WeChatpayNotify",
        //        SignType = "MD5"
        //    };

        //    weChatPay.OrderIds = orderInfo.Id.ToString();

        //    //获取微信公众号中，微信js   支付信息
        //    var result = GetJsPayRedirect(weChatPay, request.Payment);
        //    return new Response<PlaceAnOrderInfoResponse>
        //    {
        //        Data = new PlaceAnOrderInfoResponse
        //        {
        //            MwebUrl = result,
        //            WeChatAmount = orderInfo.TotalAmount,
        //            OutTradeNo = orderInfo.Id.ToString(),
        //            PaymentType = request.Payment
        //        }
        //    };
        //}


        ///// <summary>
        ///// 下订单
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <param name="userId">The user identifier.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        ///// <exception cref="Exception">
        ///// 请添加支付密码信息
        ///// or
        ///// 支付密码错误
        ///// or
        ///// 获取购物车信息失败
        ///// </exception>
        ///// <exception cref="NotImplementedException"></exception>
        //public async  Task<Response<PlaceAnOrderInfoResponse>> PlaceOrder(PlaceOrderRequest request)
        //{
        //    var userId = contextAccessor.UserId();

        //    var paySecurity = await this.strokeContext.PaySecurities.FirstOrDefaultAsync(x => x.UserId == userId);
        //    if (paySecurity==null)
        //    {
        //        throw new Exception("请添加支付密码信息");
        //    }
        //    if (paySecurity.PayPassWord == request.PayPassWord.EncryptPassword())
        //    {
        //        throw new Exception("支付密码错误");
        //    }

        //    var totalAmount = 0m;
        //    var orderIds = "";
        //    foreach (var item in request.AddtoOrderInfos)
        //    {
        //        var shoppingCartInfo =await  this.strokeContext.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == item.ShoppingCartId);
        //        if (shoppingCartInfo == null)
        //        {
        //            throw new Exception("获取购物车信息失败");
        //        }

        //        var strokeInfo =await  this.strokeContext.Strokes.FirstOrDefaultAsync(x => x.Id == shoppingCartInfo.StrokeId);
        //        #region 添加订单
        //        var orderInfo = this.strokeContext.Orders.Add(new Order
        //        {
        //            OrderNo = DateTime.Now.ToString("yyyyMMddHHmmssffff"),
        //            UserId = shoppingCartInfo.UserId,
        //            StrokeId = strokeInfo.Id,
        //            Title = strokeInfo.Title,
        //            OrderStatus = OrderStatus.NoPaid,
        //            Price = strokeInfo.Price * shoppingCartInfo.Quantity,
        //            Payment = request.Payment,
        //            Remark = item.Remark,
        //            TotalCount = shoppingCartInfo.Quantity
        //        });
        //        #endregion
        //        totalAmount += orderInfo.Entity.Price;
        //        orderIds += orderInfo.Entity.Id+"-";
        //        //删除购物车
        //        this.strokeContext.ShoppingCarts.Remove(shoppingCartInfo);
        //    }
        //    this.strokeContext.SaveChanges();

        //    //支付
        //    var weChatPay = new WeChatPayRequest
        //    {
        //        Body = HttpUtility.UrlEncode("订单支付"),
        //        TotalFee = totalAmount,
        //        SuccessUrl = request.SuccessUrl,
        //        FailedUrl = request.FaildUrl,
        //        NotifyUrl = "https://bcl.baocailang.com/api/Payment/WeChatpayNotify",
        //        SignType = "MD5"
        //    };

        //    weChatPay.OrderIds = orderIds;

        //    //获取微信公众号中，微信js   支付信息
        //    var result = GetJsPayRedirect(weChatPay, request.Payment);

        //    return new Response<PlaceAnOrderInfoResponse>
        //    {
        //        Data = new PlaceAnOrderInfoResponse
        //        {
        //            OutTradeNo = orderIds,
        //            PaymentType = request.Payment,
        //            WeChatAmount = totalAmount,
        //            MwebUrl = result
        //        }
        //    };

        //}

        ///// <summary>
        ///// 创建微信JsPay支付请求信息
        ///// </summary>
        ///// <param name="weChatPay">The we chat pay.</param>
        ///// <param name="paymentType">Type of the payment.</param>
        ///// <returns>System.String.</returns>
        //public string GetJsPayRedirect(WeChatPayRequest weChatPay, PaymentType paymentType)
        //{
        //    logger.LogDebug("创建支付订单信息");
        //    var paymentInfo = CreatePaymentInfo(weChatPay.OrderIds, paymentType);
        //    logger.LogDebug($"订单支付PaymentId:{paymentInfo.Id}");
        //    var returnUrl = string.Format("https://bcl.baocailang.com/WeChat/JsPay");
        //    var state = Guid.NewGuid().ToString("N");
        //    this.memoryCache.Set(state, weChatPay, TimeSpan.FromMinutes(2));
        //    logger.LogDebug("创建state成功");
        //    return OAuthApi.GetAuthorizeUrl(this.weChatOptions.AppId, returnUrl, state, Senparc.Weixin.MP.OAuthScope.snsapi_base);
        //}

        ///// <summary>
        ///// 创建支付订单信息
        ///// </summary>
        ///// <param name="orderIds">The order ids.</param>
        ///// <param name="paymentType">Type of the payment.</param>
        ///// <returns>System.Object.</returns>
        //public Payment  CreatePaymentInfo(string orderIds,PaymentType paymentType) {

        //    var paymentInfo=this.strokeContext.Payments.Add(new Payment
        //    {
        //        OrderIds = orderIds,
        //        PaymentType = paymentType,
        //        Status = false
        //    });
        //    this.strokeContext.SaveChanges();
        //    return paymentInfo.Entity;
        //}

        #endregion
    }
}

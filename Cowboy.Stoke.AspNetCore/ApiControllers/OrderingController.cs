// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="OrderingController.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.Stoke.AspNetCore.Controllers;
using Cowboy.Stoke.AspNetCore.IService;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.ApiControllers.Filters;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stoke.AspNetCore.ApiControllers
{
    /// <summary>
    ///  订单管理
    /// </summary>
    /// <seealso cref="Cowboy.Stoke.AspNetCore.Controllers.ApiController" />
    public class OrderingController:ApiController
    {
        #region  Fields
        /// <summary>
        /// 订单服务
        /// </summary>
        private readonly IOrderingService orderingService;
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OrderingController"/> class.
        /// </summary>
        /// <param name="orderingService">The ordering service.</param>
        public OrderingController(IOrderingService orderingService)
        {
            this.orderingService = orderingService;
        }
        #endregion


        /// <summary>
        /// 获取全部订单信息
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetOrderingResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetOrderingResponse[]>> GetOrderingInfo( [FromBody]GetOrderingRequest request ) {
            return orderingService.GetOrderingInfo(request);
        }


        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetOrderDetailsResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<GetOrderDetailsResponse>> GetOrderDetails([FromBody]GetOrderDetailsRequest request ) {
            return orderingService.GetOrderDetails(request);
        }

        /// <summary>
        /// 未支付订单的支付
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;OrderPayResponse&gt;&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response<OrderPayResponse>> OrderPay([FromBody] OrderPayRequest request)
        {
            return orderingService.OrderPay(request);
        }


        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        [HttpPost]
        [JwtAuthorize]
        public Task<Response> CancelOrder([FromBody] OrderPayRequest request) {
            return orderingService.CancelOrder(request);
        }
        ///// <summary>            下订单和支付
        ///// 下订单
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //public Task<Response<PlaceAnOrderInfoResponse>> PlaceOrder([FromBody] PlaceOrderRequest request) {
        //    return orderingService.PlaceOrder(request);
        //}

        ///// <summary>
        ///// 待支付订单的支付
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //[HttpPost]
        //public Task<Response<PlaceAnOrderInfoResponse>> OrderPayment([FromBody]  OrderPaymentRequest request) {
        //    return orderingService.OrderPayment(request);
        //}
    }
}

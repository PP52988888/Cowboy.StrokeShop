// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-18-2019
//
// Last Modified By : pan
// Last Modified On : 01-18-2019
// ***********************************************************************
// <copyright file="IOrderingService.cs" company="Cowboy.Stoke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.Stoke.AspNetCore.Model;
using Cowboy.Stroke.AspNetCore.Model.Dtos;
using Cowboy.Stroke.AspNetCore.Model.Dtos;

namespace Cowboy.Stoke.AspNetCore.IService
{
    /// <summary>
    /// 订单服务
    /// </summary>
    public interface IOrderingService
    {

        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <returns>Task&lt;Response&lt;GetOrderingResponse&gt;&gt;.</returns>
        Task<Response<GetOrderingResponse[]>> GetOrderingInfo(GetOrderingRequest request);

        /// <summary>
        /// 获取订单详细信息
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;GetOrderDetailsResponse&gt;&gt;.</returns>
        Task<Response<GetOrderDetailsResponse>> GetOrderDetails(GetOrderDetailsRequest request);


        /// <summary>
        /// 未支付订单的支付
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&lt;OrderPayResponse&gt;&gt;.</returns>
        Task<Response<OrderPayResponse>> OrderPay(OrderPayRequest request);

        /// <summary>
        /// 取消订单
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>Task&lt;Response&gt;.</returns>
        Task<Response> CancelOrder(OrderPayRequest request);


        ///// <summary>         下订单和支付
        ///// 下订单
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&gt;.</returns>
        //Task<Response<PlaceAnOrderInfoResponse>> PlaceOrder(PlaceOrderRequest request);

        ///// <summary>
        ///// 未支付订单的支付
        ///// </summary>
        ///// <param name="request">The request.</param>
        ///// <returns>Task&lt;Response&lt;PlaceAnOrderInfoResponse&gt;&gt;.</returns>
        //Task<Response<PlaceAnOrderInfoResponse>> OrderPayment(OrderPaymentRequest request);
    }
}

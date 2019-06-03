// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-20-2019
//
// Last Modified By : pan
// Last Modified On : 03-20-2019
// ***********************************************************************
// <copyright file="GetOrderingRequest.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using Cowboy.TravelShop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 获取订单信息请求
    /// </summary>
    public class GetOrderingRequest
    {
        /// <summary>
        /// 订单状态
        /// </summary>
        /// <value>The order status.</value>
        public  OrderStatus  OrderStatus { get; set; }
    }
}

// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 03-20-2019
//
// Last Modified By : pan
// Last Modified On : 03-20-2019
// ***********************************************************************
// <copyright file="OrderPayRequest.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{
    /// <summary>
    /// 未支付订单的支付请求
    /// </summary>
    public class OrderPayRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The order identifier.</value>
        public long OrderId { get; set; }

        /// <summary>
        /// 支付成功后的跳转地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get;  set; }

        /// <summary>
        /// 支付失败后的跳转地址
        /// </summary>
        /// <value>The faild URL.</value>
        public string FaildUrl { get;  set; }
    }
}

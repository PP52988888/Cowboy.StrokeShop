// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-21-2019
//
// Last Modified By : pan
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="WeChatRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 调起微信支付请求参数
    /// </summary>
    public class WeChatPayRequest
    {
        /// <summary>
        /// 商品描述
        /// </summary>
        /// <value>The body.</value>
        public string Body { get; set; }

        /// <summary>
        /// 支付订单编号
        /// </summary>
        /// <value>The payment identifier.</value>
        public string PaymentId { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        /// <value>The total fee.</value>
        public decimal TotalFee { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The order ids.</value>
        public string OrderIds { get; set; }

        /// <summary>
        /// 签名方式
        /// </summary>
        /// <value>The type of the sign.</value>
        public string SignType { get; set; }

        /// <summary>
        /// 通知地址
        /// </summary>
        /// <value>The notify URL.</value>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// jsPay支付时，支付成功后的跳转地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// jsPay支付时，支付失败后的跳转地址
        /// </summary>
        /// <value>The failed URL.</value>
        public string FailedUrl { get; set; }
    }
}

// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-21-2019
//
// Last Modified By : pan
// Last Modified On : 01-21-2019
// ***********************************************************************
// <copyright file="OrderPaymentRequest.cs" company="Cowboy.Stroke.AspNetCore">
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
    /// 待支付订单中的去支付
    /// </summary>
    [Serializable]
    public class OrderPaymentRequest
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        /// <value>The order identifier.</value>
        public long OrderId { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <value>The type of the payment.</value>
        public PaymentType Payment { get; set; }

        /// <summary>
        ///支付密码 
        /// </summary>
        /// <value>The pay pass word.</value>
        public string PayPassWord { get; set; }

        /// <summary>
        /// 备注信息
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }

        /// <summary>
        /// 支付成功后的跳转地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// 支付失败后的跳转地址
        /// </summary>
        /// <value>The faild URL.</value>
        public string FaildUrl { get; set; }
    }
}

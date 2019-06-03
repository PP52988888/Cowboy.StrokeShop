// ***********************************************************************
// Assembly         : Cowboy.Stroke.AspNetCore
// Author           : pan
// Created          : 01-22-2019
//
// Last Modified By : pan
// Last Modified On : 01-22-2019
// ***********************************************************************
// <copyright file="PlaceAnOrderInfoResponse.cs" company="Cowboy.Stroke.AspNetCore">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cowboy.TravelShop.Model;

namespace Cowboy.Stroke.AspNetCore.Model.Dtos
{

    /// <summary>
    /// Class PlaceAnOrderInfoResponse.
    /// </summary>
    [Serializable]
    public class PlaceAnOrderInfoResponse
    {

        
        /// <summary>
        /// 订单单号
        /// </summary>
        /// <value>The out trade no.</value>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        /// <value>The type of the payment.</value>
        public PaymentType PaymentType { get; set; }

        /// <summary>
        /// 微信支付所需金额
        /// </summary>
        /// <value>The we chat amount.</value>
        public decimal WeChatAmount { get; set; }

        /// <summary>
        /// 微信JsPay支付获取Code地址
        /// </summary>
        /// <value>The mweb URL.</value>
        public string MwebUrl { get; set; }
    }
}

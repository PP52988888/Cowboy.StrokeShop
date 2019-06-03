// ***********************************************************************
// Assembly         : Cowboy.Stoke.AspNetCore
// Author           : pan
// Created          : 01-19-2019
//
// Last Modified By : pan
// Last Modified On : 01-19-2019
// ***********************************************************************
// <copyright file="PlaceOrderRequest.cs" company="Cowboy.Stoke.AspNetCore">
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
    ///  下订单请求
    /// </summary>
    [Serializable]
    public class PlaceOrderRequest
    {
        /// <summary>
        /// 支付方式 
        /// </summary>
        /// <value>The payment.</value>
        public PaymentType Payment { get; set; }


        /// <summary>
        /// 支付密码
        /// </summary>
        /// <value>The pay pass word.</value>
        public string PayPassWord { get; set; }


        /// <summary>
        /// jaPay支付时，支付成功后的跳转地址
        /// </summary>
        /// <value>The success URL.</value>
        public string SuccessUrl { get; set; }

        /// <summary>
        /// jsPay支付时，支付失败后的跳转地址
        /// </summary>
        /// <value>The faild URL.</value>
        public string FaildUrl { get; set; }

        /// <summary>
        /// 要添加到订单的数据请求信息
        /// </summary>
        /// <value>The addto order infos.</value>
        public AddtoOrderInfo[] AddtoOrderInfos { get; set; }  
    }
    /// <summary>
    /// 要添加到订单的数据请求信息
    /// </summary>
    public class AddtoOrderInfo {
        /// <summary>
        /// 购物车编号
        /// </summary>
        /// <value>The shopping cart identifier.</value>
        public long ShoppingCartId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// <value>The remark.</value>
        public string Remark { get; set; }
    }
}
